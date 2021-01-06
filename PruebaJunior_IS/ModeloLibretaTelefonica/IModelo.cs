using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ModeloLibretaTelefonica
{
    interface IModelo
    {
        public DataTable consultaTabla();
        
    }
}
