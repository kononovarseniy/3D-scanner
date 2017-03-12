#define STP_PIN 2
#define DIR_PIN 5
#define EN_PIN 8

#define CLOCKWISE_SIGNAL LOW
#define COUNTER_CLOCKWISE_SIGNAL (!CLOCKWISE)

#define MSG_START '$'

#define NOP_CMD 0
#define WRITE_CMD 1
#define READ_CMD 2
#define START_CMD 3
#define STOP_CMD 4
#define ROTATE_CMD 5
#define LASER_CMD 6
#define LED_CMD 7
#define SETUP_CMD 8

#define MEMSIZE 32
#define VERSION_ADDR 0
#define CONFIG_ADDR 1 // STP(7), DIR(6), EN(5), DIVIDER(4..2), RESERVED(1..0)
  #define CFG_STP 7
  #define CFG_DIR 6
  #define CFG_EN 5
  #define CFG_D2 4
  #define CFG_D1 3
  #define CFG_D0 2
//
#define STP_PIN_ADDR 2
#define DIR_PIN_ADDR 3
#define EN_PIN_ADDR 4
#define LASER_PIN_ADDR 5
#define LED_PIN_ADDR 6
#define PULLEY1_ADDR 7
#define PULLEY2_ADDR 8
// 16-bit registers
#define CICLE_ADDR 9
#define STEP_DELAY1_ADDR 11
#define STEP_DELAY2_ADDR 13
// 32-bit registers
#define STEPPER_POSITION_ADDR 15
#define STEPPER_ANGLE_ADDR 19
// 11 bytes of RAM
#define FREE_MEMORY_ADDR 23

byte MEMORY[32];
uint8_t *version = (uint8_t*)(MEMORY + VERSION_ADDR);
uint8_t *config = (uint8_t*)(MEMORY + CONFIG_ADDR);
uint8_t *stpPin = (uint8_t*)(MEMORY + STP_PIN_ADDR);
uint8_t *dirPin = (uint8_t*)(MEMORY + DIR_PIN_ADDR);
uint8_t *enPin = (uint8_t*)(MEMORY + EN_PIN_ADDR);
uint8_t *laserPin = (uint8_t*)(MEMORY + LASER_PIN_ADDR);
uint8_t *ledPin = (uint8_t*)(MEMORY + LED_PIN_ADDR);
uint8_t *pulley1 = (uint8_t*)(MEMORY + PULLEY1_ADDR);
uint8_t *pulley2 = (uint8_t*)(MEMORY + PULLEY2_ADDR);

uint16_t *cicle = (uint16_t*)(MEMORY + CICLE_ADDR);
uint16_t *stepDelay1 = (uint16_t*)(MEMORY + STEP_DELAY1_ADDR);
uint16_t *stepDelay2 = (uint16_t*)(MEMORY + STEP_DELAY2_ADDR);

uint32_t *stepperPosition = (uint32_t*)(MEMORY + STEPPER_POSITION_ADDR);
uint32_t *stepperAngle = (uint32_t*)(MEMORY + STEPPER_ANGLE_ADDR);

#define STEPS_IN_MOTOR_CICLE (*cicle * (1L << ((*config >> CFG_D0) & 0b111)))
#define STEPS_IN_CICLE (STEPS_IN_MOTOR_CICLE * *pulley2 / *pulley1)
#define ANGLE_TO_POS(ANGLE) (STEPS_IN_CICLE * (ANGLE) / 360)


void step(bool dir)
{
  digitalWrite(*dirPin, dir ^ !(*config & _BV(CFG_DIR)));
  digitalWrite(*stpPin, *config & _BV(CFG_STP));
  delayMicroseconds(*stepDelay1);
  digitalWrite(*stpPin, !(*config & _BV(CFG_STP)));
  delayMicroseconds(*stepDelay2);
}

void rotate(int deg) {
  *stepperAngle += deg;

  long target = ANGLE_TO_POS(*stepperAngle);
  long cnt = target - *stepperPosition;
  bool clockwise = cnt >= 0;
  long abscnt = abs(cnt);
  
  for (int i = 0; i < abscnt; i++) {
    step(clockwise);
  }
  
  if (*stepperAngle >= 0)
    *stepperAngle = *stepperAngle % 360;
  else
    *stepperAngle = 360 - (-*stepperAngle - 1) % 360 - 1;

  *stepperPosition += cnt;
  if (*stepperPosition >= 0)
    *stepperPosition = *stepperPosition % STEPS_IN_CICLE;
  else
    *stepperPosition = STEPS_IN_CICLE - (-*stepperPosition - 1) % STEPS_IN_CICLE - 1;
}

void setup() {
  *version = 0x01;
  *config = (1 << CFG_STP) | (1 << CFG_DIR) | (0 << CFG_EN) | (4 << CFG_D0) | (0b00 << 0);
  *stpPin = 2;
  *dirPin = 5;
  *enPin = 8;
  *laserPin = 4; // STP_Z
  *ledPin = 7; // DIR_Z
  *pulley1 = 18;
  *pulley2 = 90;
  
  *cicle = 200;
  *stepDelay1 = 600;
  *stepDelay2 = 600;
  
  *stepperPosition = 0;
  *stepperAngle = 0;
  
  Serial.begin(9600);
}

bool started = false;

void nopCmd() {}
void writeCmd() {
  while (Serial.available() < 2);
  uint8_t addr = Serial.read();
  uint8_t len = Serial.read();
  for (int i = 0; i < len; i++) {
    while (!Serial.available());
    MEMORY[addr + i] = Serial.read();
  }
}
void readCmd() {
  while (Serial.available() < 2);
  uint8_t addr = Serial.read();
  uint8_t len = Serial.read();
  for (int i = 0; i < len; i++) {
    Serial.write(MEMORY[addr + i]);
  }
}
void startCmd() {
  digitalWrite(*stpPin, !(*config & _BV(CFG_STP)));
  digitalWrite(*enPin, *config & _BV(CFG_EN));
  started = true;
}
void stopCmd() {
  digitalWrite(*enPin, !(*config & _BV(CFG_EN)));
  digitalWrite(*stpPin, !(*config & _BV(CFG_STP)));
  started = false;
}
void rotateCmd() {
  while (Serial.available() < 2);
  short angle = Serial.read() << 8 | Serial.read();
  if (started) {
    rotate(angle);
  }
}
void laserCmd() {
  while (Serial.available() < 1);
  uint8_t state = Serial.read();
  digitalWrite(*laserPin, state);
}
void ledCmd() {
  while (Serial.available() < 1);
  uint8_t state = Serial.read();
  digitalWrite(*ledPin, state);
}
void setupCmd() {
  pinMode(*stpPin, OUTPUT);
  pinMode(*dirPin, OUTPUT);
  pinMode(*enPin, OUTPUT);
  pinMode(*laserPin, OUTPUT);
  pinMode(*ledPin, OUTPUT);
  digitalWrite(*stpPin, !(*config & _BV(CFG_STP)));
  digitalWrite(*enPin, !(*config & _BV(CFG_EN)));
  digitalWrite(*laserPin, LOW);
  digitalWrite(*ledPin, LOW);
}

void loop() {
  if (Serial.available() >= 2 && Serial.read() == MSG_START) {
    char cmd = Serial.read();
    switch  (cmd) {
      case NOP_CMD: nopCmd(); break;
      case WRITE_CMD: writeCmd(); break;
      case READ_CMD: readCmd(); break;
      case START_CMD: startCmd(); break;
      case STOP_CMD: stopCmd(); break;
      case ROTATE_CMD: rotateCmd(); break;
      case LASER_CMD: laserCmd(); break;
      case LED_CMD: ledCmd(); break;
      case SETUP_CMD: setupCmd(); break;
      default: break;
    }
    Serial.write(MSG_START);
  }
}
