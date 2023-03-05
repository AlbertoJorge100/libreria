using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libreria_desktop.views.layouts
{
    public partial class template : Form
    {
        public template()
        {
            InitializeComponent();
            //IsMdiContainer = true;
        }

        private void menu1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            productos.all p = new productos.all();
            p.MdiParent = this;
            p.Dock = DockStyle.Fill;
            p.Show();
        }

        private void template_Load(object sender, EventArgs e)
        {

        }

        private void menu2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            categorias.all c = new categorias.all();
            c.MdiParent = this;
            c.Dock = DockStyle.Fill;
            c.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clientes.all c = new clientes.all();
            c.MdiParent = this;
            c.Dock = DockStyle.Fill;
            c.Show();
        }

        private void menu3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            empleados.all em = new empleados.all();
            em.MdiParent = this;
            em.Dock = DockStyle.Fill;
            em.Show();
        }

        private void facturarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Facturar.facturar f = new Facturar.facturar();
            f.MdiParent = this;
            f.Dock = DockStyle.Fill;
            f.Show();
        }
    }
}
