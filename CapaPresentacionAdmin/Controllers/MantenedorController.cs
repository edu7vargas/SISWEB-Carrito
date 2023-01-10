using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;

namespace CapaPresentacionAdmin.Controllers
{
    //[Authorize]
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }
                
        public ActionResult Marca()
        {
            return View();
        }

        
        public ActionResult Producto()
        {
            return View();
        }



        // ************* CATEGORIAS *************

        #region CATEGORIAS

        [HttpGet]
        public JsonResult ListarCategorias()
        {

            List<Categoria> oLista = new List<Categoria>();

            oLista = new CN_Categoria().Listar();

            return Json(new { data = oLista, status = true, message = "" }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GuardarCategoria(Categoria oCategoria)
        {
            object resultado;
            bool status = false;
            string mensaje = string.Empty;

            if (oCategoria.IdCategoria == 0)
            {
                resultado = new CN_Categoria().Registrar(oCategoria, out mensaje);
                if ((int)resultado > 0)
                {
                    // NO hay ninguna validación pendiente de la CAPA DE NEGOCIOS
                    status = true;
                    oCategoria.IdCategoria = (int)resultado;
                }
            }
            else
            {

                resultado = new CN_Categoria().Editar(oCategoria, out mensaje);
                if ((bool)resultado == true)
                {
                    // NO hay ninguna validación pendiente de la CAPA DE NEGOCIOS
                    status = true;
                }

            }

            return Json(new { data = oCategoria, status = status, message = mensaje }, JsonRequestBehavior.AllowGet);

        }





        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Categoria().Eliminar(id, out mensaje);

            Categoria data = new Categoria();// envio un usuario vacio para 

            return Json(new { data = data, status = respuesta, message = mensaje }, JsonRequestBehavior.AllowGet);

        }
        #endregion





        // ************* MARCAS *************

        #region MARCAS
        [HttpGet]
        public JsonResult ListarMarcas()
        {

            List<Marca> oLista = new List<Marca>();

            oLista = new CN_Marca().Listar();

            return Json(new { data = oLista, status = true, message = "" }, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public JsonResult GuardarMarca(Marca oMarca)
        {
            object resultado;
            bool status = false;
            string mensaje = string.Empty;

            if (oMarca.IdMarca == 0)
            {
                resultado = new CN_Marca().Registrar(oMarca, out mensaje);
                if ((int)resultado > 0)
                {
                    // NO hay ninguna validación pendiente de la CAPA DE NEGOCIOS
                    status = true;
                    oMarca.IdMarca = (int)resultado;
                }
            }
            else
            {

                resultado = new CN_Marca().Editar(oMarca, out mensaje);
                if ((bool)resultado == true)
                {
                    // NO hay ninguna validación pendiente de la CAPA DE NEGOCIOS
                    status = true;
                }

            }
            
            return Json(new { data = oMarca, status = status, message = mensaje }, JsonRequestBehavior.AllowGet);

        }





        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Marca().Eliminar(id, out mensaje);

            Marca data = new Marca();// envio un usuario vacio para 

            return Json(new { data = data, status = respuesta, message = mensaje }, JsonRequestBehavior.AllowGet);

        }

        #endregion







        // ************* PRODUCTO *************

        #region PRODUCTO
        [HttpGet]
        public JsonResult ListarProductos()
        {

            List<Producto> oLista = new List<Producto>();

            oLista = new CN_Producto().Listar();

            return Json(new { data = oLista, status = true, message = "" }, JsonRequestBehavior.AllowGet);
        }






        [HttpPost]
        public JsonResult GuardarProducto(string objeto,HttpPostedFileBase archivoImagen)
        {
            object resultado;
            string mensaje = string.Empty;
            bool status = false;
            bool guardar_imagen_exitoso = true;


            // convertir string en objeto producto
            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);


            // Validar Precio
            decimal precio;
            if (decimal.TryParse(oProducto.PrecioTexto, System.Globalization.NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
            {
                oProducto.Precio = precio;
            }
            else {
                return Json(new { status = false, mensaje = "El formato del precio debe ser ##.##" },JsonRequestBehavior.AllowGet);
            }


            if (oProducto.IdProducto == 0)
            {
                resultado = new CN_Producto().Registrar(oProducto, out mensaje);
                if ((int)resultado > 0)
                {
                    // NO hay ninguna validación pendiente de la CAPA DE NEGOCIOS
                    status = true;
                    oProducto.IdProducto = (int)resultado;
                }
            }
            else
            {
                resultado = new CN_Producto().Editar(oProducto, out mensaje);
                if ((bool)resultado == true)
                {
                    // NO hay ninguna validación pendiente de la CAPA DE NEGOCIOS
                    status = true;
                }
            }



            if (status)
            {
                if (archivoImagen != null)
                {
                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProducto.IdProducto.ToString(), extension);
                   
                    try {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                        
                    }
                    catch (Exception ex) {

                        mensaje = ex.Message.ToString();
                        guardar_imagen_exitoso = false;
                    }


                    if (guardar_imagen_exitoso)
                    {
                        oProducto.RutaImagen = ruta_guardar;
                        oProducto.NombreImagen = nombre_imagen;
                        status = new CN_Producto().GuardarDatosImagen(oProducto, out mensaje);

                    }
                    else {

                        mensaje = "Se guardo el producto, pero hay problemas con la imagen";
                    }


                }
            
            }


            return Json(new { data = oProducto, status = status, message = mensaje }, JsonRequestBehavior.AllowGet);

        }

        private object Json()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Producto().Eliminar(id, out mensaje);

            Marca data = new Marca();// envio un usuario vacio para 

            return Json(new { data = data, status = respuesta, message = mensaje }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult ImagenProducto(int id) {

            bool conversion;
            Producto oProducto = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();

            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen,oProducto.NombreImagen), out conversion);


            return Json(new 
                { 
                    conversion = conversion,
                    textobase64 = textoBase64,
                    extension = Path.GetExtension(oProducto.NombreImagen)
                },
                JsonRequestBehavior.AllowGet
            
            );

        }




        #endregion


    }
}