using Scan3D.Ar.Utils;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D
{
    public class DeviceController
    {
        private SerialPort Serial;
        private const byte Magic = (byte)'$';

        private const byte NopCmd = 0;
        private const byte WriteCmd = 1;
        private const byte ReadCmd = 2;
        private const byte StartCmd = 3;
        private const byte StopCmd = 4;
        private const byte RotateCmd = 5;
        private const byte LaserCmd = 6;
        private const byte LedCmd = 7;
        private const byte SetupCmd = 8;

        private const int MemorySize = 32;

        private const int VersionAddr = 0;
        private const int ConfigAddr = 1;
        private const int StpPinAddr = 2;
        private const int DirPinAddr = 3;
        private const int EnPinAddr = 4;
        private const int LaserPinAddr = 5;
        private const int LedPinAddr = 6;
        private const int Pulley1Addr = 7;
        private const int Pulley2Addr = 8;
        private const int CicleAddr = 9;
        private const int StepDelay1Addr = 11;
        private const int StepDelay2Addr = 13;
        private const int StepperPositionAddr = 15;
        private const int StepperAngleAddr = 19;
        private const int FreeMemoryAddr = 23;

        private async Task ReadSerialAsync(byte[] bytes, int offset, int count)
        {
            int remainder = count;
            while (remainder > 0)
                remainder -= await Serial.BaseStream.ReadAsync(bytes, offset + count - remainder, remainder);
        }
        private async Task WriteSerialAsync(byte[] bytes, int offset, int count)
        {
            await Serial.BaseStream.WriteAsync(bytes, offset, count);
        }

        private async Task ReceiveAcknowledgment()
        {
            byte[] bytes = new byte[1];
            await ReadSerialAsync(bytes, 0, 1);
        }

        #region Memory access
        private async Task ReadAsync(int addr, byte[] bytes, int offset, int count)
        {
            Serial.BaseStream.WriteByte(Magic);
            Serial.BaseStream.WriteByte(ReadCmd);
            Serial.BaseStream.WriteByte((byte)addr);
            Serial.BaseStream.WriteByte((byte)count);
            await ReadSerialAsync(bytes, offset, count);
            await ReceiveAcknowledgment();
        }

        private async Task WriteAsync(int addr, byte[] bytes, int offset, int count)
        {
            Serial.BaseStream.WriteByte(Magic);
            Serial.BaseStream.WriteByte(WriteCmd);
            Serial.BaseStream.WriteByte((byte)addr);
            Serial.BaseStream.WriteByte((byte)count);
            await WriteSerialAsync(bytes, offset, count);
            await ReceiveAcknowledgment();
        }

        private async Task<byte> GetReg8(int addr)
        {
            byte[] bytes = new byte[1];
            await ReadAsync(addr, bytes, 0, bytes.Length);
            return bytes[0];
        }

        private async Task SetReg8(int addr, byte value)
        {
            byte[] bytes = new byte[1] { value };
            await WriteAsync(addr, bytes, 0, bytes.Length);
        }

        private async Task<short> GetReg16(int addr)
        {
            byte[] bytes = new byte[2];
            await ReadAsync(addr, bytes, 0, bytes.Length);
            return MyBitConverter.LEToInt16(bytes, 0);
        }

        private async Task SetReg16(int addr, short value)
        {
            byte[] bytes = MyBitConverter.GetBytesLE(value);
            await WriteAsync(addr, bytes, 0, bytes.Length);
        }

        private async Task<int> GetReg32(int addr)
        {
            byte[] bytes = new byte[4];
            await ReadAsync(addr, bytes, 0, bytes.Length);
            return MyBitConverter.LEToInt32(bytes, 0);
        }

        private async Task SetReg32(int addr, int value)
        {
            byte[] bytes = MyBitConverter.GetBytesLE(value);
            await WriteAsync(addr, bytes, 0, bytes.Length);
        }
        #endregion

        #region Register Getters/Setters
        public Task<byte> GetVersionAsync() => GetReg8(VersionAddr);
        public Task SetVersionAsync(byte value) => SetReg8(VersionAddr, value);

        public Task<byte> GetConfigAsync() => GetReg8(ConfigAddr);
        public Task SetConfigAsync(byte value) => SetReg8(ConfigAddr, value);

        public Task<byte> GetStpPinAsync() => GetReg8(StpPinAddr);
        public Task SetStpPinAsync(byte value) => SetReg8(StpPinAddr, value);

        public Task<byte> GetDirPinAsync() => GetReg8(DirPinAddr);
        public Task SetDirPinAsync(byte value) => SetReg8(DirPinAddr, value);

        public Task<byte> GetEnPinAsync() => GetReg8(EnPinAddr);
        public Task SetEnPinAsync(byte value) => SetReg8(EnPinAddr, value);

        public Task<byte> GetLaserPinAsync() => GetReg8(LaserPinAddr);
        public Task SetLaserPinAsync(byte value) => SetReg8(LaserPinAddr, value);

        public Task<byte> GetLedPinAsync() => GetReg8(LedPinAddr);
        public Task SetLedPinAsync(byte value) => SetReg8(LedPinAddr, value);

        public Task<byte> GetPulley1Async() => GetReg8(Pulley1Addr);
        public Task SetPulley1Async(byte value) => SetReg8(Pulley1Addr, value);

        public Task<byte> GetPulley2Async() => GetReg8(Pulley2Addr);
        public Task SetPulley2Async(byte value) => SetReg8(Pulley2Addr, value);

        public Task<short> GetCicleAsync() => GetReg16(CicleAddr);
        public Task SetCicleAsync(short value) => SetReg16(CicleAddr, value);

        public Task<short> GetStepDelay1Async() => GetReg16(StepDelay1Addr);
        public Task SetStepDelay1Async(short value) => SetReg16(StepDelay1Addr, value);

        public Task<short> GetStepDelay2Async() => GetReg16(StepDelay2Addr);
        public Task SetStepDelay2Async(short value) => SetReg16(StepDelay2Addr, value);

        public Task<int> GetStepperPositionAsync() => GetReg32(StepperPositionAddr);
        public Task SetStepperPositionAsync(int value) => SetReg32(StepperPositionAddr, value);

        public Task<int> GetStepperAngleAsync() => GetReg32(StepperAngleAddr);
        public Task SetStepperAngleAsync(int value) => SetReg32(StepperAngleAddr, value);
        #endregion

        public double DeviceAngle => GetStepperAngleAsync().Result * Math.PI / 180;

        public double Angle { get; private set; } = 0;

        public bool IsBusy { get; private set; } = false;

        public DeviceController(SerialPort port)
        {
            Serial = port;
            Serial.Open();
        }

        public async Task<int> GetStepsPerDegree()
        {
            var circle = await GetCicleAsync();
            var config = await GetConfigAsync();
            var p1 = await GetPulley1Async();
            var p2 = await GetPulley2Async();
            return circle * (1 << (config >> 2 & 0x07)) * p2 / p1 / 360;
        }

        public async Task Setup()
        {
            if (IsBusy) throw new InvalidOperationException();
            IsBusy = true;
            byte[] packet = new byte[2];
            packet[0] = Magic;
            packet[1] = SetupCmd;
            await WriteSerialAsync(packet, 0, packet.Length);
            await ReceiveAcknowledgment();
            IsBusy = false;
        }

        public async Task Start()
        {
            if (IsBusy) throw new InvalidOperationException();
            IsBusy = true;
            byte[] packet = new byte[2];
            packet[0] = Magic;
            packet[1] = StartCmd;
            await WriteSerialAsync(packet, 0, packet.Length);
            await ReceiveAcknowledgment();
            IsBusy = false;
        }

        public async Task Stop()
        {
            if (IsBusy) throw new InvalidOperationException();
            IsBusy = true;
            byte[] packet = new byte[2];
            packet[0] = Magic;
            packet[1] = StopCmd;
            await WriteSerialAsync(packet, 0, packet.Length);
            await ReceiveAcknowledgment();
            IsBusy = false;
        }

        public async Task SetLaserState(bool state)
        {
            if (IsBusy) throw new InvalidOperationException();
            IsBusy = true;
            byte[] packet = new byte[3];
            packet[0] = Magic;
            packet[1] = LaserCmd;
            packet[2] = (byte)(state ? 1 : 0);
            await WriteSerialAsync(packet, 0, packet.Length);
            await ReceiveAcknowledgment();
            IsBusy = false;
        }

        public async Task SetLedState(bool state)
        {
            if (IsBusy) throw new InvalidOperationException();
            IsBusy = true;
            byte[] packet = new byte[3];
            packet[0] = Magic;
            packet[1] = LedCmd;
            packet[2] = (byte)(state ? 1 : 0);
            await WriteSerialAsync(packet, 0, packet.Length);
            await ReceiveAcknowledgment();
            IsBusy = false;
        }

        public async Task Rotate(double angle)
        {
            if (IsBusy) throw new InvalidOperationException();
            IsBusy = true;
            short angleShort = (short)(angle / Math.PI * 180);
            byte[] packet = new byte[4];
            packet[0] = Magic;
            packet[1] = RotateCmd;
            MyBitConverter.GetBytesBE(angleShort).CopyTo(packet, 2);
            await WriteSerialAsync(packet, 0, packet.Length);
            await ReceiveAcknowledgment();
            Angle += angle;
            IsBusy = false;
        }

        public void Dispose()
        {
            Serial.Close();
            Serial = null;
        }
    }
}
