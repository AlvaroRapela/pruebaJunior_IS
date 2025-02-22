﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ModeloLibretaTelefonica
{
    interface IModelo
    {
        public DataTable consultaTabla();

        public DataRow consultaFila(int idx);

        public DataTable consultaFilas(int[] idx);

        public List<int> consultaIndices(string key, string columna);

        public string[] consultaEncabezados();
               
    }
}
