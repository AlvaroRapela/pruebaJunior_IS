using System.IO;
using ModeloLibretaTelefonica;
using VisorLibretaTelefonica;

namespace ControladorLibretaTelefonica
{
    public class Controlador : IControlador
    {
        LibretaTelefonica libreta = null;
        MainWindow vista = null;

        public Controlador(MainWindow vista)
        {
            this.vista = vista;
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

            //si ha ido bien, rellenamos el CB que sirve de selector de columna para las busquedas. E imprimimos la libreta. 
            vista.rellenaSelectorColumna(libreta.consultaColIndices());
            imprimirLibreta();

            return null;
        }

        public void busca(string key, string columna)
        {
            //consultamos indices
            int[] indices = libreta.consultaIndices(key, columna);

            //imprimimos resultado
            if (indices == null)    vista.busquedaVacia();
            else                    vista.imprimirLibreta(libreta.consultaFilas(indices).DefaultView);
            
        }

        public bool hayLibreta()
        {
            return libreta != null;
        }

        public void imprimirLibreta()
        {
            vista.imprimirLibreta(libreta.consultaTabla().DefaultView);
        }
    }
}
