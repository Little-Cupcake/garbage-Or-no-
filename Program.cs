using System;

namespace siec_neuronowa
{
    class Program
    {
        
        static void Main(string[] args)
        {
             double correct (synapsa synapsa,neuron Out, neuron Hidden_one,int e,bool b,double ror)
            {
                synapsa.delta_bs(1, ror,Out.input); // znaleźliśmy deltę
                                                       // szukamy gradient i poprawkę w wadzę
                synapsa.popr_Hidden(Hidden_one.output, 0.5,0.3, b);
                Console.WriteLine("aktualna (nowa) waga : " + synapsa.waga.ToString());
                return synapsa.waga;
            }

            Random r = new Random();

            synapsa w1 = new synapsa(Convert.ToDouble(r.Next(10)));         //(Convert.ToDouble(r.Next(10)));
            synapsa w2 = new synapsa(Convert.ToDouble(r.Next(10)));
            synapsa w3 = new synapsa(Convert.ToDouble(r.Next(10)));
            synapsa w4 = new synapsa(Convert.ToDouble(r.Next(10)));
            synapsa w5 = new synapsa(Convert.ToDouble(r.Next(10)));
            synapsa w6 = new synapsa(Convert.ToDouble(r.Next(10)));

            neuron I1 = new neuron();
            neuron I2 = new neuron();
            neuron H1 = new neuron();
            neuron H2 = new neuron();
            neuron O1 = new neuron();
            double error = 1;
            for (int e = 0; e < 10 && error !=0 && O1.input != 1; e++)
            {
                Console.WriteLine("Attempt : " + e.ToString() + " ====================================================================");
                for(int s = 0; s < 1; s++)
                {
                    O1.input += H1.Hidden(I1.Start(1, w1.waga), I2.Start(0, w3.waga), w5.waga);
                    O1.input += H2.Hidden(I1.Start(1, w2.waga), I2.Start(0, w4.waga), w6.waga);
                    O1.input = H1.Sigmoid(O1.input);
                    error = Math.Pow(1 - O1.input, 2) / 1;
                    if(O1.input == 1 && error == 0)
                    {
                        Console.WriteLine("Cel został osiągnięty");
                        Console.WriteLine("Result: " + O1.input.ToString());
                        Console.WriteLine("Error equals : " + error.ToString());
                        continue;
                    }
                        
                     Console.WriteLine("Result: " + O1.input.ToString());
                    Console.WriteLine("Error equals : " + error.ToString());
                }
                correct(w5, O1, H1, e, true,error);
                correct(w6, O1, H2, e, true, error);
                correct(w1, H1, I1, e, false, error);
                correct(w2, H2, I1, e, false, error);
                correct(w3, H1, I2, e, false, error);
                correct(w4, H2, I2, e, false, error);
            }
           Console.ReadKey();
        }
    }
}
