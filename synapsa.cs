using System;
using System.Collections.Generic;
using System.Text;

namespace siec_neuronowa
{
    class synapsa
    {
        neuron sig = new neuron();

        public double waga = 0;
        private double gradient = 0;
        double previous_weith_change = 0;
        private double del = 0;
        public synapsa(double wag)
        {
            waga = wag;
        }

        public void delta_bs(double ideal_out, double actual_out, double inp)
        {
           del += ((ideal_out - actual_out) * sig.Sigmoid(inp));
           // Console.WriteLine("delta = " + del.ToString());
        }
        public void popr_Hidden (double output, double speed, double moment,bool input_syn)
        {
            if (input_syn)
                gradient = output * (double)del;// delta = gradient's start point
            else
                gradient = output * 0;
           // Console.WriteLine("gradient = " + gradient.ToString());
            waga += (speed * gradient) + (moment * previous_weith_change);
            previous_weith_change += (speed * gradient) + (moment * previous_weith_change);
           // Console.WriteLine("previous change = " + previous_weith_change.ToString());

        }

    }
}
