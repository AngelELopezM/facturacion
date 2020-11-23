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
using database_logic.comboboxitem;
using database_logic.model;
namespace la_pañalera
{

    public partial class frm_productos : Form
    {
        double ganancia;
        bool editar_prod;
        int id;
        database servicios;
        public frm_productos(bool editar)
        {
            editar_prod = editar;
            servicios = new database();
            InitializeComponent();
        }
        #region eventos
        private void frm_productos_Load(object sender, EventArgs e)
        {
            if (editar_prod == true)
            {
                btn_editar.Enabled = true;

            }
            else
            {
                btn_agregar.Enabled = true;
            }
            load_combo();
            load_grid();
            load_cb_busqueda();

        }

        private void tb_precio_compra_KeyUp_1(object sender, KeyEventArgs e)
        {/*Con este evento mientras vayamos digitando los valores, el calculo de la ganancia se ira haciendo
            automaticamente utilizando el metodo*/
            try
            {
                calcular_ganancia();
            }
            catch (Exception)
            {
                tb_precio_compra.Text = "0";
                MessageBox.Show("Debe digitar un valor numerico");

            }
        }

        private void tb_precio_venta_KeyUp(object sender, KeyEventArgs e)
        {
            /*Con este evento mientras vayamos digitando los valores, el calculo de la ganancia se ira haciendo
            automaticamente utilizando el metodo*/
            try
            {
                calcular_ganancia();
            }
            catch (Exception)
            {
                tb_precio_venta.Text = "0";
                MessageBox.Show("Debe digitar un valor numerico");

            }
        }
        private void btn_editar_Click_1(object sender, EventArgs e)
        {/*Aqui verificamos que el producto tenga una ganancia antes de editarlo*/
            try
            {
                if (ganancia <= 0)
                {
                    MessageBox.Show("La ganancia del producto debe ser mayor a Cero");
                }
                else
                {
                    editar();
                    load_grid();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No puede dejar ningun campo vacio");

            }
        }

        private void btn_agregar_Click_1(object sender, EventArgs e)
        {/*Verificamos que el producto tenga una ganancia antes de agregarlo*/
            try
            {

                if (ganancia <= 0)
                {
                    MessageBox.Show("La ganancia del producto debe ser mayor a Cero");
                }
                else
                {
                    agregar_producto();
                    load_grid();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("No puede dejar ningun campo vacio");
            }
        }

        private void dgv_productos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0 && editar_prod == true)
            {
                id = Convert.ToInt32(dgv_productos.Rows[index].Cells[0].Value);
                tb_nombre.Text = dgv_productos.Rows[index].Cells[1].Value.ToString();
                cb_categoria.Text = dgv_productos.Rows[index].Cells[2].Value.ToString();
                tb_precio_compra.Text = dgv_productos.Rows[index].Cells[3].Value.ToString();
                tb_precio_venta.Text = dgv_productos.Rows[index].Cells[4].Value.ToString();
                tb_ganancia.Text = dgv_productos.Rows[index].Cells[5].Value.ToString();
                tb_direccion.Text = dgv_productos.Rows[index].Cells[6].Value.ToString();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            buscar_producto();
        }
        #endregion








        #region metodos

        private void load_combo()
        {/*Aqui primero creamos la opcion por defecto que va a tener el combobox
            despues creamos un listado del tipo del combobox y le pasamos lo que tragimos de la
            base de datos y con un bloque forech llenamos el CB*/
            combobox seleccion_por_defecto = new combobox() {
                value = null,
                categoria = "Seleccione una opcion"
            };
            cb_categoria.Items.Add(seleccion_por_defecto);
            cb_categoria.SelectedItem = seleccion_por_defecto;

            List<combobox> listado = servicios.load_combo_categoria();

            foreach (var item in listado)
            {
                combobox opciones_combo = new combobox()
                {
                    value = item.value,
                    categoria = item.categoria
                };
                cb_categoria.Items.Add(opciones_combo);
            }
        }
        private void agregar_producto()
        {
            combobox opcion_combo = cb_categoria.SelectedItem as combobox;
            producto productos_nuevos = new producto();

            productos_nuevos.nombre = tb_nombre.Text;
            productos_nuevos.id_categoria = Convert.ToInt32(opcion_combo.value);
            productos_nuevos.precio_compra = Convert.ToDouble(tb_precio_compra.Text);
            productos_nuevos.precio_venta = Convert.ToDouble(tb_precio_venta.Text);
            productos_nuevos.ganancia = Convert.ToDouble(tb_ganancia.Text);
            productos_nuevos.descripcion = tb_direccion.Text;

            servicios.agregar_productos(productos_nuevos);
            MessageBox.Show("Producto agregado");
            limpiar_boxes();
        }

        private void editar()
        {
            combobox opcion_combo = cb_categoria.SelectedItem as combobox;
            producto productos_nuevos = new producto();

            productos_nuevos.nombre = tb_nombre.Text;
            productos_nuevos.id_categoria = Convert.ToInt32(opcion_combo.value);
            productos_nuevos.precio_compra = Convert.ToDouble(tb_precio_compra.Text);
            productos_nuevos.precio_venta = Convert.ToDouble(tb_precio_venta.Text);
            productos_nuevos.ganancia = Convert.ToDouble(tb_ganancia.Text);
            productos_nuevos.descripcion = tb_direccion.Text;

            servicios.editar_producto(productos_nuevos, id);
            MessageBox.Show("Producto editado");
            limpiar_boxes();
        }

        private void calcular_ganancia()
        {
            double precio_compra, precio_venta;
            precio_compra = Convert.ToDouble(tb_precio_compra.Text);
            precio_venta = Convert.ToDouble(tb_precio_venta.Text);

            ganancia = (precio_venta - precio_compra);
            tb_ganancia.Text = ganancia.ToString();

        }

        private void load_grid()
        {

            dgv_productos.DataSource = servicios.load_productos();
        }

        private void limpiar_boxes()
        {
            tb_nombre.Clear();
            tb_ganancia.Clear();
            tb_direccion.Clear();
            tb_precio_compra.Clear();
            tb_precio_venta.Clear();
            tb_direccion.Clear();
        }


        private void load_cb_busqueda()
        {
            combobox opcion_defecto = new combobox
            {
                value = null,
                categoria = "Seleccione una opcion"

            };
            cb_busqueda.Items.Add(opcion_defecto);

            combobox buscar_id = new combobox
            {
                value = 1,
                categoria = "ID"
                
            };
            cb_busqueda.Items.Add(buscar_id);

            combobox buscar_nombre = new combobox
            {
                value = 2,
                categoria = "Nombre del producto"

            };
            cb_busqueda.Items.Add(buscar_nombre);

            cb_busqueda.SelectedItem = opcion_defecto;

        }
        
        private void buscar_producto()
        {
            /*Aqui utilizamos el value de */
            combobox busqueda = cb_busqueda.SelectedItem as combobox;
            if (busqueda.value == null)
            {
                MessageBox.Show("Tiene que seleccionar un metodo de busqueda");
            }
            else
            {/*utilizamos el tipo de busqueda para determinar porque medio es que vamos a buscar al cliente
                basado en las opciones seleccionadas en el combobox que tenemos  y cargar el data grid*/
                int tipo_busqueda = (int)busqueda.value;
                dgv_productos.DataSource = servicios.buscar_producto(tipo_busqueda,tb_buscar.Text);
            }
        }





        #endregion

        
    }
}
