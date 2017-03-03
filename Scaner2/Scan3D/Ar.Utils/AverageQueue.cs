using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Utils
{
    public class AverageQueue
    {
        private double Sum = 0;
        private int Index = 0;
        private double[] Values;
        public double Value
        {
            get { return Sum / Values.Length; }
            set
            {
                Index = 0;
                Sum = value * Values.Length;
                for (int i = 0; i < Values.Length; i++)
                {
                    Values[i] = value;
                }
            }
        }
        public void Enqueue(double value)
        {
            Sum -= Values[Index];
            Sum += value;
            Values[Index] = value;
            Index = (Index + 1) % Values.Length;
        }
        public AverageQueue(int capacity, double initialValue = 0)
        {
            Values = new double[capacity];
            Value = initialValue;
        }
    }
}
