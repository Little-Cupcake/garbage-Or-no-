class Dev_tools
    {       
        double previous_weith_change = 0;
        public double Sigmoid(double value)
        {
            double k = Math.Exp(value);
            return k / (1.0f + k);
        }
        public double HyperbolicTangtent(double x)
        {
            if (x < -45.0) return -1.0;
            else if (x > 45.0) return 1.0;
            else return Math.Tanh(x);
        }
        public double ReLU_prime(double x)
        {
            if (x >= 0)
                return 1;
            else
                return 1.0 / 20;
        }
        public Weights[,] weights_Initializing(int layer1, int layer2)
        {
            Weights[,] result = new Weights[layer2, layer1];
            Random r = new Random();
            for (int i = 0; i < layer2; i++)
                for (int j = 0; j < layer1; j++)
                    result[i, j] = new Weights(r.Next(0, 10));
            return result;
        }
        public float[,] normalization(Bitmap InputImage, string color) //нормализация
        {
            float[,] result = new float[InputImage.Width, InputImage.Height];
            for(int i = 0; i < InputImage.Height;i++)
                for(int j = 0; j < InputImage.Width; j++)
                    switch (color) {
                        case "red":
                            result[i, j] = (float)InputImage.GetPixel(i, j).R/255;
                                break;
                        case "green":
                            result[i, j] = (float)InputImage.GetPixel(i, j).G/225;
                            break;
                        case "blue":
                            result[i, j] = (float)InputImage.GetPixel(i, j).B/255;
                            break;
                    }
            return result;
                    
        }
        public float[,] convolution (float[,] InImage,float[,] core,int mW,int mH,int kW,int kH){//поиск признакои        {
            float[,] result = new float[mW - kW + 1, mH - kH + 1];
            int y = 0;
            int x = 0;
            
            for (int h = 0; h < InImage.GetLength(0); h++)
            {
                for (int w = 0; w < InImage.GetLength(1); w++)
                {
                    float[] dm = new float[core.Length];
                    int t = 0;
                    for (int i = 0; i < core.GetLength(0); i++)
                    {
                        for (int j = 0; j < core.GetLength(1); j++) 
                        { if(w <= mW - kW && h <= mH - kH )
                            dm[t++] = (float)InImage[i + h, j + w] * core[i, j];
                        else
                                dm[t++] = (float)InImage[i + mH - kH, j + mW - kW] * core[i, j];
                        }
                    }
                    if(y== mW - kW && x < mH - kH ) 
                    {
                        x++;
                        y = 0;
                        result[x, y] = (float)ReLU_prime((dm.Average()));
                    }else if(x < mH - kH) 
                    {
                        result[x, y] = (float)ReLU_prime((dm.Average()));
                        y++;
                    }

                }

            }
            return result;
        }
        public float[,] max_pooling (float[,] input) // свертывание
        {
            int y = 0;
            int x = 0;
            float[,] result = new float[input.GetLength(0)/2,input.GetLength(1)/2];
            for (int h = 0; h < input.GetLength(0)-1; h++)
            {
                for (int w = 0; w < input.GetLength(1)-1; w+=2)
                {
                    float[] dm = new float[4];
                    int t = 0;
                    for (int i = 0; i < 2; i+=2)
                    {
                        for (int j = 0; j < 2; j++)
                            dm[t++] = input[i + h, j + w];
                    }
                    if (y == input.GetLength(1) / 2 && x < (input.GetLength(0) / 2)-1)
                    {
                        x++;
                        y = 0;
                        result[x, y] = float.Parse(dm.Max().ToString());
                    }
                    else if (x < input.GetLength(0) / 2)
                    {
                        result[x, y] = float.Parse(dm.Max().ToString());
                        if(y != (input.GetLength(1) / 2)-1)
                        y++;
                    }
                }

            }
            return result;

        }
        double gradient;
        public double New_Weight_Generation(double output, double speed, double moment, bool input_syn)
        {        
          gradient = output * 0;
            previous_weith_change = (speed * gradient) + (moment * previous_weith_change);
             return (speed * gradient) + (moment * previous_weith_change);
        }

        public float Finding_delta(float output, float ideal,float input, string function) 
        {
            switch (function) 
            {
                case "sig":
                    return float.Parse(((ideal - output) * Sigmoid(input)).ToString());
                case "tan":
                    return float.Parse(((ideal - output) * HyperbolicTangtent(input)).ToString());
                default:
                    return 0;
            }
        }
        public void first_Fully_connected(float[,] RedMap, float[,] BlueMap, float[,] GreenMap) 
        {
            int x = 0;
            
            float[] result = new float[(RedMap.GetLength(0) * RedMap.GetLength(1))*3];
            for(int c = 0; c<3;c++)
         for(int i =0; i < RedMap.GetLength(0);i++)
                for(int j = 0; j < RedMap.GetLength(1);j++)
                        switch (c) 
                        {
                            case 0:
                                result[x++] =(float)ReLU_prime( RedMap[i, j]);
                                break;
                            case 1:
                                result[x++] = (float)ReLU_prime(BlueMap[i, j]);
                                break;
                            case 2:
                                result[x++] = (float)ReLU_prime(GreenMap[i, j]);
                                break;
                        }
            
        }
       
        }
    }
