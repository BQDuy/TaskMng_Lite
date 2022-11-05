using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetProcess();
        }
        /// <summary>
        /// Luu danh sach process
        /// </summary>
        Process[] process;
        /// <summary>
        /// Lay danh sach process va luu lai
        /// Dong thoi show len listview
        /// </summary>
        void GetProcess()
        {
            process = Process.GetProcesses();
            listView1.Items.Clear();
            foreach (var item in process)
            {
                string name = item.ProcessName.ToString();
                ListViewItem newItem = new ListViewItem() { Text = chuanHoa(name) };
                newItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = (item.PagedSystemMemorySize64/1024f).ToString("0.0") + " MB" });
                newItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = (item.VirtualMemorySize64/1024f).ToString("0.0") + " MB"});
                newItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = (item.WorkingSet64/1024f).ToString("0.0") + " MB" });
                listView1.Items.Add(newItem);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(process.Length != Process.GetProcesses().Length)
            {
                GetProcess();
            }
        }

        public static string chuanHoa(string str1)
        {
            return string.Join(" ", str1.Split(' ').Select(name => char.ToUpper(str1[0]) + str1.Substring(1)));
        }

        private void endTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count >0)
            {
                int index = 0;
                foreach( var item in process)
                {
                    if(item.ProcessName == listView1.SelectedItems[0].Text)
                    {
                        index = process.ToList().IndexOf(item);
                        break;
                    }
                }
                process[index].Kill();
            }
                
        }
    }
}
