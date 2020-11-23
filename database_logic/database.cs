using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using database_logic.model;
using database_logic.repositorio;
using database_logic.comboboxitem;
using System.Data.Entity;
namespace database_logic
{
    enum opciones
    {
        
        id = 1,
        nombre_producto
    }
    public class database
    {
        #region mantenimiento clientes
        public void agregar_cliente(repositorio_clientes cliente)
        {
            /*Aqui estamos agregando a nuestros clientes*/
            using (kid_storeEntities database = new kid_storeEntities())
            {
                cliente clientes_agregar = new cliente();
                clientes_agregar.nombre = cliente.nombre;
                clientes_agregar.apellido = cliente.apellido;
                clientes_agregar.telefono = cliente.telefono;
                clientes_agregar.direccion = cliente.direccion;

                database.clientes.Add(clientes_agregar);
                database.SaveChanges();
            }
        }

        public void editar_cliente(repositorio_clientes cliente,int id)
        {/*Aqui lo que hacemos es que primero buscamos el cliente que vamos a editar
            y despues procedemos con la edicion*/
            using (kid_storeEntities database = new kid_storeEntities())
            {
                cliente cliente_editar = database.clientes.Find(id);
                cliente_editar.nombre = cliente.nombre;
                cliente_editar.apellido = cliente.apellido;
                cliente_editar.telefono = cliente.telefono;
                cliente_editar.direccion = cliente.direccion;

                database.Entry(cliente_editar).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
            }
        }
        public object buscar_cliente(string telefono)
        {
            using (kid_storeEntities database = new kid_storeEntities())
            {
                var list = from b in database.clientes
                           where b.telefono == telefono
                           select b;

                return list.ToList();
            }
        }

        public object load_clientes()
        {
            using (kid_storeEntities database = new kid_storeEntities())
            {
                var clientes = from b in database.clientes
                               select b;

                return clientes.ToList();
            }
        }
        #endregion

        #region mantenimiento productos

        public List<combobox> load_combo_categoria()
        {/*Aqui lo que hacemos es que creamos un listado con el tipo de combobox
            para poder retornar el mismo listado, mas abajo traemos todos los datos de la tabla
            y despues con un bucle foreach creamos un objeto del tipo combobox para despues poder
            llenar el listado y devolverlo*/
            List<combobox> listado = new List<combobox>();
            using (kid_storeEntities database = new kid_storeEntities())
            {
                var combo = from b in database.categoria_productos
                            select b;
                foreach (var item in combo)
                {
                    combobox pan = new combobox
                    {
                        value = item.id,
                        categoria = item.nombre
                    };
                    listado.Add(pan);
                }

                return listado;
            }
        }
        public object load_productos()
        {
            using (kid_storeEntities database = new kid_storeEntities())
            {
                var list = from b in database.vw_productos
                           select b;
                return list.ToList();
            }
        }

        public void agregar_productos(producto producto)
        {
            using (kid_storeEntities database = new kid_storeEntities())
            {
                producto productos_nuevos = new producto();

                productos_nuevos.nombre = producto.nombre;
                productos_nuevos.id_categoria = producto.id_categoria;
                productos_nuevos.precio_compra = producto.precio_compra;
                productos_nuevos.precio_venta = producto.precio_venta;
                productos_nuevos.ganancia = producto.ganancia;
                productos_nuevos.descripcion = producto.descripcion;

                database.productos.Add(productos_nuevos);
                database.SaveChanges();

            }
        }

        public void editar_producto(producto _producto, int id)
        {
            using (kid_storeEntities database = new kid_storeEntities())
            {
                producto producto_editar = database.productos.Find(id);

                producto_editar.nombre = _producto.nombre;
                producto_editar.id_categoria = _producto.id_categoria;
                producto_editar.precio_compra = _producto.precio_compra;
                producto_editar.precio_venta = _producto.precio_venta;
                producto_editar.ganancia = _producto.ganancia;
                producto_editar.descripcion = _producto.descripcion;

                database.Entry(producto_editar).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
            }
        }

        public object buscar_producto( int id,string busqueda)
        {/*Aqui es donde se ven los diferentes metodos de busqueda, el primero es a travez del id
            del producto y el 2do es utilizando el nombre del producto*/
            using (kid_storeEntities database = new kid_storeEntities())
            {
                switch (id)
                {
                    case (int)opciones.id:
                        /*Aqui en vez de convertir directamente busqueda a int lo que hacemos es que lo pasamos
                         a otra variable porque por alguna razon el entity framework despues no lo acepta*/
                        int prod = int.Parse(busqueda);
                        var producto = database.productos.Where(x=> x.id == prod);

                        return producto.ToList();
                        break;

                    case (int)opciones.nombre_producto:
                        var list1 = from b in database.productos
                                    where b.nombre.Contains(busqueda)
                                    select b;
                        return list1.ToList();
                        break;
                }
                return 0;
            }
        }
        #endregion
    }
}
