using ModeloLibretaTelefonica;
using System.Data;
using VisorLibretaTelefonica;

namespace ControladorLibretaTelefonica
{
    interface IControlador
    {
        public string initLibreta(string path);

        public bool hayLibreta();

        public void imprimirLibreta();

        public void busca(string key, string columna);

    }
}
