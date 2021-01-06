using System.Data;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using ModeloLibretaTelefonica;

namespace VisorLibretaTelefonica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        libretaTelefonica libreta = null;
        
        private const string msg_err_no_libreta = "No has cargado ninguna libreta";
        private const string msg_err_titulo_ventana = "Algo ha ido mal..";

        public MainWindow()
        {
            InitializeComponent();
        }

        #region EVENTOS

        /* Cargar libreta */
        private void button_carga_libreta_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == false) return; //si no seleccionamos fichero, salimos.

            if (cargarLibreta(openFileDialog.FileName))
            {
                imprimirLibreta();
            }
        }

        /* Buscar */
        private void button_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (!hayLibreta())
            {
                MessageBox.Show(msg_err_no_libreta, msg_err_titulo_ventana, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int[] idxs = { 0, 49, 99 };
                imprimirLibreta(idxs);
            } 
            
        }

        /* Listar */
        private void button_listar_Click(object sender, RoutedEventArgs e)
        {
            if (!hayLibreta())
            {
                MessageBox.Show(msg_err_no_libreta, msg_err_titulo_ventana, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {                
                imprimirLibreta();
            }
        }

        #endregion

        #region FUNCIONES AUXILIARES

        private bool cargarLibreta(string ruta)
        {
            try
            {
                libreta = new libretaTelefonica(ruta);
            }
            catch (IOException error)
            {
                MessageBox.Show(error.Message, msg_err_titulo_ventana, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void imprimirLibreta(int[] idxs=null)
        {     

            if (idxs == null)
                this.visor.ItemsSource = libreta.consultaTabla().DefaultView;
            else
            {
                DataTable dt = creaDT();
                foreach(int idx in idxs)
                {
                    dt.Rows.Add(libreta.consultaFila(idx).ItemArray);
                }

                this.visor.ItemsSource = dt.DefaultView;
            }
        }

        private DataTable creaDT()
        {
            DataTable dt = new DataTable();
            string[] columnas = libreta.consultaEncabezados();

            foreach(string col in columnas)
            {
                dt.Columns.Add(col);
            }

            return dt;
        } 

        private bool hayLibreta()
        {
            return this.libreta != null;
        }



        #endregion
    }
}
