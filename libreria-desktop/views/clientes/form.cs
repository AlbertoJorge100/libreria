using libreria_desktop.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libreria_desktop.views.clientes
{
    public partial class form : Form
    {
        private int? id;
        public form(int ? id = null)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (libreriaEntities db = new libreriaEntities())
            {
                var c = new cliente();
                c.nombres = txbNombres.Text;
                c.apellidos = txbApellidos.Text;
                c.direccion = txbDireccion.Text;
                c.dui = txbDUI.Text;
                c.telefono = txbTelefono.Text;
                c.activo = 1;

                if (id == null)
                    db.clientes.Add(c);
                else
                {
                    c.id = (long)id;
                    db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                }

                if (db.SaveChanges() != 0)
                {
                    MessageBox.Show("Registro procesado exitosamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("hubo un error !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void form_Load(object sender, EventArgs e)
        {
            using (libreriaEntities db = new libreriaEntities())
            {                
                if (id == null) return;

                var cl = db.clientes.Find(id);
                txbNombres.Text = cl.nombres;
                txbApellidos.Text = cl.apellidos;
                txbTelefono.Text = cl.telefono;
                txbDireccion.Text = cl.direccion;
                txbDUI.Text = cl.dui;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
