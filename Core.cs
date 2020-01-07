class Core
    {
       public float[,] core;
        public void  CoreInitializing(int x,int y) 
        {
            
            Random r = new Random();
            float[,] result = new float[x, y];
            for(int i = 0; i<x;i++)
                for(int j =0; j < y; j++) 
                {
                    double t = r.NextDouble();
                    if (t > 0.5)
                        result[i, j] = float.Parse((0.5 - t).ToString());
                    else
                        result[i, j] = float.Parse(t.ToString());
                }
            core = result;
            //return result;
            
        }
    }
