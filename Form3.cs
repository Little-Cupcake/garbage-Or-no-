using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unicorn
{
    public partial class Form3 : Form
    {
        Form2 f2 = new Form2();
     public   int row;
        public Form3()
        {
            InitializeComponent();
            Form2 main = this.Owner as Form2;
            if (main != null)
            {
                int row = main.r;
            }

        }
        private void Form3_Load(object sender, EventArgs e)
        {
            

            this.grupaTableAdapter.Fill(this.data.grupa);
            this.salaTableAdapter.Fill(this.data.Sala);
            this.nauczycielTableAdapter.Fill(this.data.nauczyciel);
            this.przedmiotTableAdapter.Fill(this.data.przedmiot);
            this.planTableAdapter.Fill(this.data.plan);
            DataRow t = data.grupa.FindById(Convert.ToInt32(data.przedmiot.Rows[row][3]));
            nazwaComboBox.SelectedIndex = data.grupa.Rows.IndexOf(t);
            textBox1.Text = data.przedmiot.Rows[row][1].ToString();
            if(!data.przedmiot.Rows[row][4].Equals(Convert.DBNull))
            {
                DataRow a = data.nauczyciel.FindById(Convert.ToInt32(data.przedmiot.Rows[row][4]));
                imie_i_nazwiskoComboBox.SelectedIndex = data.nauczyciel.Rows.IndexOf(a);

            }else
            imie_i_nazwiskoComboBox.SelectedItem = null;
            if (!data.przedmiot.Rows[row][5].Equals(Convert.DBNull))
            {
                DataRow a = data.Sala.FindById(Convert.ToInt32(data.przedmiot.Rows[row][5]));
                numerComboBox.SelectedIndex = data.Sala.Rows.IndexOf(a);

            }
            else
                numerComboBox.SelectedItem = null;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow nienawiedzę_was = data.przedmiot.Rows[row];
            nienawiedzę_was.BeginEdit();
            if (textBox1.Text.Length != 0)
                nienawiedzę_was.SetField(1,textBox1.Text);
            if(nazwaComboBox.SelectedItem != null)
            nienawiedzę_was.SetField(3, Convert.ToInt32(data.grupa.Rows[nazwaComboBox.SelectedIndex][0]));
            if(imie_i_nazwiskoComboBox.SelectedItem != null)
            nienawiedzę_was.SetField(4, Convert.ToInt32(data.nauczyciel.Rows[imie_i_nazwiskoComboBox.SelectedIndex][0]));
            if (numerComboBox.SelectedItem != null)
                nienawiedzę_was.SetField(5, Convert.ToInt32(data.Sala.Rows[numerComboBox.SelectedIndex][0]));
            nienawiedzę_was.EndEdit();
            przedmiotTableAdapter.Update(data.przedmiot);
            data.AcceptChanges();
            DataRow[] cos_adekwatnego = data.plan.Select("[fk przedmiotu] = " + nienawiedzę_was[0].ToString());
            for(int i = 0; i < cos_adekwatnego.Length; i++)
            { cos_adekwatnego[i].BeginEdit();
                if(!nienawiedzę_was[5].Equals(Convert.DBNull))
                cos_adekwatnego[i].SetField(2,Convert.ToInt32(nienawiedzę_was[5]));
                if(!nienawiedzę_was[4].Equals(Convert.DBNull))
                cos_adekwatnego[i].SetField(3,Convert.ToInt32(nienawiedzę_was[4]));
                cos_adekwatnego[i].SetField(6, Convert.ToInt32(nienawiedzę_was[3]));
                cos_adekwatnego[i].EndEdit();
                planTableAdapter.Update(data.plan);
                data.AcceptChanges();
                if(numerComboBox.SelectedItem.Equals(null))
            numerComboBox.SelectedItem = null;
                if (imie_i_nazwiskoComboBox.SelectedItem.Equals(null))
                    imie_i_nazwiskoComboBox.SelectedItem = null;
                

            }
            if (numerComboBox.SelectedItem.Equals(null))
                numerComboBox.SelectedItem = null;
            if (imie_i_nazwiskoComboBox.SelectedItem.Equals(null))
                imie_i_nazwiskoComboBox.SelectedItem = null;
            MessageBox.Show("Operacja przebiegła pomyślnie");
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            f2.Show();

           
           
        }

        private void Form3_Shown(object sender, EventArgs e)
        {
           
        }
    }
}
