using MiBotica.SolPedido.Entidades.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MiBotica.SolPedido.AccesoDatos.Core
{
    public class UsuarioDA
    {
        public List<Usuario> ListaUsuarios()
        {
            List<Usuario> listaEntidad = new List<Usuario>();
            Usuario entidad = null;

            using (SqlConnection conexion = new SqlConnection(
                ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cmSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuarioLista", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        entidad = LlenaEntidad(reader);
                        listaEntidad.Add(entidad);
                    }
                    conexion.Close();
                }
            }
            return listaEntidad;
        }

        public Usuario LlenaEntidad(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();

            // Verificar y asignar cada campo
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='IdUsuario'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["IdUsuario"]))
                    usuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='CodUsuario'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["CodUsuario"]))
                    usuario.CodUsuario = Convert.ToString(reader["CodUsuario"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='Clave'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["Clave"]))
                    usuario.Clave = (byte[])reader["Clave"];
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='Nombres'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["Nombres"]))
                    usuario.Nombres = Convert.ToString(reader["Nombres"]);
            }

            return usuario;
        }

        public void InsertarUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cmSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuarioInsertar", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@CodUsuario", usuario.CodUsuario);
                    comando.Parameters.AddWithValue("@Clave", usuario.Clave);
                    comando.Parameters.AddWithValue("@Nombres", usuario.Nombres);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            Usuario usuario = null;

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cmSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuarioObtenerPorId", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdUsuario", id);

                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario = LlenaEntidad(reader);
                    }
                }
                conexion.Close();
            }
            return usuario;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cmSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuarioActualizar", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    comando.Parameters.AddWithValue("@CodUsuario", usuario.CodUsuario);
                    comando.Parameters.AddWithValue("@Clave", usuario.Clave);
                    comando.Parameters.AddWithValue("@Nombres", usuario.Nombres);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

        public void EliminarUsuario(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cmSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuarioEliminar", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdUsuario", id);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }
    }
}