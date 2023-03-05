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
    public partial class all : Form
    {
        public all()
        {
            InitializeComponent();
        }

        private void _all()
        {
            using (models.libreriaEntities db = new models.libreriaEntities())
            {
                var list = (from p in db.productos
                            where p.activo == 1
                            select new
                            {
                                p.id,
                                p.producto1,
                                p.descripcion,
                                p.fecha_ingreso,
                                p.precio
                            }).ToList();

                dtgProductos.DataSource = list;
                dtgProductos.Columns[0].Visible = false;
                lblEstado.Text = $"{list.Count.ToString()} productos encontrados";
            }
        }

        private void all_Load_1(object sender, EventArgs e)
        {
            _all();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dtgProductos.CurrentRow.Cells["id"].Value.ToString());
            views.productos._form f = new _form(id);
            f.ShowDialog();
            _all();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            views.productos._form f = new _form();
            f.ShowDialog();
            _all();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Desea eliminar el registro?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (models.libreriaEntities db = new models.libreriaEntities())
            {
                int id = int.Parse(dtgProductos.CurrentRow.Cells["id"].Value.ToString());

                var p = db.productos.Find(id);
                p.activo = 0;
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;

                if (db.SaveChanges() != 0)                
                    MessageBox.Show("Registro procesado exitosamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);                                    
                else
                    MessageBox.Show("hubo un error !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _all();
            }
        }
    }
}
