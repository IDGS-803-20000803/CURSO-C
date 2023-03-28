using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Curso
{
    public partial class ContactDetails : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        private Contact _contacto;
        public ContactDetails()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Contact contact = new Contact();

            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtAddress.Text;
            contact.Id = _contacto != null ? _contacto.Id : 0;

            _businessLogicLayer.SaveContact(contact);
            this.Close();
            ((Main)this.Owner).cargarDatos();
        }

        public void LoadContact(Contact contacto)
        {
            _contacto = contacto;
             clearForm();
            if (contacto != null)
            {
                txtFirstName.Text = contacto.FirstName;
                txtLastName.Text = contacto.LastName;
                txtPhone.Text = contacto.Phone;
                txtAddress.Text = contacto.Address;
            }
        }
        private void clearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }
    }
}
