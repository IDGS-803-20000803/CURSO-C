using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso
{
    public class BusinessLogicLayer
    {
        private DataAccessLayer _dataAccessLayer;
        public BusinessLogicLayer()
        {
            _dataAccessLayer = new DataAccessLayer();
        }

        public Contact SaveContact(Contact contactos)
        {
            if (contactos.Id == 0)
              _dataAccessLayer.InsertContact(contactos);
            else
                _dataAccessLayer.UpdateContacto(contactos);
            return contactos;
        }

        public List<Contact> obtenerContactos(string buscar = null)
        {
           return  _dataAccessLayer.GetContactos(buscar);
        }

        public void DeleteContact(int id)
        {
            _dataAccessLayer.EliminarContacto(id);
        }
    }
}
