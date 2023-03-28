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
    public partial class Main : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        public Main()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }
        #region Eventos

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails.ShowDialog(this);
        }
        #endregion

        private void Main_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        public void cargarDatos(string buscar = null)
        {
            List<Contact> contactos = _businessLogicLayer.obtenerContactos(buscar);
            dataContactos.DataSource = contactos;
            
        }

        private void dataContactos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell) dataContactos.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() == "Editar")
            {
                ContactDetails detalleContacto = new ContactDetails();
                detalleContacto.LoadContact(new Contact
                {
                    Id = int.Parse(dataContactos.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = dataContactos.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = dataContactos.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = dataContactos.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = dataContactos.Rows[e.RowIndex].Cells[4].Value.ToString(),
                });
                detalleContacto.ShowDialog(this);
            }
            if (cell.Value.ToString() == "delete")
            {
                deleteContact(int.Parse(dataContactos.Rows[e.RowIndex].Cells[0].Value.ToString()));
                cargarDatos();
            }
        }

        private void deleteContact(int id)
        {
            _businessLogicLayer.DeleteContact(id);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            cargarDatos(txtSearch.Text);
            txtSearch.Text = string.Empty;       
        }
    }
}
