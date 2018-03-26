using CslaPoc.Core.Business.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            this.dataGridView1.DataSource = PersonList.GetList();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
