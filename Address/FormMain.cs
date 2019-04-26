using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Address
{
    public partial class FormAddressManager : Form
    {
        public FormAddressManager()
        {
            InitializeComponent();
        }

        static AppData db;
        protected static AppData App
        {
            get
            {
                if (db == null)
                    db = new AppData();
                return db;
            }

        }



        private void FormAddressManager_Load(object sender, EventArgs e)
        {
            textBoxPhoneNumber.Focus();
            string fileName = string.Format("{0}//data.dat", Application.StartupPath);
            if (File.Exists(fileName))
                App.AddressBook.ReadXml(fileName);
            addressBookBindingSource.DataSource = App.AddressBook;
            panel1.Enabled = false;

                
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                addressBookBindingSource.EndEdit();
                App.AddressBook.AcceptChanges();
                App.AddressBook.WriteXml(string.Format("{0}//data.dat", Application.StartupPath));
                panel1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                App.AddressBook.RejectChanges();

            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
 
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            addressBookBindingSource.ResetBindings(false);
            panel1.Enabled = false;
        }

        private void ButtonNew_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxPhoneNumber.Focus();
                panel1.Enabled = true;
                App.AddressBook.AddAddressBookRow(App.AddressBook.NewAddressBookRow());
                addressBookBindingSource.MoveLast();
                textBoxPhoneNumber.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                App.AddressBook.RejectChanges();

            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    addressBookBindingSource.RemoveCurrent();

            }

        }

        private void TextBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

            {
                {
                    // doing this only to show how much I find linq tedious ;)
                    var query = from o in App.AddressBook
                                where o.Address == textBoxSearch.Text || o.Address.Contains(textBoxSearch.Text) || o.Address == textBoxSearch.Text
                                select o;
                    dataGridView.DataSource = query.ToList();
                }

                dataGridView.DataSource = addressBookBindingSource;
            }
  

        }


        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
    }
