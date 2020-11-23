using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace la_pañalera
{
    public partial class frm_menu_productos : Form
    {
        public frm_menu_productos()
        {
            InitializeComponent();
        }

        #region eventos
        private void btn_agregar_Click(object sender, EventArgs e)
        {
            frm_productos productos = new frm_productos(false);
            productos.Show();
            this.Hide();
        }
        private void btn_editar_Click(object sender, EventArgs e)
        {
            frm_productos productos = new frm_productos(true);
            productos.Show();
            this.Hide();
        }
        #endregion


    }
}
