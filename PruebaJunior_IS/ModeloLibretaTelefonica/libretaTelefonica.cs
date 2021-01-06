using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace ModeloLibretaTelefonica
{
    public class libretaTelefonica : IModelo
    {
        private DataTable libreta;

        Dictionary<string, List<int>> indice_nombre;
        Dictionary<string, List<int>> indice_apellido;
        Dictionary<string, List<int>> indice_ciudad;
        Dictionary<string, List<int>> indice_telefono;

        private const string delimitador = "|";
        private const string delimitador_nombres = " ";

        private const string cNombre = "Nombre";
        private const string cApellido = "Apellido";
        private const string cCiudad = "Ciudad";
        private const string cTelefono = "Teléfono";

        public libretaTelefonica(string pathTexto)
        {
            creaDataTable(pathTexto);
            creaIndices();
        }

        private void creaIndices()
        {
            indice_nombre = new Dictionary<string, List<int>>();
            indice_apellido = new Dictionary<string, List<int>>();
            indice_ciudad = new Dictionary<string, List<int>>();
            indice_telefono = new Dictionary<string, List<int>>();

            int idx = 0;
            foreach (DataRow row in this.libreta.Rows)
            { 
                agregaReferencia(indice_nombre, row[0].ToString(), idx);
                agregaReferencia(indice_apellido, row[1].ToString(), idx);
                agregaReferencia(indice_ciudad, row[2].ToString(), idx);
                agregaReferencia(indice_telefono, row[3].ToString(), idx);
                idx++;
            }

        }

        private void agregaReferencia(Dictionary<string, List<int>> indice, string key, int idx)
        {
            List<int> aux;
            if (indice.ContainsKey(key))                 
                indice.GetValueOrDefault(key).Add(idx);
            else
            {
                aux = new List<int>();
                aux.Add(idx);
                indice.Add(key, aux);
            } 
        }        

        private void creaDataTable(string pathTexto)
        {
            libreta = new DataTable();
            StreamReader sr = new StreamReader(pathTexto);
            string delimitadores = delimitador + delimitador_nombres;

            //instanciamos columnas
            libreta.Columns.Add(cNombre);
            libreta.Columns.Add(cApellido);
            libreta.Columns.Add(cCiudad);
            libreta.Columns.Add(cTelefono);

            //instanciamos filas
            while (!sr.EndOfStream)
            {
                string[] row = sr.ReadLine().Split(delimitadores.ToCharArray());
                DataRow dr = libreta.NewRow();

                for (int i = 0; i < libreta.Columns.Count; i++)
                {
                    dr[i] = row[i];
                }

                libreta.Rows.Add(dr);
            }
        }

        public DataTable consultaTabla()
        {
            return this.libreta;
        }

        public string[] consultaEncabezados()
        {
            return new string[] { cNombre, cApellido, cCiudad, cTelefono };
        }

        public DataRow consultaFila(int idx)
        {
            return this.libreta.Rows[idx];
        }
    }
}
