using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public String serial, name, kor, eng, math;
        Form1 mainForm = null;
        ListViewItem item = null;
        
        public Form2(Form1 mf)
        {
            InitializeComponent();
            this.mainForm = mf;

        }

        public Form2(ListViewItem i)
        {
            InitializeComponent();
            this.item = i;
            textBox1.Text = item.SubItems[1].Text;
            textBox2.Text = item.SubItems[2].Text;
            textBox3.Text = item.SubItems[3].Text;
            textBox4.Text = item.SubItems[4].Text;
            textBox5.Text = item.SubItems[5].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //do input data processing
            if (mainForm != null)
            {
                serial = textBox1.Text;
                name = textBox2.Text;
                kor = textBox3.Text;
                eng = textBox4.Text;
                math = textBox5.Text;
                mainForm.addStudent(serial, name, kor, eng, math);
            }
            else
            {
                item.SubItems[1].Text = textBox1.Text;
                item.SubItems[2].Text = textBox2.Text;
                item.SubItems[3].Text = textBox3.Text;
                item.SubItems[4].Text = textBox4.Text;
                item.SubItems[5].Text = textBox5.Text;
                
            }
            Close();
        }

       
    }
}
