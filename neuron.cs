using System;
using System.Collections.Generic;
using System.Text;


namespace siec_neuronowa
{
    class neuron
    {
        public double Sigmoid(double value)
        {
            double k = Math.Exp(value);
            return k / (1.0f + k);
        }
        public double input = 0;
        public double output = 0;

        public double Hidden(double d1, double d2, double waga)
        {
            input = d1 + d2;
            output = Sigmoid(input) * waga;
            return output ;
        }
        
        public double Start (double d1, double waga)
        {
            output = d1 * waga;
            return output; 
        }
    }
}
