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

namespace libreria_desktop.views.productos
{
    public partial class _form : Form
    {
        private int? id;
        public _form(int ? id = null)
        {
            InitializeComponent();
            this.id = id;
        }

        private void _form_Load(object sender, EventArgs e)
        {           
            using (libreriaEntities db = new libreriaEntities())
            {
                var cats = db.categorias.ToList();
                cmbCategoria.DataSource = cats;
                cmbCategoria.DisplayMember = "categoria1";
                cmbCategoria.ValueMember = "id";
                cmbCategoria.SelectedIndex = -1;

                if (id == null) return;                              

                var prod = db.productos.Find(id);
                txbProducto.Text = prod.producto1;
                txbPrecio.Text = prod.precio.ToString();
                txbDescripcion.Text = prod.descripcion;

                cmbCategoria.SelectedValue = prod.categoria.id;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(libreriaEntities db = new libreriaEntities())
            {
                var p = new producto();
                p.producto1 = txbProducto.Text;
                p.descripcion = txbDescripcion.Text;
                p.fecha_ingreso = DateTime.Now;
                p.precio = (decimal)double.Parse(txbPrecio.Text);
                p.id_categoria = (long)cmbCategoria.SelectedValue;
                p.activo = 1;

                if (id == null)
                    db.productos.Add(p);
                else
                {
                    p.id = (long)id;
                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
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
    }
}
