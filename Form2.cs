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
    public partial class Form2 : Form
    {        Form1 f = new Form1();
        public int r;
        bool b = false;
        public void f1()
        {
         
            grupaTableAdapter.Update(data.grupa);
            this.salaTableAdapter.Update(this.data.Sala);
            this.nauczycielTableAdapter.Update(this.data.nauczyciel);
            this.przedmiotTableAdapter.Update(this.data.przedmiot);
            this.planTableAdapter.Update(this.data.plan);

            grupaDataGridView.Refresh();
            nauczycielDataGridView.Refresh();
            salaDataGridView.Refresh();
            dataGridView1.Refresh();

            data.AcceptChanges();
            if (data.przedmiot.Rows.Count != 0)
            {
                for (int i = 0; i < data.przedmiot.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    for (int j = 0; j < 5; j++)
                    {
                        switch (j)
                        {
                            case 0:
                                dataGridView1[j, i].Value = data.przedmiot.Rows[i][j + 1];
                                break;
                            case 1:
                                dataGridView1[j, i].Value = data.przedmiot.Rows[i][j + 1];
                                break;
                            case 2:
                                dataGridView1[j, i].Value = data.grupa.FindById(Convert.ToInt32(data.przedmiot.Rows[i][j + 1]))[1].ToString();
                                break;
                            case 3:
                                if (!data.przedmiot.Rows[i][j + 1].Equals(Convert.DBNull))
                                    dataGridView1[j, i].Value = data.nauczyciel.FindById(Convert.ToInt32(data.przedmiot.Rows[i][j + 1]))[1].ToString();
                                else
                                    dataGridView1[j, i].Value = "";
                                break;
                            case 4:
                                if (!data.przedmiot.Rows[i][j + 1].Equals(Convert.DBNull))
                                    dataGridView1[j, i].Value = data.Sala.FindById(Convert.ToInt32(data.przedmiot.Rows[i][j + 1]))[1].ToString();
                                else
                                    dataGridView1[j, i].Value = "";
                                break;

                        }
                    }
                }
            }
        }


        public Form2()
        {
            InitializeComponent();
            grupaDataGridView.MultiSelect = false;
            nauczycielDataGridView.MultiSelect = false;
            salaDataGridView.MultiSelect = false;
            dataGridView1.MultiSelect = false;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            

            f.Hide();
           
            
            this.salaTableAdapter.Fill(this.data.Sala);
            this.nauczycielTableAdapter.Fill(this.data.nauczyciel);
            this.grupaTableAdapter.Fill(this.data.grupa);
            this.przedmiotTableAdapter.Fill(this.data.przedmiot);
            tableTableAdapter.Fill(data.Table);
            this.planTableAdapter.Fill(this.data.plan);
            this.klasa_timeTableAdapter.Fill(this.data.klasa_time);
            this.room_timeTableAdapter.Fill(this.data.room_time);
            this.teacher_timeTableAdapter.Fill(this.data.teacher_time);
            if(data.przedmiot.Count != 0)
            {
                for (int i = 0; i < data.przedmiot.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    for (int j = 0; j < 5; j++)
                    {
                        switch (j)
                        {
                            case 0:
                                dataGridView1[j, i].Value = data.przedmiot.Rows[i][j + 1];
                                break;
                            case 1:
                                dataGridView1[j, i].Value = data.przedmiot.Rows[i][j + 1];
                                break;
                            case 2:
                                dataGridView1[j, i].Value = data.grupa.FindById(Convert.ToInt32(data.przedmiot.Rows[i][j + 1]))[1].ToString();
                                break;
                            case 3:
                                if (!data.przedmiot.Rows[i][j + 1].Equals(Convert.DBNull))
                                    dataGridView1[j, i].Value = data.nauczyciel.FindById(Convert.ToInt32(data.przedmiot.Rows[i][j + 1]))[1].ToString();
                                else
                                    dataGridView1[j, i].Value = "";
                                break;
                            case 4:
                                if (!data.przedmiot.Rows[i][j + 1].Equals(Convert.DBNull))
                                    dataGridView1[j, i].Value = data.Sala.FindById(Convert.ToInt32(data.przedmiot.Rows[i][j + 1]))[1].ToString();
                                else
                                    dataGridView1[j, i].Value = "";
                                break;

                        }
                    }
                }
            }


        }

        private void grupa_Click(object sender, EventArgs e)
        {
            
            if(textBox1.Text.Length != 0 )
            {
                DataRow row = data.grupa.NewRow();
                row[0] = Convert.ToInt32(data.Table.Rows[0][1]) + 1;
                data.Table.Rows[0][1] = Convert.ToInt32(data.Table.Rows[0][1]) + 1;
                tableTableAdapter.Update(data.Table);
                row[1] = textBox1.Text;
                data.grupa.Rows.Add(row);
                grupaTableAdapter.Update(data.grupa);
                data.AcceptChanges();
                MessageBox.Show("klasa dodana");
                textBox1.Clear();
            }

        }

        private void naucz_Click(object sender, EventArgs e)
        {
            if(textBox2.Text.Length != 0)
            {
                DataRow row = data.nauczyciel.NewRow();
                row[0] = Convert.ToInt32(data.Table.Rows[0][2]) + 1;
                data.Table.Rows[0][2] = Convert.ToInt32(data.Table.Rows[0][2]) + 1;
                tableTableAdapter.Update(data.Table);
                row[1] = textBox2.Text;
                row[2] = Convert.DBNull;
                data.nauczyciel.Rows.Add(row);
                nauczycielTableAdapter.Update(data.nauczyciel);
                data.AcceptChanges();
                textBox2.Clear();
                MessageBox.Show("nauczyciel dodany");
            }
        }

        private void sala_Click(object sender, EventArgs e)
        {
            if(textBox4.Text.Length != 0)
            {
                DataRow row = data.Sala.NewRow();
                row[0] = Convert.ToInt32(data.Table.Rows[0][5]) + 1;
                data.Table.Rows[0][5] = Convert.ToInt32(data.Table.Rows[0][5]) + 1;
                tableTableAdapter.Update(data.Table);
                row[1] = textBox4.Text;
                row[2] = textBox3.Text;
                data.Sala.Rows.Add(row);
                salaTableAdapter.Update(data.Sala);
                data.AcceptChanges();
                textBox4.Clear();
                textBox3.Clear();
                MessageBox.Show("sala dodana");

            }
        }

        private void przedm_Click(object sender, EventArgs e)
        {
                if(textBox5.Text.Length != 0 && nazwaComboBox.SelectedItem != null )
                {
                 DataRow row = data.przedmiot.NewRow();
                 row[0] = Convert.ToInt32(data.Table.Rows[0][4]) + 1;
                 data.Table.Rows[0][4] = Convert.ToInt32(data.Table.Rows[0][4]) + 1;
                tableTableAdapter.Update(data.Table);
                 row[1] = textBox5.Text;
                 row[2] = numericUpDown1.Value;
                 row[3] = Convert.ToInt32(data.grupa.Rows[nazwaComboBox.SelectedIndex][0]);
                 row[4] = Convert.DBNull; ;
                 row[5] = Convert.DBNull; ;
                 data.przedmiot.Rows.Add(row);
                 przedmiotTableAdapter.Update(data.przedmiot);
                 data.AcceptChanges();
                for (int i = 0; i < Convert.ToInt32(data.przedmiot.AsEnumerable().Last()[2]); i++)
                {
                    DataRow row1 = data.plan.NewRow();
                    row1[0] = Convert.ToInt32(data.Table.AsEnumerable().Last()[3]) + 1;
                    data.Table.Rows[0][3] = Convert.ToInt32(data.Table.Rows[0][3]) + 1;
                    tableTableAdapter.Update(data.Table);
                    row1[1] = Convert.ToInt32(data.przedmiot.AsEnumerable().Last()[0]);
                    row1[2] = Convert.DBNull; ;
                    row1[3] = Convert.DBNull; ;
                    row1[4] = Convert.DBNull;
                    row1[5] = Convert.DBNull;
                    row1[6] = Convert.ToInt32(data.przedmiot.AsEnumerable().Last()[3]);
                    data.plan.Rows.Add(row1);
                    planTableAdapter.Update(data.plan);

                }
                 data.AcceptChanges();
                 MessageBox.Show("przedmiot dodany");
                 textBox5.Clear();
            }
            dataGridView1.Rows.Clear();
            for (int i = 0; i < data.przedmiot.Count;i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < 5;j++)
                {
                    switch (j)
                    {
                        case 0:
                            dataGridView1[j, i].Value = data.przedmiot.Rows[i][j + 1];
                            break;
                        case 1:
                            dataGridView1[j, i].Value = data.przedmiot.Rows[i][j + 1];
                            break;
                        case 2:
                            dataGridView1[j, i].Value = data.grupa.FindById(Convert.ToInt32( data.przedmiot.Rows[i][j + 1]))[1].ToString();
                            break;
                        case 3:
                            if (!data.przedmiot.Rows[i][j + 1].Equals(Convert.DBNull))
                                dataGridView1[j, i].Value = data.nauczyciel.FindById(Convert.ToInt32(data.przedmiot.Rows[i][j + 1]))[1].ToString();
                            else
                                dataGridView1[j, i].Value = "";
                            break;
                        case 4:
                            if (!data.przedmiot.Rows[i][j + 1].Equals(Convert.DBNull))
                                dataGridView1[j, i].Value = data.Sala.FindById(Convert.ToInt32(data.przedmiot.Rows[i][j + 1]))[1].ToString();
                            else
                                dataGridView1[j, i].Value = "";
                            break;

                    }
                }
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!b)
            {
                f.Show();

                
            } else
              b = false;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
         var result =  MessageBox.Show("czy napewno chcesz usunąć klasę i wszystkie informację z nią związane?","Uwaga",MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                
             int del = grupaDataGridView.CurrentRow.Index;
                DataRow[] przedm = data.przedmiot.Select("[fk klasy] = " + data.grupa.Rows[del][0].ToString());
                DataRow[] less = data.plan.Select("[grupa fk] = " + data.grupa.Rows[del][0].ToString());  
               for(int i = 0; less.Length > i;i++ )
                {
                 if (less[i][4] != Convert.DBNull && less[i][5] != Convert.DBNull)
                    {
                    DataRow[] row3 = data.klasa_time.Select("grupa = " + data.grupa.Rows[del][0].ToString());
                   for (int q = 0; q < row3.Length; q++)
                   {
                        
                            if (row3[q][2].Equals(less[i][4]) && row3[q][3].Equals(less[i][5]))
                            {
                                data.klasa_time.Rows[data.klasa_time.Rows.IndexOf(row3[q])].Delete();
                                klasa_timeTableAdapter.Update(data.klasa_time);
                                data.AcceptChanges();
                            }
                   }
                        DataRow[] row = data.teacher_time.Select("teacher = " + less[i][3].ToString());
                        if (row.Any())
                            for (int q = 0; q < row.Length; q++)
                            {
                                if (row[q][2].Equals(less[i][4]) && row[q][3].Equals(less[i][5]))
                                {
                                    data.teacher_time.Rows[data.teacher_time.Rows.IndexOf(row[q])].Delete();
                                    teacher_timeTableAdapter.Update(data.teacher_time);
                                    data.AcceptChanges();
                                    
                                }
                            }
                        DataRow[] row2 = data.room_time.Select("room = " + less[i][2].ToString());
                        if (row2.Any())
                            for (int q = 0; q < row2.Length; q++)
                            {
                                if (row2[q][2].Equals(less[i][4]) && row2[q][3].Equals(less[i][5]))
                                {
                                    data.room_time.Rows[data.room_time.Rows.IndexOf(row2[q])].Delete();
                                    room_timeTableAdapter.Update(data.room_time);
                                    data.AcceptChanges();
                                    
                                }
                            }
                    }
                    planTableAdapter.DeleteRowQ(Convert.ToInt32(less[i][0]));
                    planTableAdapter.Update(data.plan);
                data.AcceptChanges();
               }
                
                for(int i = 0; przedm.Length > i; i++ )
                {
                    przedmiotTableAdapter.DeleteRowQ(Convert.ToInt32(przedm[i][0]));                       // data.przedmiot.Rows[przedm[0]].Delete();
                    przedmiotTableAdapter.Update(data.przedmiot);
                    data.AcceptChanges();
                    

                }
                


                // grupaTableAdapter.DeleteRowQ(Convert.ToInt32(data.grupa.Rows[del][0]));
                data.grupa.Rows[del].Delete();
                grupaTableAdapter.Update(data.grupa);
                data.grupa.AcceptChanges();
                data.AcceptChanges();
               dataGridView1.Rows.Clear();
                f1();
                
              
                
                
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("czy napewno chcesz usunąć nauczyciela i wszystkie informację z nim związane?", "Uwaga", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int del = nauczycielDataGridView.CurrentRow.Index;
                DataRow[] przedm = data.przedmiot.Select("[fk naucz_] = " + data.nauczyciel.Rows[del][0].ToString());
                DataRow[] less = data.plan.Select("[fk naucz_] = " + data.nauczyciel.Rows[del][0].ToString());

                for (int i = 0; less.Length > i; i++)
                {
                    if (less[i][4] != Convert.DBNull && less[i][5] != Convert.DBNull)
                    {
                        DataRow[] row3 = data.klasa_time.Select("grupa = " + data.grupa.Rows[del][0].ToString());
                        for (int q = 0; q < row3.Length; q++)
                        {

                            if (row3[q][2].Equals(less[i][4]) && row3[q][3].Equals(less[i][5]))
                            {
                                data.klasa_time.Rows[data.klasa_time.Rows.IndexOf(row3[q])].Delete();
                                klasa_timeTableAdapter.Update(data.klasa_time);
                                data.AcceptChanges();
                            }
                        }
                        DataRow[] row = data.teacher_time.Select("teacher = " + less[i][3].ToString());
                        if (row.Any())
                            for (int q = 0; q < row.Length; q++)
                            {
                                if (row[q][2].Equals(less[i][4]) && row[q][3].Equals(less[i][5]))
                                {
                                    data.teacher_time.Rows[data.teacher_time.Rows.IndexOf(row[q])].Delete();
                                    teacher_timeTableAdapter.Update(data.teacher_time);
                                    data.AcceptChanges();

                                }
                            }
                        DataRow[] row2 = data.room_time.Select("room = " + less[i][2].ToString());
                        if (row2.Any())
                            for (int q = 0; q < row2.Length; q++)
                            {
                                if (row2[q][2].Equals(less[i][4]) && row2[q][3].Equals(less[i][5]))
                                {
                                    data.room_time.Rows[data.room_time.Rows.IndexOf(row2[q])].Delete();
                                    room_timeTableAdapter.Update(data.room_time);
                                    data.AcceptChanges();

                                }
                            }
                    }
                    planTableAdapter.DeleteRowQ(Convert.ToInt32(less[i][0]));
                    planTableAdapter.Update(data.plan);
                    data.AcceptChanges();
                }

                for (int i = 0; przedm.Length > i; i++)
                {
                    przedmiotTableAdapter.DeleteRowQ(Convert.ToInt32(przedm[i][0]));                       // data.przedmiot.Rows[przedm[0]].Delete();
                    przedmiotTableAdapter.Update(data.przedmiot);
                    data.AcceptChanges();


                }  
                //nauczycielTableAdapter.DeleteRowQ(Convert.ToInt32(data.nauczyciel.Rows[del][0]));
                data.nauczyciel.Rows[del].Delete();
                nauczycielTableAdapter.Update(data.nauczyciel);
                nauczycielDataGridView.ClearSelection();
                data.AcceptChanges();
                dataGridView1.Rows.Clear();
                f1();

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("czy napewno chcesz usunąć salę i wszystkie informację z nią związane?", "Uwaga", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int del = salaDataGridView.CurrentRow.Index;
                DataRow[] przedm = data.przedmiot.Select("[fk sala] = " + data.Sala.Rows[del][0].ToString());
                DataRow[] less = data.plan.Select("[fk sali] = " + data.Sala.Rows[del][0].ToString());

                for (int i = 0; less.Length > i; i++)
                {
                    planTableAdapter.DeleteRowQ(Convert.ToInt32(less[i][0]));
                    planTableAdapter.Update(data.plan);
                    data.AcceptChanges();
                }

                for (int i = 0; przedm.Length > i; i++)
                {
                    przedmiotTableAdapter.DeleteRowQ(Convert.ToInt32(przedm[i][0]));                       // data.przedmiot.Rows[przedm[0]].Delete();
                    przedmiotTableAdapter.Update(data.przedmiot);
                    data.AcceptChanges();


                }
                DataRow[] row2 = data.room_time.Select("room = " + data.Sala.Rows[del][0].ToString());
                for (int q = 0; q < row2.Length; q++)
                {
                    data.room_time.Rows.Remove(row2[q]);
                    room_timeTableAdapter.Update(data.room_time);
                    data.AcceptChanges();
                }
                // salaTableAdapter.DeleteRowQ(Convert.ToInt32(data.Sala.Rows[del][0]));
                data.Sala.Rows[del].Delete();
                salaTableAdapter.Update(data.Sala);
                salaDataGridView.ClearSelection();
                data.AcceptChanges();
                dataGridView1.Rows.Clear();
                f1();

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("czy napewno chcesz usunąć przedmiot i wszystkie informację z nim związane?", "Uwaga", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int del = dataGridView1.CurrentRow.Index;
                DataRow[] less = data.plan.Select("[fk przedmiotu] = " + data.przedmiot.Rows[del][0].ToString());

                for (int i = 0; less.Length > i; i++)
                {
                    if (less[i][4] != Convert.DBNull && less[i][5] != Convert.DBNull)
                    { 
                    DataRow[] row = data.teacher_time.Select("teacher = " + less[i][3].ToString());
                        if (row.Any())
                        for (int q = 0; q < row.Length; q++)
                        {
                            if (row[q][2].Equals(less[i][4]) && row[q][3].Equals(less[i][5]) )
                            {
                                data.teacher_time.Rows[data.teacher_time.Rows.IndexOf(row[q])].Delete();
                                teacher_timeTableAdapter.Update(data.teacher_time);
                                data.AcceptChanges();
                                
                            }
                        }
                    DataRow[] row2 = data.room_time.Select("room = " + less[i][2].ToString());
                        if(row2.Any())
                    for (int q = 0; q < row2.Length; q++)
                    {
                            if (row2[q][2].Equals(less[i][4]) && row2[q][3].Equals(less[i][5]))
                            {
                                data.room_time.Rows[data.room_time.Rows.IndexOf(row2[q])].Delete();
                                room_timeTableAdapter.Update(data.room_time);
                                data.AcceptChanges();
                                
                            }
                    }
                    DataRow[] row3 = data.klasa_time.Select("grupa = " + less[i][6].ToString());
                        if(row.Any())
                    for (int q = 0; q < row3.Length; q++)
                    {
                            if (row3[q][2].Equals(less[i][4]) && row3[q][3].Equals(less[i][5]))
                            {
                                
                                data.klasa_time.Rows[data.klasa_time.Rows.IndexOf(row3[q])].Delete();
                                klasa_timeTableAdapter.Update(data.klasa_time);
                                data.AcceptChanges();
                                break;
                            }
                    }
                    }

                    planTableAdapter.DeleteRowQ(Convert.ToInt32(less[i][0]));
                    planTableAdapter.Update(data.plan);
                    data.AcceptChanges();
                }
               // przedmiotTableAdapter.DeleteRowQ(Convert.ToInt32(data.przedmiot.Rows[del][0]));
                data.przedmiot.Rows[del].Delete();
                przedmiotTableAdapter.Update(data.przedmiot);
                dataGridView1.ClearSelection();
                data.AcceptChanges();
                dataGridView1.Rows.Clear();
                f1();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
              r = dataGridView1.CurrentRow.Index;
              b = true;
              Form3 f = new Form3();
              f.row = r;
              f.Show();
                this.Close();
            }
        }
    }
}
