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

namespace libreria_desktop.views.empleados
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
                var em = new empleado();
                em.nombres = txbNombres.Text;
                em.apellidos = txbApellidos.Text;
                em.direccion = txbDireccion.Text;
                em.dui = txbDUI.Text;
                em.password = txbPassword.Text;
                em.email = txbEmail.Text;
                em.telefono = txbTelefono.Text;
                em.activo = 1;

                if (id == null)
                    db.empleados.Add(em);
                else
                {
                    em.id = (long)id;
                    db.Entry(em).State = System.Data.Entity.EntityState.Modified;
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

                var em = db.empleados.Find(id);
                txbNombres.Text = em.nombres;
                txbApellidos.Text = em.apellidos;
                txbTelefono.Text = em.telefono;
                txbDireccion.Text = em.direccion;
                txbDUI.Text = em.dui;
                txbEmail.Text = em.email;
                txbPassword.Text = em.password;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
