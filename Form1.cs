using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();

            this.Text = "학생 성적";
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            Form2 f2 = new Form2(this);
            f2.ShowDialog();
        }

        public void addStudent(String serial, String name, String kor, String eng, String math)
        {
            String num = (listView1.Items.Count + 1).ToString();
            ListViewItem item = new ListViewItem(num);
            item.SubItems.Add(serial);
            item.SubItems.Add(name);
            item.SubItems.Add(kor);
            item.SubItems.Add(eng);
            item.SubItems.Add(math);
            listView1.Items.Add(item);
        }


        public void deleteItem(String serial)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("#", 40);
            listView1.Columns.Add("학번", 70);
            listView1.Columns.Add("이름", 60);
            listView1.Columns.Add("국어", 50);
            listView1.Columns.Add("영어", 50);
            listView1.Columns.Add("수학", 50);
            listView1.View = View.Details;
        }
        int col_old = 0;
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // 리스트뷰의 ColumnClick 이벤트 핸들러
          
       
	     listView1.Columns[col_old].Text = listView1.Columns[col_old].Text.Replace(" ▼", "");
         listView1.Columns[col_old].Text = listView1.Columns[col_old].Text.Replace(" ▲", "");

	     listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text.Replace(" ▼", "");
	     listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text.Replace(" ▲", "");
         col_old = e.Column;

	   if (this.listView1.Sorting == SortOrder.Ascending || listView1.Sorting == SortOrder.None)
	  {
		this.listView1.ListViewItemSorter = new ListviewItemComparer(e.Column, "desc");
		listView1.Sorting = SortOrder.Descending;
		listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + " ▼";
	} else {
		this.listView1.ListViewItemSorter = new ListviewItemComparer(e.Column, "asc");
		listView1.Sorting = SortOrder.Ascending;
		listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + " ▲";
	}

	listView1.Sort();



        }

        class ListviewItemComparer : System.Collections.IComparer
        {
            private int col;
            public string sort = "asc";
            public ListviewItemComparer()
            {
                col = 0;
            }

            public ListviewItemComparer(int column, string sort)
            {
                col = column;
                this.sort = sort;
            }

            public int Compare(object x, object y)
            {
                if (sort == "asc")
                {
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else
                {
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
                }
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                ListViewItem item = listView1.SelectedItems[0];
                string num = item.SubItems[0].Text;
                string serial = item.SubItems[1].Text;
                string name = item.SubItems[2].Text;
                string kor = item.SubItems[3].Text;
                string eng = item.SubItems[4].Text;
                string math = item.SubItems[5].Text;

                Form2 f2 = new Form2(item);
                f2.ShowDialog();
            }
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "data";
            saveFileDialog1.DefaultExt = "txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                // save list to a file or DB
                StreamWriter wr = new StreamWriter(saveFileDialog1.FileName, false);//TURE->이어서 만듦 false-> 새로만듦
                // false: new file.

                String data;

                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    data = listView1.Items[i].SubItems[0].Text;
                    data = data + "|" + listView1.Items[i].SubItems[1].Text;
                    data = data + "|" + listView1.Items[i].SubItems[2].Text;
                    data = data + "|" + listView1.Items[i].SubItems[3].Text;
                    data = data + "|" + listView1.Items[i].SubItems[4].Text;
                    data = data + "|" + listView1.Items[i].SubItems[5].Text;
                    wr.WriteLine(data);

                }
                wr.Close();
            }
        }

        private void menuLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Clear();
                StreamReader rdr = new StreamReader(openFileDialog1.FileName, false);
                String line;
                while ((line = rdr.ReadLine()) != null)
                {
                    // 한 라인씩 읽어서 리스트뷰 아이템을 구성하여 추가.
                    //1|201901|ㅇㅇㅇ|50|50|50
                    String[] subItem = line.Split('|');
                    addStudent(subItem[1], subItem[2], subItem[3], subItem[4], subItem[5]);
                }

            }
        }
        private void menuDetailVeiw_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void menuSimpleView_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
        }

        private void menuHelp_Click(object sender, EventArgs e)
        {

        }

        private void menuInformation_Click(object sender, EventArgs e)
        {
            String msg = "     안동대학교\n";
            msg = msg + "학생성적부 Version 0.01\n";
            msg = msg + "      2019.11.12";
            MessageBox.Show(msg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];
            listView1.SelectedItems[0].Remove();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
           
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            String txt = " #  학번  이름  국어  영어  수학 ";
            Font font = new Font("Times New Roman", 14);
            PointF point = new PointF(50, 50);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(txt, font, brush, point);
            Pen pen = new Pen(brush);
            g.DrawLine(pen, 40, 45, 500, 45);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            listView1.Items.Clear();
            
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //MessageBox.Show(files[0]);

            listView1.Items.Clear();
            StreamReader rdr = new StreamReader(files[0]);
            String line;
            while ((line = rdr.ReadLine()) != null)
            {
                // 한 라인씩 읽어서 리스트뷰 아이템을 구성하여 추가.
                //1|201901|ㅇㅇㅇ|50|50|50
                String[] subItem = line.Split('|');
                addStudent(subItem[1], subItem[2], subItem[3], subItem[4], subItem[5]);
            }

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                String s = "";
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].SubItems[2].Text.Contains(textBox1.Text) == true)
                    {
                        s += listView1.Items[i].SubItems[2].Text + "\n";
                    }
                }

                MessageBox.Show(s);
            }
        }

        
    }

}
