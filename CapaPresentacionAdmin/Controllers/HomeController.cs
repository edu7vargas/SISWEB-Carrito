using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Usuarios()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarUsuarios() {

            List<Usuario> oLista = new List<Usuario>();

            oLista = new CN_Usuarios().Listar();

            return Json(new { data = oLista, status = true, message="" }, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public JsonResult GuardarUsuario(Usuario oUsuario)
        {
            object resultado;
            bool status = false;
            string mensaje = string.Empty;

            if (oUsuario.IdUsuario == 0)
            {
                resultado = new CN_Usuarios().Registrar(oUsuario, out mensaje);
                if ((int)resultado>0)
                {
                    // NO hay ninguna validación pendiente de la CAPA DE NEGOCIOS
                    status = true;
                }
            }
            else {

                resultado = new CN_Usuarios().Editar(oUsuario, out mensaje);
                if ((bool)resultado==true)
                {
                    // NO hay ninguna validación pendiente de la CAPA DE NEGOCIOS
                    status = true;
                }

            }

            return Json(new { data = oUsuario, status = status, message = mensaje }, JsonRequestBehavior.AllowGet);

        }





        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);
            
            Usuario data = new Usuario();// envio un usuario vacio para 

            return Json(new { data = data, status = respuesta, message = mensaje }, JsonRequestBehavior.AllowGet);

        }





        }
}