﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using CapaDatos;


namespace CapaNegocio
{
    public class CN_Carrito
    {

        private CD_Carrito objCapaDato = new CD_Carrito();
        public bool ExisteCarrito(int idcliente, int idproducto, out string Mensaje)
        {
            return objCapaDato.ExisteCarrito(idcliente, idproducto, out Mensaje);
        }

        public bool OperacionCarrito(int idcliente, int idproducto, bool sumar, out string Mensaje) {
            return objCapaDato.OperacionCarrito(idcliente, idproducto, sumar, out Mensaje);
        }

        public int CantidadEnCarrito(int idcliente) {
            return objCapaDato.CantidadEnCarrito(idcliente);
        }

        public List<Carrito> ListarProducto(int idcliente) {
            return objCapaDato.ListarProducto(idcliente);
        }


        public bool EliminarCarrito(int idcliente, int idproducto, out string Mensaje) {
            return objCapaDato.EliminarCarrito(idcliente, idproducto, out Mensaje);
        }



    }
}
