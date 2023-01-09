using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;

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





        [HttpGet]
        public JsonResult ListaReporte(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reporte> oLista = new List<Reporte>();

            bool respuesta = true;
            string mensaje = string.Empty;

            oLista = new CN_Reporte().Ventas(fechainicio, fechafin, idtransaccion);

            return Json(new { data = oLista, status = respuesta, message = mensaje }, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public JsonResult VistaDashboard()
        {
            bool respuesta = true;
            string mensaje = string.Empty;

            Dashboard objeto = new CN_Reporte().VerDashboard();
            
            return Json(new { data = objeto, status = respuesta, message = mensaje }, JsonRequestBehavior.AllowGet);
        }






        [HttpPost]
        public FileResult ExportarVenta(string fechainicio, string fechafin, string idtransaccion)
        {

            List<Reporte> oLista = new List<Reporte>();
            oLista = new CN_Reporte().Ventas(fechainicio, fechafin, idtransaccion);

            DataTable dt = new DataTable();

            dt.Locale = new System.Globalization.CultureInfo("es-PE");
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Cantidad", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("IdTransacción", typeof(string));


            foreach (Reporte rp in oLista)
            {
                dt.Rows.Add(new object[] {
                    rp.FechaVenta,
                    rp.Cliente,
                    rp.Producto,
                    rp.Precio,
                    rp.Cantidad,
                    rp.Total,
                    rp.IdTrasaccion
               });
            }


            dt.TableName = "Datos";



            using (XLWorkbook wb = new XLWorkbook()){

                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                }

            
            }


        }








        }
}