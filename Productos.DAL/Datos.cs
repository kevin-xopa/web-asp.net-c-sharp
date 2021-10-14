using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Productos;

namespace Productos.DAL
{
    public class Datos
    {
        string conexion;
        SqlConnection carretera;
        public void conectar()
        {
            conexion = ConfigurationManager.ConnectionStrings["ConectaProducto"].ConnectionString;
            carretera = new SqlConnection(conexion);
        }
        public void liberarConexion()
        {
            carretera.Close();
        }
        public List<Productos.Entities.Producto> ListaProductos()
        {
            string consulta = "";
            DataRow fila;
            List<Productos.Entities.Producto> ListProducto = new List<Productos.Entities.Producto>();
            DataSet Ds = new DataSet();
            SqlCommand dbcommnad = new SqlCommand();
            conectar();
            consulta = "select * from Producto";
            dbcommnad.CommandText = consulta;
            dbcommnad.Connection = carretera;


            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = dbcommnad;


            try
            {
                adapter.Fill(Ds);
                DataTable tabla = Ds.Tables[0];


                int i = 0;
                while (i <= tabla.Rows.Count - 1)
                {
                    fila = tabla.Rows[i];
                    Productos.Entities.Producto Pr = new Productos.Entities.Producto
                    {
                        id = Convert.ToInt16(fila["id"]),
                        nombre = fila["nombre"].ToString(),
                        descripcion = fila["descripcion"].ToString()


                    };
                    ListProducto.Add(Pr);
                    ++i;
                }
                dbcommnad.Connection.Close();
                dbcommnad.Dispose();
                adapter.Dispose();
            }
            catch (Exception h)
            {
                Productos.Entities.Producto Pro = new Productos.Entities.Producto
                {
                    mensaje = "Ha ocurrido un error" + h.Message// al acceder a la Base de Datos",
                };
                ListProducto.Add(Pro);
            }
            return ListProducto;
        }

    }
}
