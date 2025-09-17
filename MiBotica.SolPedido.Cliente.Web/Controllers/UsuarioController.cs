using MiBotica.SolPedido.Entidades.Core;
using MiBotica.SolPedido.LogicaNegocio.Core;
using MiBotica.SolPedido.Utiles.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MiBotica.SolPedido.Cliente.Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = new UsuarioLN().ListaUsuarios();
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            Usuario usuario = new Usuario();
            return View(usuario); // Pasar el objeto usuario a la vista
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                // DEBUG: Verificar que los datos lleguen
                if (usuario == null)
                {
                    ModelState.AddModelError("", "El objeto usuario es nulo");
                    return View(usuario);
                }

                if (string.IsNullOrEmpty(usuario.ClaveTexto))
                {
                    ModelState.AddModelError("", "La contraseña está vacía");
                    return View(usuario);
                }

                // DEBUG: Verificar encriptación
                byte[] claveEncriptada = EncriptacionHelper.EncriptarByte(usuario.ClaveTexto);
                if (claveEncriptada == null || claveEncriptada.Length == 0)
                {
                    ModelState.AddModelError("", "Error al encriptar la contraseña");
                    return View(usuario);
                }

                usuario.Clave = claveEncriptada;

                // DEBUG: Verificar inserción
                new UsuarioLN().InsertarUsuario(usuario);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Mensaje más específico
                ModelState.AddModelError("", "Error al crear el usuario: " + ex.Message + " - " + ex.InnerException?.Message);
                return View(usuario);
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Usuario usuario = new UsuarioLN().ObtenerUsuarioPorId(id);

                if (usuario == null)
                {
                    return HttpNotFound();
                }
                usuario.ClaveTexto = ""; // Dejar vacío para que el usuario ingrese nueva contraseña

                return View(usuario);
            }
            catch (Exception ex)
            {
                // Log del error
                return View("Error");
            }
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Usuario usuario)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    // Si se ingresó nueva contraseña, encriptarla
                    if (!string.IsNullOrEmpty(usuario.ClaveTexto))
                    {
                        usuario.Clave = EncriptacionHelper.EncriptarByte(usuario.ClaveTexto);
                    }
                    else
                    {
                        // Mantener la contraseña actual si no se cambió
                        Usuario usuarioActual = new UsuarioLN().ObtenerUsuarioPorId(id);
                        usuario.Clave = usuarioActual.Clave;
                    }

                    usuario.IdUsuario = id; // Asegurar que el ID sea correcto
                    new UsuarioLN().ActualizarUsuario(usuario);

                    return RedirectToAction("Index");
                }

                return View(usuario);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar usuario: " + ex.Message);
                return View(usuario);
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID de usuario es requerido");
            }

            try
            {
                Usuario usuario = new UsuarioLN().ObtenerUsuarioPorId(id.Value);

                if (usuario == null)
                {
                    return HttpNotFound();
                }

                return View(usuario);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al cargar usuario: " + ex.Message);
                return View("Error");
            }
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                new UsuarioLN().EliminarUsuario(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar usuario: " + ex.Message);

                // Volver a cargar el usuario para mostrar en la vista
                Usuario usuario = new UsuarioLN().ObtenerUsuarioPorId(id);
                return View("Delete", usuario);
            }
        }
    }
}