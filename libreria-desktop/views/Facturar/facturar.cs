using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libreria_desktop.cls;
namespace libreria_desktop.views.Facturar
{
    public partial class facturar : Form
    {
        private DataTable aux2;
        private double total = 0.0;
        List<select> selects = new List<select>();
        
        public facturar()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void _all()
        {
            using (models.libreriaEntities db = new models.libreriaEntities())
            {
                var prods = (from p in db.productos
                            where p.activo == 1
                            select new
                            {
                                p.id,
                                p.producto1,
                                p.precio,
                                p.descripcion,
                                p.id_categoria,
                                p.fecha_ingreso
                            }).ToList();
                var clientes = (from c in db.clientes
                                where c.activo == 1
                                select new
                                {
                                    c.id,
                                    c.nombres,
                                    c.apellidos
                                }).ToList();
                dtgProductos.DataSource = prods;
                dtgProductos.Columns[0].Visible = false;
                dtgClientes.DataSource = clientes;
                dtgClientes.Columns[0].Visible = false;                
            }
        }

        private void facturar_Load(object sender, EventArgs e)
        {
            aux2 = new DataTable();
            aux2.Columns.Add("producto");
            aux2.Columns.Add("precio");
            aux2.Columns.Add("cantidad");

            _all();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dtgProductos.CurrentRow.Cells["id"].Value.ToString());
            string prod = dtgProductos.CurrentRow.Cells["producto1"].Value.ToString();
            int cant = int.Parse(txbCantidad.Text);
            double precio = double.Parse(dtgProductos.CurrentRow.Cells["precio"].Value.ToString());
           

            dtgSeleccionados.DataSource = aux2;
            DataRow linea = aux2.NewRow();
            linea["producto"] = prod;
            linea["precio"] = precio;
            linea["cantidad"] = cant;
            total += (cant * precio);

            aux2.Rows.Add(linea);
            dtgSeleccionados.DataSource = aux2;
            lblTotal.Text = $"Total a pagar: ${total}";
            selects.Add(new select { id = id, cantidad = cant, precio = precio, subtotal = (cant * precio) });
        }

        private void Pagar_Click(object sender, EventArgs e)
        {
            using (models.libreriaEntities db = new models.libreriaEntities())
            {
                var factura = new models.factura();
                factura.id_cliente = int.Parse(dtgClientes.CurrentRow.Cells["id"].Value.ToString());
                factura.id_empleado = AppManager.user.id;
                factura.total = 0;
                factura.cant_prods = 0;
                factura.fecha = DateTime.Now;
                db.facturas.Add(factura);
                db.SaveChanges();
                
                int cant_prods = 0;
                double total = 0.0;
                foreach(var f in selects)
                {
                    cant_prods += f.cantidad;
                    total += f.subtotal;

                    var fp = new models.factura_productos();
                    fp.cant_prods = f.cantidad;
                    fp.id_producto = f.id;
                    fp.id_factura = factura.id;
                    fp.sub_total = (decimal)f.subtotal;
                    db.factura_productos.Add(fp);
                    db.SaveChanges();
                }

                factura.total = (decimal)total;
                factura.cant_prods = cant_prods;
                db.Entry(factura).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("factura creada exitosamente", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
