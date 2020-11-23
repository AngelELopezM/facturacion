using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using database_logic.repositorio;

namespace la_pañalera
{
    public partial class frm_clientes : Form
    {
        bool editar_contac;
        int id;
        database_logic.database servicios;
        repositorio_clientes clientes;
        /*Como va a ser un solo formulario para la edicion y agregar nuevos clientes
         este constructor recibirá un booleano que le va a indicar si es un nuevo cliente o si 
         vamos a editar algun cliente*/
        public frm_clientes(bool _editar)
        {
            editar_contac = _editar;
            clientes = new repositorio_clientes();
            servicios = new database_logic.database();
            InitializeComponent();
        }

        #region eventos
        private void button4_Click(object sender, EventArgs e)
        {
            servicios.buscar_cliente(tb_buscar.Text);
        }
        private void frm_clientes_Load(object sender, EventArgs e)
        {
            load_grid();
            if (editar_contac == true)
            {
                btn_editar.Enabled = true;
                
            }
            else
            {
                btn_agregar.Enabled = true;
            }

        }
        private void btn_agregar_Click(object sender, EventArgs e)
        {//primero verificamos que ningun campo nos quede vacio
            if (string.IsNullOrWhiteSpace(tb_nombre.Text) || string.IsNullOrWhiteSpace(tb_apellido.Text) || string.IsNullOrWhiteSpace(tb_telefono.Text) || string.IsNullOrWhiteSpace(tb_direccion.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos para registrar al cliente");
            }
            else
            {
                agregar();
                MessageBox.Show("Nuevo cliente agregado con exito");
                clear_boxes();
            }
        }
        private void btn_editar_Click(object sender, EventArgs e)
        {//primero verificamos que ningun campo nos quede vacio
            if (string.IsNullOrWhiteSpace(tb_nombre.Text) || string.IsNullOrWhiteSpace(tb_apellido.Text) || string.IsNullOrWhiteSpace(tb_telefono.Text) || string.IsNullOrWhiteSpace(tb_direccion.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos para editar al cliente");
            }
            else
            {
                editar();
                MessageBox.Show("Cliente editado con exito");
                clear_boxes();
            }

        }
        private void dgv_clientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index>=0 && editar_contac == true)
            {
                id = int.Parse(dgv_clientes.Rows[index].Cells[0].Value.ToString());
                tb_nombre.Text = dgv_clientes.Rows[index].Cells[1].Value.ToString();
                tb_apellido.Text =  dgv_clientes.Rows[index].Cells[2].Value.ToString();
                tb_telefono.Text = dgv_clientes.Rows[index].Cells[3].Value.ToString();
                tb_direccion.Text = dgv_clientes.Rows[index].Cells[4].Value.ToString();

            }
            else
            {
                MessageBox.Show("Para poder editar un usuario debes volver atras y seleccionar la opcion editar");
            }
        }
        #endregion

        #region metodos
        private void agregar()
        {
            /*Aqui utilizamos el objeto cliente que instanciamos en el constructor para entonces insertar los datos
             en el metodo que agrega info a la base de datos*/
            clientes.nombre = tb_nombre.Text;
            clientes.apellido = tb_apellido.Text;
            clientes.telefono = tb_telefono.Text;
            clientes.direccion = tb_direccion.Text;
            servicios.agregar_cliente(clientes);
            load_grid();
        }

        private void editar()
        { /*Aqui utilizamos el objeto cliente que instanciamos en el constructor para entonces insertar los datos
             en el metodo que agrega info a la base de datos*/
            clientes.nombre = tb_nombre.Text;
            clientes.apellido = tb_apellido.Text;
            clientes.telefono = tb_telefono.Text;
            clientes.direccion = tb_direccion.Text;
            servicios.editar_cliente(clientes,id);
            load_grid();
        }
        private void load_grid()
        {
            dgv_clientes.DataSource = servicios.load_clientes();
        }
        private void clear_boxes()
        {
            tb_nombre.Clear();
            tb_apellido.Clear();
            tb_telefono.Clear();
            tb_direccion.Clear();
            dgv_clientes.ClearSelection();
        }


        #endregion

        
    }
}
