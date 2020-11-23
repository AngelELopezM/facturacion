using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using database_logic;
namespace la_pañalera
{
    public partial class frm_ventas : Form
    {
        database servicios;
        public frm_ventas()
        {
            servicios = new database();
            InitializeComponent();
        }

        #region eventos
        private void frm_ventas_Load(object sender, EventArgs e)
        {
            load_grid();
        }
        #endregion

        #region metodos
        private void load_grid()
        {
            dgv_productos.DataSource = servicios.load_productos();
            dgv_productos.Columns[0].Visible = false;
        }
        #endregion
    }
}
