﻿using ModeloLibretaTelefonica;
using System.Data;

namespace ControladorLibretaTelefonica
{
    interface IControlador
    {
        public string initLibreta(string path);
        public DataTable busca(string key, string columna);
    }
}
