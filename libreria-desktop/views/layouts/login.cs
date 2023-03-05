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

namespace libreria_desktop.views.layouts
{
    public partial class login : Form
    {
        public Boolean access = false;
        public login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txbEmail.Text.Equals("") || txbPassword.Text.Equals(""))
            {
                MessageBox.Show("Ingrese su usuario y contraseña", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (libreriaEntities db = new libreriaEntities())
            {
                var user = (from em in db.empleados
                            where em.email == txbEmail.Text && em.password == txbPassword.Text && em.activo == 1
                            select new empleadoDto
                            {
                                id = em.id,
                                nombres = em.nombres,
                                apellidos = em.apellidos,
                                dui = em.dui,
                                direccion = em.direccion,
                                email = em.email,
                                password = em.password,
                                telefono = em.telefono
                            }).FirstOrDefault();

                if (user != null)
                {
                    AppManager.user = user;
                    this.access = true;
                    this.Close();
                }
                else
                    MessageBox.Show("Usuario o contraseña incorrecto", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
