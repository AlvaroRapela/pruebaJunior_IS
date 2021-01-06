using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ModeloLibretaTelefonica
{
    public class LibretaTelefonica : IModelo
    {
        private DataTable libreta;

        Dictionary<string, List<int>> indice_nombre;
        Dictionary<string, List<int>> indice_apellido;
        Dictionary<string, List<int>> indice_ciudad;

        private const string delimitador = "|";
        private const string delimitador_nombres = " ";

        private const string cNombre = "Nombre";
        private const string cApellido = "Apellido";
        private const string cCiudad = "Ciudad";
        private const string cTelefono = "Teléfono";

        public LibretaTelefonica(string pathTexto)
        {
            creaDataTable(pathTexto);
            creaIndices();
        }

        #region INTERFAZ

        public DataTable consultaTabla()
        {
            return this.libreta;
        }

        public DataTable consultaFilas(int[] idxs)
        {
            DataTable dt = creaDtVacia();//instanciamos encabezados

            foreach (int idx in idxs)
            {
                dt.Rows.Add(this.libreta.Rows[idx].ItemArray);
            }

            return dt;
        }

        public int[] consultaIndices(string key, string columna)
        {
            List<int> indices = null;

            switch (columna)
            {
                case cNombre: indice_nombre.TryGetValue(key, out indices); break;
                case cApellido: indice_apellido.TryGetValue(key, out indices); break;
                case cCiudad: indice_ciudad.TryGetValue(key, out indices); break;
                default: break;
            }

            //hemos encontrado algo?
            if (indices == null) return null;
            else                 return indices.ToArray();

        }

        public string[] consultaColIndices()
        {
            //devolvemos aquellas columnas que se les efectuan busquedas.
            return new string[] { cNombre, cApellido, cCiudad };
        }

        #endregion

        #region AUXILIARES

        private void creaIndices()
        {
            //registramos los campos, con sus posiciones en la libreta
            indice_nombre = new Dictionary<string, List<int>>();
            indice_apellido = new Dictionary<string, List<int>>();
            indice_ciudad = new Dictionary<string, List<int>>();

            int idx = 0;
            foreach (DataRow row in this.libreta.Rows)
            {
                agregaReferencia(indice_nombre, row[0].ToString(), idx);
                agregaReferencia(indice_apellido, row[1].ToString(), idx);
                agregaReferencia(indice_ciudad, row[2].ToString(), idx);
                idx++;
            }

        }

        private void agregaReferencia(Dictionary<string, List<int>> indice, string key, int idx)
        {
            List<int> aux;
            if (indice.ContainsKey(key))
                indice.GetValueOrDefault(key).Add(idx);//añadimos nuevo indice
            else
            {
                //añadimos nuevo registro
                aux = new List<int>();
                aux.Add(idx);
                indice.Add(key, aux);
            }
        }

        private void creaDataTable(string pathTexto)
        {
            libreta = new DataTable();
            StreamReader sr = new StreamReader(pathTexto);

            //instanciamos columnas
            libreta.Columns.Add(cNombre);
            libreta.Columns.Add(cApellido);
            libreta.Columns.Add(cCiudad);
            libreta.Columns.Add(cTelefono);

            //instanciamos filas
            while (!sr.EndOfStream)
            {
                string linea = sr.ReadLine();
                string[] row = creaFila(linea);//separar los tokens

                //rellenamos cada campo de la fila
                DataRow dr = libreta.NewRow();
                for (int i = 0; i < libreta.Columns.Count; i++)
                {
                    dr[i] = row[i];
                }

                libreta.Rows.Add(dr);
            }
        }

        private DataTable creaDtVacia()
        {
            DataTable dt = new DataTable();

            //instanciamos columnas
            dt.Columns.Add(cNombre);
            dt.Columns.Add(cApellido);
            dt.Columns.Add(cCiudad);
            dt.Columns.Add(cTelefono);

            return dt;
        }

        private string[] creaFila(string linea)
        {
            // Las ciudades pueden ser del tipo 'palma de mallorca'. 
            // Por tanto, el separador ' ', sólo hay que aplicárselo al campo que contiene el nombre y el apellido.

            string[] row = new string[4];
            string[] aux = linea.Split(delimitador);

            //rellenamos datos
            row[0] = aux[0].Split(delimitador_nombres)[0];
            row[1] = aux[0].Split(delimitador_nombres)[1];
            row[2] = aux[1];
            row[3] = aux[2];

            return row;
        }

        #endregion


    }
}
