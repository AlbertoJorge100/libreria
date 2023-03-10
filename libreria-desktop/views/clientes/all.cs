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
                var list = (from c in db.clientes
                            where c.activo == 1
                            select new
                            {
                                c.id,
                                c.nombres,
                                c.apellidos,
                                c.direccion,
                                c.telefono,
                                c.dui
                            }).ToList();

                dtgClientes.DataSource = list;
                dtgClientes.Columns[0].Visible = false;
                lblEstado.Text = $"{list.Count.ToString()} productos encontrados";
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            views.clientes.form c = new form();
            c.ShowDialog();
            _all();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dtgClientes.CurrentRow.Cells["id"].Value.ToString());
            views.clientes.form c = new form(id);
            c.ShowDialog();
            _all();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea eliminar el registro?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (models.libreriaEntities db = new models.libreriaEntities())
            {
                int id = int.Parse(dtgClientes.CurrentRow.Cells["id"].Value.ToString());

                var c = db.clientes.Find(id);
                c.activo = 0;
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;

                if (db.SaveChanges() != 0)
                    MessageBox.Show("Registro procesado exitosamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("hubo un error !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _all();
            }
        }

        private void all_Load(object sender, EventArgs e)
        {
            _all();
        }
    }
}
