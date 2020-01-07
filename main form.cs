 public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)//wybierz obraz
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        pictureBox1.Load(filePath);     
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(pictureBox1.Image);


            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dev_tools tool = new Dev_tools();
            Core ctool = new Core();
            Core[] cores = { new Core(), new Core(), new Core(), new Core()};
            Neuron[] wejście = new Neuron[150];
            for (int i = 0; i < 4; i++)            
                cores[i].CoreInitializing(5,5);           
                
            for (int i = 0; i < 150; i++)
                wejście[i] = new Neuron();         
        }
    }
    //float[,] dev = tool.normalization(b, "blue");
           // 
              //  MessageBox.Show(dev[q, 4].ToString());
              
           // var r = tool.max_pooling(tool.convolution(dev, core.CoreInitialization(5, 5), dev.GetLength(1), dev.GetLength(0), 5, 5));
            //for (int q = 0; q < 2; q++)
           // r =   tool.max_pooling(r);

           // for (int q = 0; q < r.GetLength(0); q++)
              //  for (int q1 = 0; q1 < r.GetLength(1); q1++)
                //    MessageBox.Show(r[q, q1].ToString() + "\n q="+q+ "q1=" +q1);//
