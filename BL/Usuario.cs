using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static Dictionary<string, object> Add (ML.Usuario usuario)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using (DL.RvazquezMoviesContext context = new DL.RvazquezMoviesContext())
                {
                    var filasAfectadas = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.UserName}', '{usuario.Email}', '{usuario.Password}', {usuario.Rol.IdRol}");

                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
        public static Dictionary<string, object> Update(ML.Usuario usuario)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using (DL.RvazquezMoviesContext context = new DL.RvazquezMoviesContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.UserName}', '{usuario.Email}', '{usuario.Password}', {usuario.Rol.IdRol}");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
        public static Dictionary<string, object> Delete(int idUsuario)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using (DL.RvazquezMoviesContext context = new DL.RvazquezMoviesContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"UsuarioDelete {idUsuario}");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
        public static Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using (DL.RvazquezMoviesContext context = new DL.RvazquezMoviesContext())
                {

                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
        public static Dictionary<string, object> GetById(int idUsuario)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using (DL.RvazquezMoviesContext context = new DL.RvazquezMoviesContext())
                {

                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
    }
}