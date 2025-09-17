using MiBotica.SolPedido.AccesoDatos.Core;
using MiBotica.SolPedido.Entidades.Core;
using MiBotica.SolPedido.Entidades.Base;
using System;
using System.Collections.Generic;

namespace MiBotica.SolPedido.LogicaNegocio.Core
{
    public class UsuarioLN : BaseLN
    {
        public List<Usuario> ListaUsuarios()
        {
            try
            {
                return new UsuarioDA().ListaUsuarios();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertarUsuario(Usuario usuario)
        {
            try
            {
                new UsuarioDA().InsertarUsuario(usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            try
            {
                return new UsuarioDA().ObtenerUsuarioPorId(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            try
            {
                new UsuarioDA().ActualizarUsuario(usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public void EliminarUsuario(int id)
        {
            try
            {
                new UsuarioDA().EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
    }
}
