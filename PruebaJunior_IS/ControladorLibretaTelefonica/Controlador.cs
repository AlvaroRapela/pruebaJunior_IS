using System.Collections.Generic;
using System.Data;
using System.IO;
using ModeloLibretaTelefonica;

namespace ControladorLibretaTelefonica
{
    public class Controlador : IControlador
    {
        LibretaTelefonica libreta;
        VisorLibretaTelefonica vista;

        public DataTable busca(string key, string columna)
        {
            throw new System.NotImplementedException();
        }

        public string initLibreta(string path)
        {
            try
            {
                libreta = new LibretaTelefonica(path);
            }
            catch (IOException error)
            {                
                return error.Message;
            }



            return null;
        }
    }
}
