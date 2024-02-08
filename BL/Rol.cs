using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static Dictionary<string, object> GetAll()
        {
            ML.Rol rol = new ML.Rol();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Rol", rol }, { "Exepcion", "" }, { "Resultado", false } };
            try
            {
                using (DL.RvazquezMoviesContext context = new DL.RvazquezMoviesContext())
                {
                    var registro = (from registroRol in context.Rols
                                    select new
                                    {
                                        IdRol = registroRol.IdRol,
                                        Nombre = registroRol.Tipo
                                    }).ToList();

                    if (registro != null)
                    {
                        rol.Roles = new List<object>();
                        foreach (var objRol in registro)
                        {
                            ML.Rol rolobj = new ML.Rol();
                            rolobj.IdRol = objRol.IdRol;
                            rolobj.Tipo = objRol.Nombre;

                            rol.Roles.Add(rolobj);
                        }
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
                diccionario["Exepcion"] = ex.Message;
            }
            return diccionario;
        }
    }
}
