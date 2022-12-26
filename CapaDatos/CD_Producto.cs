using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Producto
    {

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            string Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT p.IdProducto,p.Nombre,p.Descripcion,p.IdMarca,p.IdCategoria,p.Precio,p.Stock,p.RutaImagen,p.NombreImagen,p.Activo,m.Descripcion[DescMarca],c.Descripcion[DescCategoria] FROM PRODUCTO p");
                    sb.AppendLine("inner join CATEGORIA c ON p.IdCategoria = c.IdCategoria");
                    sb.AppendLine("inner join MARCA m ON p.IdMarca = m.IdMarca");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new Producto()
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    //IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                    //IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                    oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"]), Descripcion = dr["DescMarca"].ToString() },
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DescCategoria"].ToString() },
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    RutaImagen = dr["RutaImagen"].ToString(),
                                    NombreImagen = dr["NombreImagen"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"])

                                }



                                );

                        }

                    }


                }


            }
            catch(Exception ex)
            {
                Mensaje = ex.Message;
                lista = new List<Producto>();
            }


            return lista;
        }







        public int Registrar(Producto obj, out string Mensaje)
        {

            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ins_producto", oconexion);

                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    //cmd.Parameters.AddWithValue("RutaImagen", obj.RutaImagen);
                    //cmd.Parameters.AddWithValue("NombreImagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }

            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;

            }

            return idautogenerado;



        }




        public bool Editar(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_upd_producto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    //cmd.Parameters.AddWithValue("RutaImagen", obj.RutaImagen);
                    //cmd.Parameters.AddWithValue("NombreImagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;

            }

            return resultado;

        }




        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_del_producto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;

            }

            return resultado;

        }



        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {

            bool resultado = false;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "update producto set RutaImagen = @RutaImagen, NombreImagen = @NombreImagen where IdProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@RutaImagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue("@NombreImagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("@IdProducto", obj.IdProducto);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else {
                        Mensaje = "No se puedo actulizar imagen.";
                    }

                   
                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;

            }

            return resultado;


        }




    }
}

