using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ModeloLibretaTelefonica
{
    interface IModelo
    {
        public DataTable consultaTabla();

        public DataTable consultaFilas(int[] idxs);

        public int[] consultaIndices(string key, string columna);

        public string[] consultaColIndices();
               
    }
}
