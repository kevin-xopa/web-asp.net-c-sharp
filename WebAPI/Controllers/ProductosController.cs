using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Productos.DAL;
using Productos.Entities;

namespace WebAPI.Controllers
{
    public class ProductosController : ApiController
    {
        Datos datos = new Datos();
        List<Productos.Entities.Producto> DatosProducts;

        [HttpGet]
        public List<Productos.Entities.Producto> ListProducts()
        {
            DatosProducts = datos.ListaProductos();
            datos.liberarConexion();
            return DatosProducts;
        }

    }
}
