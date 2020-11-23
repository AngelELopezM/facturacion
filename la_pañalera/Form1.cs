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
    public partial class frm_menu : Form
    {
        public frm_menu()
        {
            InitializeComponent();
        }

        #region eventos
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btn_clientes_Click(object sender, EventArgs e)
        {
            frm_menu_clientes menu = new frm_menu_clientes();
            menu.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_menu_productos menu = new frm_menu_productos();
            menu.ShowDialog();
        }
    }
    #endregion

    #region metodos

    #endregion


}
