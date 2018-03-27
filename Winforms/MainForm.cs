using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flurl.Http;

namespace Winforms
{
    public partial class MainForm : Form
    {
        PersonList Persons;

        public MainForm()
        {
            InitializeComponent();
        }

        public async void LoadData()
        {
            Persons = await PersonList.GetPersonList();
            this.dataGridView1.DataSource = Persons;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a person");
                return;
            }

            var id = Convert.ToInt16(dataGridView1.SelectedRows[0].Cells[0].Value);

            await PersonEdit.DeletePersonEdit(id);

            LoadData();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Please enter the name");
                return;
            }

            var person = PersonEdit.CreatePerson();

            person.Name = tbName.Text;

            if (person.IsSavable)
            {
                person = await person.SaveAsync();
                LoadData();

                this.tbName.Text = String.Empty;
            }
            else
            {
                var error = String.Empty;
                foreach (var item in person.BrokenRulesCollection)
                    error += item.Description + Environment.NewLine;

                MessageBox.Show(error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
