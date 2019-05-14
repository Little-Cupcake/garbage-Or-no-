using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static unicorn.Data;
using unicorn.DataTableAdapters;
using System.IO;
using System.Xml;

namespace unicorn
{
    public partial class Form1 : Form
    {
        bool b = true;
        string filename = "";
        //------------------------------------------------------
        public void SaveToFile(XmlTextWriter xmlOut, int col,int row)
        { 
            xmlOut.WriteStartElement("go to");

            if (dataGridView1[col, row].Value != null)
            {
                xmlOut.WriteAttributeString(dataGridView1.Columns[col].Name.ToString(), row.ToString() + ". " + dataGridView1[col, row].Value.ToString());
            }
            else
                xmlOut.WriteAttributeString(dataGridView1.Columns[col].Name.ToString(), row.ToString() +". "+ "|==========|");
                    
                
          
            
            xmlOut.WriteEndElement();


        }

        //----------------------------------------------------

        public string StringDay()
        {
            string weekDay = "";
            switch (day())
            {
                case 1:
                    weekDay = "poniedziałek";
                    break;
                case 2:
                    weekDay = "wtorek";
                    break;
                case 3:
                    weekDay = "środa";
                    break;
                case 4:
                    weekDay = "czwartek";
                    break;
                case 5:
                    weekDay = "piątek";
                    break;

            }
            return weekDay;
        }
        //----------------------------------------------------
        public void Export()
        {
            //создание потока записи и объекта создания  xml-документа
            FileStream fs = new FileStream(filename, FileMode.Create);
            XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.Unicode);

            xmlOut.Formatting = Formatting.Indented;

            //старт начала документа
            xmlOut.WriteStartDocument();
            //xmlOut.WriteComment("Тестовое задание. Телефонная книга");
            //xmlOut.WriteComment("Работу выполнил: Григорий Чуприна");

            //создание корневого документа
            xmlOut.WriteStartElement(StringDay());
        //xmlOut.WriteAttributeString(" ");
        int a = 0;
        for (int i = 0; i < data.grupa.Count; i++)
        {
        a = 0;
        while (a < dataGridView1.Rows.Count)
        {
            SaveToFile(xmlOut,i,a);
                a++;
        }
        }

            //закрытие основного тега и документа
            xmlOut.WriteEndElement();
            xmlOut.WriteEndDocument();

            //закрытие объекта записи
            xmlOut.Close();
            fs.Close();
        }
        public void Clean()
        {
            for (int j = 0; j < 5; j++)
                {
            for(int i = 0; i< 10; i++)
            {

                    dataGridView2[j, i].Style.BackColor = Color.White;
                }
            }
        }

        void paintLesson()
        {

            listBox1.Items.Clear();
            for (int i = 0; i < data.plan.Rows.Count; i++)
            {
                if (Convert.ToInt32(data.plan.Rows[i][6]) == Convert.ToInt32(data.grupa.Rows[dataGridView1.CurrentCell.ColumnIndex][0]))
                {
                    if (data.plan.Rows[i][4] == Convert.DBNull && data.plan.Rows[i][5] == Convert.DBNull)
                        listBox1.Items.Add(data.przedmiot.FindById(Convert.ToInt32(data.plan.Rows[i][1]))[1]);

                }

            }
        }

        int t = 0;
        int col1 = 0;
        //-------check grade time---------------
        public bool grade_time(int time, int day,int grade)
        {
            for (int i = 0; i < data.klasa_time.Rows.Count; i++)
            {
                if (Convert.ToInt32(data.klasa_time.Rows[i][1]) == grade && Convert.ToInt32(data.klasa_time.Rows[i][2]) == time&& Convert.ToInt32(data.klasa_time.Rows[i][3]) == day)
                {
                  //  MessageBox.Show("Klasa nie może mieć  lekcji z dwóch przedmiotów w tym samym czasie ");
                  return false;
                }

                    
            }
            return true;

        }
        //-----------check teacher time-----------------------
        public bool teacher_time(int time, int day, int teach)
        {
            

            for (int i = 0; i < data.teacher_time.Rows.Count; i++)
            {
                if (Convert.ToInt32(data.teacher_time.Rows[i][1]) == teach && Convert.ToInt32(data.teacher_time.Rows[i][2]) == time && Convert.ToInt32(data.teacher_time.Rows[i][3]) == day)
                {
                  //  MessageBox.Show("Nauczyciel " + data.nauczyciel.FindById(Convert.ToInt32(data.teacher_time.Rows[i][1]))[1].ToString() + " jest niedostępny w tym czasie");
                  return false;

                }
                    
            }
            return true;
        }
        //-----------check room time-------------------------------
        public bool room_time(int time, int day, int room)
        {
            

            for (int i = 0; i < data.room_time.Rows.Count; i++)
            {
                if (Convert.ToInt32(data.room_time.Rows[i][1]) == room && Convert.ToInt32(data.teacher_time.Rows[i][2]) == time && Convert.ToInt32(data.teacher_time.Rows[i][3]) == day )
                {
                  //  MessageBox.Show("Sala " + data.Sala.FindById(Convert.ToInt32(data.room_time.Rows[i][1]))[1].ToString() + "jest w tym czasie zajęta ");
                  return false;
                }
                    
            }
            return true;
        }
        //------------------------------------------------------------------
        public int day()
        {
            if (checkBox1.Checked)
                return 1;
            if (checkBox2.Checked)
                return 2;
            if (checkBox3.Checked)
                return 3;
            if (checkBox4.Checked)
                return 4;
            if (checkBox5.Checked)
                return 5;
            return -1;
        }
        //-----------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
            dataGridView1.MultiSelect = false;
            
        }
        //--------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            this.salaTableAdapter.Fill(this.data.Sala);           
            this.tableTableAdapter.Fill(this.data.Table);
            this.nauczycielTableAdapter.Fill(this.data.nauczyciel);
            this.grupaTableAdapter.Fill(this.data.grupa);
            this.przedmiotTableAdapter.Fill(this.data.przedmiot);
            this.room_timeTableAdapter.Fill(this.data.room_time);
            this.teacher_timeTableAdapter.Fill(this.data.teacher_time);
            this.planTableAdapter.Fill(this.data.plan);
            klasa_timeTableAdapter.Fill(data.klasa_time);
            for (int i = 0; i < data.grupa.Rows.Count; i++)
            {
                if (data.grupa.Rows.Count != 0)
                    dataGridView1.Columns.Add(data.grupa.Rows[i][1].ToString(), data.grupa.Rows[i][1].ToString());



            }
            if (dataGridView1.Columns.Count != 0)
                dataGridView1.Rows.Add(10); dataGridView1.ClearSelection();
            
                dataGridView2.Rows.Add(10);

            dataGridView2.ClearSelection();
        }
        
        //--------------------------------------------------------------
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int col2 = dataGridView1.CurrentCell.ColumnIndex;
            if(col1!= col2)
            {
                col1 = col2;
                Clean();
            }
        
            paintLesson();
            


        }
        //-----------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        { 

            if (listBox1.SelectedItem != null && day() > 0)
            { 
            t = 0;
            int n = dataGridView1.CurrentCell.ColumnIndex;
           
               
            DataRow[] same_name = data.przedmiot.Select(string.Format("nazwa = '{0}'" , listBox1.SelectedItem.ToString()));

            for (int i = 0; same_name.Length > i; i++)
            {

                if (same_name[i][3].Equals(data.grupa.Rows[n][0]))
                {

                    DataRow[] lesson = data.plan.Select("[fk przedmiotu] = " + Convert.ToString(same_name[i][0])); 
                    while (t < lesson.Length)
                    {
                            if (!lesson[t][3].Equals(Convert.DBNull) && !lesson[t][2].Equals(Convert.DBNull))
                            {
                                if (lesson[t][4].Equals(Convert.DBNull) && lesson[t][5].Equals(Convert.DBNull))
                                {


                                    if (data.teacher_time.Count != 0 && data.room_time.Count != 0)
                                    {
                                        if (teacher_time(dataGridView1.CurrentCell.RowIndex, day(), Convert.ToInt32(lesson[t][3])) && room_time(dataGridView1.CurrentCell.RowIndex, day(), Convert.ToInt32(lesson[t][2])) && grade_time(dataGridView1.CurrentCell.RowIndex, day(), Convert.ToInt32(lesson[t][6])))
                                        {
                                            lesson[t].SetField(4, dataGridView1.CurrentCell.RowIndex);
                                            lesson[t].SetField(5, day());
                                            planTableAdapter.Update(data.plan);
                                            DataRow Trow = data.teacher_time.NewRow();
                                            DataRow Rrow = data.room_time.NewRow();
                                            DataRow Grow = data.klasa_time.NewRow();
                                            Grow[1] = Convert.ToInt32(lesson[t][6]);

                                            Trow[1] = Convert.ToInt32(lesson[t][3]);
                                            Rrow[1] = Convert.ToInt32(lesson[t][2]);
                                            Grow[2] = dataGridView1.CurrentCell.RowIndex;
                                            Trow[2] = dataGridView1.CurrentCell.RowIndex;
                                            Rrow[2] = dataGridView1.CurrentCell.RowIndex;
                                            Grow[3] = day();
                                            Trow[3] = day();
                                            Rrow[3] = day();
                                            Grow[0] = Convert.ToInt32(data.Table.Rows[0][8]) + 1;
                                            data.Table.Rows[0][8] = Convert.ToInt32(data.Table.Rows[0][8]) + 1;
                                            Trow[0] = Convert.ToInt32(data.Table.Rows[0][6]) + 1;
                                            data.Table.Rows[0][6] = Convert.ToInt32(data.Table.Rows[0][6]) + 1;
                                            Rrow[0] = Convert.ToInt32(data.Table.Rows[0][7]) + 1;
                                            data.Table.Rows[0][7] = Convert.ToInt32(data.Table.Rows[0][7]) + 1;
                                            tableTableAdapter.Update(data.Table);
                                            data.teacher_time.Rows.Add(Trow);
                                            teacher_timeTableAdapter.Update(data.teacher_time);
                                            data.room_time.Rows.Add(Rrow);
                                            room_timeTableAdapter.Update(data.room_time);
                                            data.klasa_time.Rows.Add(Grow);
                                            klasa_timeTableAdapter.Update(data.klasa_time);
                                            data.AcceptChanges();
                                            dataGridView1.CurrentCell.ReadOnly = true;
                                            dataGridView1.CurrentCell.Value = listBox1.SelectedItem.ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(lesson[t][3]))[1].ToString() + " " + data.Sala.FindById(Convert.ToInt32(lesson[t][2]))[1].ToString();
                                            dataGridView1.CurrentCell.Style.BackColor = Color.Gray;
                                            dataGridView1.ClearSelection();
                                            paintLesson();
                                            dataGridView2[Convert.ToInt32(lesson[t][5]) - 1, Convert.ToInt32(lesson[t][4])].Style.BackColor = Color.Red;
                                            MessageBox.Show("lekcja dodana w siatkę planu");
                                            t++;
                                           
                                            break;
                                        }
                                        else if (!teacher_time(dataGridView1.CurrentCell.RowIndex, day(), Convert.ToInt32(lesson[t][3])))
                                        {
                                            MessageBox.Show("Nauczyciel " + data.nauczyciel.FindById(Convert.ToInt32(lesson[t][3])).ToString() + " nie jest dostępny");
                                            break;
                                        }
                                        else if (!room_time(dataGridView1.CurrentCell.RowIndex, day(), Convert.ToInt32(lesson[t][2])))
                                        {
                                            MessageBox.Show("Sala " + data.Sala.FindById(Convert.ToInt32(lesson[t][2])).ToString() + " nie jest dostępna");
                                            break;
                                        }
                                        else if (!grade_time(dataGridView1.CurrentCell.RowIndex, day(), Convert.ToInt32(lesson[t][6])))
                                       {
                                            MessageBox.Show("Klasa " + data.grupa.FindById(Convert.ToInt32(lesson[t][6])).ToString() + " nie jest dostępna");
                                            break;
                                        }
                                    }
                                    else
                                    {

                                        lesson[t].SetField(4, dataGridView1.CurrentCell.RowIndex);
                                        lesson[t].SetField(5, day());
                                        planTableAdapter.Update(data.plan);
                                        DataRow Trow = data.teacher_time.NewRow();
                                        DataRow Rrow = data.room_time.NewRow();
                                        DataRow Grow = data.klasa_time.NewRow();
                                        Grow[1] = Convert.ToInt32(lesson[t][6]);
                                        Grow[2] = dataGridView1.CurrentCell.RowIndex;
                                        Grow[3] = day();
                                        Grow[0] = Convert.ToInt32(data.Table.Rows[0][8]) + 1;
                                        data.Table.Rows[0][8] = Convert.ToInt32(data.Table.Rows[0][8]) + 1;
                                        Trow[1] = Convert.ToInt32(lesson[t][3]);//InvalidCastException: "Object cannot be cast from DBNull to other types."
                                        Rrow[1] = Convert.ToInt32(lesson[t][2]);
                                        Trow[2] = dataGridView1.CurrentCell.RowIndex;
                                        Rrow[2] = dataGridView1.CurrentCell.RowIndex;
                                        Trow[3] = day();
                                        Rrow[3] = day();
                                        Trow[0] = Convert.ToInt32(data.Table.Rows[0][6]) + 1;
                                        data.Table.Rows[0][6] = Convert.ToInt32(data.Table.Rows[0][6]) + 1;
                                        Rrow[0] = Convert.ToInt32(data.Table.Rows[0][7]) + 1;
                                        data.Table.Rows[0][7] = Convert.ToInt32(data.Table.Rows[0][7]) + 1;
                                        tableTableAdapter.Update(data.Table);
                                        data.teacher_time.Rows.Add(Trow);
                                        teacher_timeTableAdapter.Update(data.teacher_time);
                                        data.room_time.Rows.Add(Rrow);
                                        room_timeTableAdapter.Update(data.room_time);
                                        data.klasa_time.Rows.Add(Grow);
                                        klasa_timeTableAdapter.Update(data.klasa_time);
                                        data.AcceptChanges();
                                        dataGridView1.CurrentCell.ReadOnly = true;
                                        dataGridView1.CurrentCell.Value = listBox1.SelectedItem.ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(lesson[t][3]))[1].ToString() + " " + data.Sala.FindById(Convert.ToInt32(lesson[t][2]))[1].ToString();
                                        dataGridView1.CurrentCell.Style.BackColor = Color.Gray;
                                        dataGridView1.ClearSelection();
                                        paintLesson();
                                        dataGridView2[Convert.ToInt32(lesson[t][5]) - 1, Convert.ToInt32(lesson[t][4])].Style.BackColor = Color.Red;
                                        t++;
                                       
                                        MessageBox.Show("dodane w siatkę planu");
                                        break;
                                    }




                                }
                                else
                                    t++;

                            } else
                            { MessageBox.Show("Nie można wpisać w plan przemniotu, który nie zawiera informacji o nauczycielu i/lub sali");break; }
                           // break;
                        }
                    
                }
                





        }   }
            
 
            }
        //---------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            
           this.Hide();
        }
        //------------------------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            if( dataGridView1.CurrentCell.Value != null)
            {   
                t = 0;
                int n = dataGridView1.CurrentCell.OwningColumn.Index;
                DataRow[] less = data.plan.Select("czas =" + dataGridView1.CurrentRow.Index.ToString() + ("and dzień = " + day().ToString()));
                for(int i = 0; i < less.Length + 1; i++)
                {
                    if (less[i][6].Equals(data.grupa.Rows[n][0]))
                    {
                        DataRow[] lesson = data.plan.Select("[fk przedmiotu] = " + Convert.ToString(less[i][1])); //+ " and czas = ' ' and dzień = ' '"
                        while (t < lesson.Length)
                        { if (lesson[t][4] != Convert.DBNull && lesson[t][5] != Convert.DBNull)
                            {
                            if (Convert.ToInt32(lesson[t][4]) == dataGridView1.CurrentCell.RowIndex && Convert.ToInt32(lesson[t][5]) == day())
                            {
                                DataRow[] teach_time = data.teacher_time.Select("teacher = " + Convert.ToString(lesson[t][3]));
                                DataRow[] R_time = data.room_time.Select("room = " + Convert.ToString(lesson[t][2]));
                                DataRow[] G_time = data.klasa_time.Select("grupa = " + Convert.ToString(lesson[t][6]));
                                for(int k = 0; k < teach_time.Length; k++)
                                {
                                    if(Convert.ToInt32(data.teacher_time.Rows[k][2]) == Convert.ToInt32(lesson[t][4]) && Convert.ToInt32(data.teacher_time.Rows[k][3])== Convert.ToInt32(lesson[t][5]))
                                    {
                                        data.teacher_time.Rows[k].Delete();
                                        teacher_timeTableAdapter.Update(data.teacher_time);
                                        break;
                                    }
                                }
                                for(int k = 0; k < R_time.Length; k++)
                                {
                                    if (Convert.ToInt32(data.room_time.Rows[k][2]) == Convert.ToInt32(lesson[t][4]) && Convert.ToInt32(data.room_time.Rows[k][3]) == Convert.ToInt32(lesson[t][5]))
                                    {
                                        data.room_time.Rows[k].Delete();
                                        room_timeTableAdapter.Update(data.room_time);
                                        break;
                                    }
                                }
                                for (int k = 0; k < G_time.Length; k++)
                                {
                                    if (Convert.ToInt32(data.klasa_time.Rows[k][2]) == Convert.ToInt32(lesson[t][4]) && Convert.ToInt32(data.klasa_time.Rows[k][3]) == Convert.ToInt32(lesson[t][5]))
                                    {
                                        data.klasa_time.Rows[k].Delete();
                                        klasa_timeTableAdapter.Update(data.klasa_time);
                                        break;
                                    }
                                }
                                    dataGridView2[Convert.ToInt32(lesson[t][5]) - 1, Convert.ToInt32(lesson[t][4])].Style.BackColor = Color.Green;
                                    lesson[t].SetField(4, Convert.DBNull);
                                lesson[t].SetField(5, Convert.DBNull);
                                planTableAdapter.Update(data.plan);
                                data.AcceptChanges();
                                dataGridView1.CurrentCell.Style.BackColor = Color.White;
                                dataGridView1.CurrentCell.Value = " ";
                                dataGridView1.ClearSelection();
                                paintLesson();
                                MessageBox.Show("lekcja została usunięta z siatki planu");
                                break;
                            }else{ t++; }
                            
                            }else { t++; }
                            
                            
                        }break;
                }   } 
            }
        }
        //--------------------------------------------------------

        #region checkBox Settings/rysowanie planu na wybrany przez użytkownika dzień


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                #region Settings
                checkBox2.Checked = false;
             checkBox3.Checked = false;
             checkBox4.Checked = false;
             checkBox5.Checked = false;
                #endregion

                

                #region rysowanie
                if (data.teacher_time.Count != 0 )
                {
                    dataGridView1.Columns.Clear();
                    for (int i = 0; i < data.grupa.Rows.Count; i++)
                    {
                        if (data.grupa.Rows.Count != 0)
                            dataGridView1.Columns.Add(data.grupa.Rows[i][1].ToString(), data.grupa.Rows[i][1].ToString());



                    }
                    if (dataGridView1.Columns.Count != 0)
                        dataGridView1.Rows.Add(10);
                    dataGridView1.ClearSelection();

                    DataRow[] pl = data.plan.Select("dzień = " + day().ToString());
                    for (int i = 0; i < pl.Length; i++)
                    {   DataRow gr =  data.grupa.FindById(Convert.ToInt32(pl[i][6]));
                        int col = data.grupa.Rows.IndexOf(gr);
                        int row = Convert.ToInt32(pl[i][4]);
                        try
                        { 
                         
                        
                        dataGridView1[col, row].Value = data.przedmiot.FindById(Convert.ToInt32(pl[i][1]))[1].ToString() + " "+ data.nauczyciel.FindById(Convert.ToInt32(pl[i][3]))[1].ToString() + " " + data.Sala.FindById(Convert.ToInt32(pl[i][2]))[1].ToString();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            dataGridView1[col, row].Value = data.przedmiot.FindById(Convert.ToInt32(pl[i][1]))[1].ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(pl[i][3]))[1].ToString();
                        }
                    }


                }
                #endregion
            }
        }
        //----------------------------------------------------------------------
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                #region Settings
                checkBox1.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
                #endregion


                #region rysowanie
                if (data.teacher_time.Count != 0)
                {
                    dataGridView1.Columns.Clear();
                    for (int i = 0; i < data.grupa.Rows.Count; i++)
                    {
                        if (data.grupa.Rows.Count != 0)
                            dataGridView1.Columns.Add(data.grupa.Rows[i][1].ToString(), data.grupa.Rows[i][1].ToString());



                    }
                    if (dataGridView1.Columns.Count != 0)
                        dataGridView1.Rows.Add(10); dataGridView1.ClearSelection();

                    DataRow[] pl = data.plan.Select("dzień = " + day().ToString());
                    for (int i = 0; i < pl.Length; i++)
                    {
                        DataRow gr = data.grupa.FindById(Convert.ToInt32(pl[i][6]));
                        int col = data.grupa.Rows.IndexOf(gr);
                        int row = Convert.ToInt32(pl[i][4]);
                        try
                        {


                            dataGridView1[col, row].Value = data.przedmiot.FindById(Convert.ToInt32(pl[i][1]))[1].ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(pl[i][3]))[1].ToString() + " " + data.Sala.FindById(Convert.ToInt32(pl[i][2]))[1].ToString();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            dataGridView1[col, row].Value = data.przedmiot.FindById(Convert.ToInt32(pl[i][1]))[1].ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(pl[i][3]))[1].ToString();
                        }

                    }


                }
                #endregion

            }
        }
        //-----------------------------------------------------------------------
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                #region Settings
                checkBox2.Checked = false;
            checkBox1.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
                #endregion


                #region rysowanie
                if (data.teacher_time.Count != 0)
                {
                    dataGridView1.Columns.Clear();
                    for (int i = 0; i < data.grupa.Rows.Count; i++)
                    {
                        if (data.grupa.Rows.Count != 0)
                            dataGridView1.Columns.Add(data.grupa.Rows[i][1].ToString(), data.grupa.Rows[i][1].ToString());



                    }
                    if (dataGridView1.Columns.Count != 0)
                        dataGridView1.Rows.Add(10); dataGridView1.ClearSelection();
                    DataRow[] pl = data.plan.Select("dzień = " + day().ToString());
                    for (int i = 0; i < pl.Length; i++)
                    {
                        DataRow gr = data.grupa.FindById(Convert.ToInt32(pl[i][6]));
                        int col = data.grupa.Rows.IndexOf(gr);
                        int row = Convert.ToInt32(pl[i][4]);
                        try
                        {


                            dataGridView1[col, row].Value = data.przedmiot.FindById(Convert.ToInt32(pl[i][1]))[1].ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(pl[i][3]))[1].ToString() + " " + data.Sala.FindById(Convert.ToInt32(pl[i][2]))[1].ToString();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            dataGridView1[col, row].Value = data.przedmiot.FindById(Convert.ToInt32(pl[i][1]))[1].ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(pl[i][3]))[1].ToString();
                        }

                    }


                }
                #endregion
            }
        }
        //------------------------------------------------------------------------
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox4.Checked)
               {
                #region Settings
                checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox1.Checked = false;
            checkBox5.Checked = false;
                #endregion


                #region rysowanie
                if (data.teacher_time.Count != 0)
                {
                    dataGridView1.Columns.Clear();
                    for (int i = 0; i < data.grupa.Rows.Count; i++)
                    {
                        if (data.grupa.Rows.Count != 0)
                            dataGridView1.Columns.Add(data.grupa.Rows[i][1].ToString(), data.grupa.Rows[i][1].ToString());



                    }
                    if (dataGridView1.Columns.Count != 0)
                        dataGridView1.Rows.Add(10); dataGridView1.ClearSelection();
                    DataRow[] pl = data.plan.Select("dzień = " + day().ToString());
                    for (int i = 0; i < pl.Length; i++)
                    {
                        DataRow gr = data.grupa.FindById(Convert.ToInt32(pl[i][6]));
                        int col = data.grupa.Rows.IndexOf(gr);
                        int row = Convert.ToInt32(pl[i][4]);
                        try
                        {


                            dataGridView1[col, row].Value = data.przedmiot.FindById(Convert.ToInt32(pl[i][1]))[1].ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(pl[i][3]))[1].ToString() + " " + data.Sala.FindById(Convert.ToInt32(pl[i][2]))[1].ToString();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            dataGridView1[col, row].Value = data.przedmiot.FindById(Convert.ToInt32(pl[i][1]))[1].ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(pl[i][3]))[1].ToString();
                        }

                    }


                }
                #endregion
            }
        }
        //--------------------------------------------------------------------
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
           
            if (checkBox5.Checked)
            { 
                #region Settings
                checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox1.Checked = false;
                #endregion


                #region rysowanie
                if (data.teacher_time.Count != 0)
                {
                    dataGridView1.Columns.Clear();
                    for (int i = 0; i < data.grupa.Rows.Count; i++)
                    {
                        if (data.grupa.Rows.Count != 0)
                            dataGridView1.Columns.Add(data.grupa.Rows[i][1].ToString(), data.grupa.Rows[i][1].ToString());



                    }
                    if (dataGridView1.Columns.Count != 0)
                        dataGridView1.Rows.Add(10); dataGridView1.ClearSelection();

                    DataRow[] pl = data.plan.Select("dzień = " + day().ToString());
                    for (int i = 0; i < pl.Length; i++)
                    {
                        DataRow gr = data.grupa.FindById(Convert.ToInt32(pl[i][6]));
                        int col = data.grupa.Rows.IndexOf(gr);
                        int row = Convert.ToInt32(pl[i][4]);
                        try
                        {


                            dataGridView1[col, row].Value = data.przedmiot.FindById(Convert.ToInt32(pl[i][1]))[1].ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(pl[i][3]))[1].ToString() + " " + data.Sala.FindById(Convert.ToInt32(pl[i][2]))[1].ToString();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            dataGridView1[col, row].Value = data.przedmiot.FindById(Convert.ToInt32(pl[i][1]))[1].ToString() + " " + data.nauczyciel.FindById(Convert.ToInt32(pl[i][3]))[1].ToString();
                        }

                    }


                }
                #endregion
            }
        }
        //----------------------------------------------------------------
        #endregion
//----------------------------------------------------------------------------------
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //  Application.Exit();
        if(b)
                Application.Exit();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.Rows.Count;
            if (count == 0 || day() < 0)
            {
                MessageBox.Show("Nie ma dannych do eksportu");
                return;
            }

            if (filename == "")
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "Eksport";
                fileDialog.Filter = "XML files|*.xml";
                if (fileDialog.ShowDialog() != DialogResult.OK)
                    return;

                filename = fileDialog.FileName;
                Export();
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                
                checkBox7.Checked = false;
            }
            }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                
                checkBox6.Checked = false;
            }
        }
   
        private void imie_i_nazwiskoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            if (data.teacher_time.Any())
            {
              if (imie_i_nazwiskoComboBox.SelectedItem!= null)
              {
                    Clean();
                DataRow[] obc_t = data.teacher_time.Select("teacher = "+ data.nauczyciel.Rows[imie_i_nazwiskoComboBox.SelectedIndex][0].ToString());
                for (int i = 0; i< obc_t.Length; i++)
                {
                    int col = Convert.ToInt32(obc_t[i][3]) - 1;
                    int row = Convert.ToInt32(obc_t[i][2]);
                    dataGridView2[col, row].Style.BackColor = Color.Red;
                }
              }

            }
           
        }

        private void numerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
                if (data.room_time.Any())
                {
                    if (numerComboBox.SelectedItem != null)
                    { 
                    Clean();
                    DataRow[] obc_r = data.room_time.Select("room = " + data.Sala.Rows[numerComboBox.SelectedIndex][0].ToString());
                    for (int i = 0; i < obc_r.Length; i++)
                    {
                        int col = Convert.ToInt32(obc_r[i][3]) - 1;
                        int row = Convert.ToInt32(obc_r[i][2]);
                        dataGridView2[col, row].Style.BackColor = Color.Red;
                    }
                    }


                }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
               
            if (data.teacher_time.Any() && data.room_time.Any() && data.klasa_time.Any())
            {
                    
                DataRow[] id = data.przedmiot.Select(string.Format("Nazwa = '{0}'", listBox1.SelectedItem.ToString()));//(" and [fk klasy] = " + data.grupa.Rows[dataGridView1.CurrentCell.ColumnIndex][0].ToString()));
                    DataRow[] f_t = data.plan.Select("[fk przedmiotu] = " + id[0][0].ToString());//+ ("and czas = "+ Convert.DBNull.ToString()));//SyntaxErrorException: "Syntax error: Missing operand after '=' operator."
                    
                    for (int r = 0; r < f_t.Length; r++)
                    {
                     if (f_t[r][4].Equals(Convert.DBNull))
                     {
                    if (f_t[r][6].Equals(data.grupa.Rows[dataGridView1.CurrentCell.ColumnIndex][0]))// && f_t[r][4].Equals(Convert.DBNull))
                    {


                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if (  teacher_time(j, i + 1, Convert.ToInt32(f_t[r][3])) && room_time(j, i + 1, Convert.ToInt32(f_t[r][2])) && grade_time(j, i + 1, Convert.ToInt32(f_t[r][6])))
                                {
                                    dataGridView2[i, j].Style.BackColor = Color.Green;
                                }
                                else
                                {
                                    dataGridView2[i, j].Style.BackColor = Color.Red;
                                }

                            }
                        }
                    }}
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {


                        dataGridView2[i, j].Style.BackColor = Color.Green;
                    }
                }
            }
        }
        }
    }
        //---------the end ------------------------------------
    
}