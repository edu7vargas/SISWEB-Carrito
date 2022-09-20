using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
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




    }
}