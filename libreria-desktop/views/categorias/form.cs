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

namespace libreria_desktop.views.categorias
{
    public partial class form : Form
    {
        private int? id;
        public form(int? id = null)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (libreriaEntities db = new libreriaEntities())
            {
                var c = new categoria();
                c.categoria1 = txbCategoria.Text;
                c.descripcion = txbDescripcion.Text;
                c.activo = 1;

                if (id == null)
                    db.categorias.Add(c);
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void form_Load(object sender, EventArgs e)
        {
            using (libreriaEntities db = new libreriaEntities())
            {
                if (id == null) return;

                var c = db.categorias.Find(id);
                txbCategoria.Text = c.categoria1;
                txbDescripcion.Text = c.descripcion;
            }
        }
    }
}
