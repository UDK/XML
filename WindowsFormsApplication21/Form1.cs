using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication21
{
    //доделать 5 задание
    public partial class Form1 : Form
    {
        //Можно ли их не делать глобальными
        TextBox[] texbox = new TextBox[8];
        Label[] label_Name = new Label[8];
        public Form1()
        {
            InitializeComponent();
            tabControl1.TabPages[0].Controls.Add(button1);
            tabControl1.TabPages[0].Controls.Add(button2);
            tabControl1.TabPages[0].Controls.Add(button3);
            tabControl1.TabPages[0].Controls.Add(button4);
            tabControl1.TabPages[0].Controls.Add(textBox1);
            tabControl1.TabPages[0].Controls.Add(label1);
            tabControl1.TabPages[0].Controls.Add(label2);
            tabControl1.TabPages[0].Controls.Add(label3);
            tabControl1.TabPages[0].Controls.Add(comboBox1);
            tabControl1.TabPages[1].Controls.Add(label3);
            /////////////////////////Подписочки
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            button5.Click += Button5_Click;
            включитьЭЦПToolStripMenuItem.Click += ВключитьЭЦПToolStripMenuItem_Click;
            button6.Click += Button6_Click;
            button7.Click += Button7_Click;
            /////////////////////////

            int RasmWid = this.Width / 2;
            int j = 100,b = 1;
            int per = 0;
            for (int i = 0; i < dris.mes.Length; i++)
            {
                j = 100*b;
                //Чтоб не забыть, типо 630/100,когда меньше будет,то будем размещать правее
                if (this.Height<j+100)
                {
                    j = 100;
                    b = 1;
                    per = +350;
                }
                
                
                //Здесь по лебалам
                label_Name[i] = new Label();
                label_Name[i].Text = "labla"+dris.mes[i] + i;
                label_Name[i].Text = dris.mes[i];
                label_Name[i].Location = new Point(RasmWid+per, j -30);
                this.Controls.Add(label_Name[i]);

                //Здесь кароч чисто объявляем текстбоксы
                texbox[i] = new TextBox();
                texbox[i].Name = "table"+ dris.mes[i] + i;
                texbox[i].Font = new Font("Timesnewroman", 12f);
                //Задаём локацию
                texbox[i].Location = new Point(RasmWid+per, j);
                texbox[i].Width = 300;
                this.Controls.Add(texbox[i]);
                b++;
                texbox[i].TextChanged += Form1_TextChanged;
                comboBox1.Items.Add(dris.mes[i]);
                comboBox2.Items.Add(dris.mes[i]);
                tabControl1.TabPages[0].Controls.Add(texbox[i]);
                tabControl1.TabPages[0].Controls.Add(label_Name[i]);
            }
            
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            string tag = comboBox2.Text;
            if(tag=="")
            {
                throw new Exception("Ничего не выбрано в ComboBox");
            }
            seven se = new seven();
            se.Шифрование(tag);

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            string pyt = UseDia.Pyt();
            if(pyt == "")
            {
                //Выходим если ничего не ввели
                goto QQ;
            }
            SIx cla = new SIx();
            cla.Proverka(pyt,textBox4);
            QQ:;
        }
        //Включение и переключение кнопки
        private void ВключитьЭЦПToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(((ToolStripMenuItem)sender).Checked !=true)
            {
                ((ToolStripMenuItem)sender).Checked = true;
            }
            else
            {
                ((ToolStripMenuItem)sender).Checked = false;
            }
        }
        //Кнопка для создания ЭЦП
        private void Button5_Click(object sender, EventArgs e)
        {
            if(включитьЭЦПToolStripMenuItem.Checked != true)
            {
                throw new ArgumentException("Не нажал галочку в параметрах");
            }
            string pyt = UseDia.Pyt();
            if (pyt == "")
            {
                //Выходим если ничего не ввели
                goto QQ;
            }
            Five five = new Five();
            five.WriteDSA(pyt,textBox2,textBox3);
            QQ: ;
            ////////////////ВЫход
        }

        public void Button4_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            OpenFileDialog dia = new OpenFileDialog();
            dia.ShowDialog();
            dris.pytik = dia.FileName;
            dour qq = new dour();
            int be=-1;
            for(int i=0;i<dris.mes.Length;i++)
            {
                string buff = comboBox1.Text;
                if(buff==dris.mes[i])
                {
                    be = i;
                    break;
                }
            }
            //Минус 1. типо отсчёт с 0. а указали с 1
            qq.drisna(Convert.ToInt32(textBox1.Text)-1,be);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.ShowDialog();
            dris.pytik = dia.FileName;
            Form2 qq = new Form2();
            //Надо сделать паузу. пока форма2 активна
            qq.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.ShowDialog();
            string pyt = dia.FileName;
            two t = new two();
            t.drisna(pyt);
            for(int i= 0;i<dris.mes.Length;i++)
            {
                texbox[i].Text = dris.mass[i];
            }
        }

        //Как сделать, чтобы не было boxed(как заменить обджект на Textbox)
        private void Form1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dris.mes.Length; i++)
            {
               if (((TextBox)sender).Name == "table" + dris.mes[i] + i)
                  {
                    dris.mass[i] = ((TextBox)sender).Text;
                    break;
                  }
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {  
            FolderBrowserDialog qq = new FolderBrowserDialog();
            qq.ShowDialog();
            string Pyt = qq.SelectedPath;
            first op = new first();
            op.drisna(Pyt + "\\LABA1.xml");
        }
        
    }
}
