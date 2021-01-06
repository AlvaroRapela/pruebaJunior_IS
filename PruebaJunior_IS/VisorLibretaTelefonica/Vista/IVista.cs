using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VisorLibretaTelefonica
{
    interface IVista
    {
        public void rellenaSelectorColumna(string[] columnas);

        public void imprimirLibreta(DataView dv);

    }
}
