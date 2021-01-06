using System.Data;
using System.IO;

namespace ModeloLibretaTelefonica
{
    public class libretaTelefonica : IModelo
    {
        private DataTable libreta;

        private const string delimitador = "|";
        private const string delimitador_nombres = " ";

        private const string cNombre = "Nombre";
        private const string cApellido = "Apellido";
        private const string cCiudad = "Ciudad";
        private const string cTelefono = "Teléfono";

        public libretaTelefonica(string pathTexto)
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
