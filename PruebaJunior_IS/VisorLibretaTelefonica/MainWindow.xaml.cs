using System.Data;
using System.Windows;
using ControladorLibretaTelefonica;
using Microsoft.Win32;

namespace VisorLibretaTelefonica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IVista
    {        
        Controlador controlador = null;

        private const string msg_busqueda_vacia = "No he encontrado nada";
        private const string msg_err_no_libreta = "No has cargado ninguna libreta";
        private const string msg_err_titulo_ventana = "Algo ha ido mal..";

        public MainWindow()
        {
            InitializeComponent();
            controlador = new Controlador(this);
        }

        #region EVENTOS

        /* Cargar libreta */
        private void button_carga_libreta_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //si no seleccionamos fichero, volvemos.
            if (openFileDialog.ShowDialog() == false) return;

            //leemos libreta
            cargarLibreta(openFileDialog.FileName);
        }

        /* Buscar */
        private void button_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (!controlador.hayLibreta())
            {
                MessageBox.Show(msg_err_no_libreta, msg_err_titulo_ventana, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (tb_buscar.Text == "") return;
                controlador.busca(tb_buscar.Text, cb_columnas.Text);
            } 
            
        }

        /* Listar */
        private void button_listar_Click(object sender, RoutedEventArgs e)
        {
            if (!controlador.hayLibreta())
            {
                MessageBox.Show(msg_err_no_libreta, msg_err_titulo_ventana, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {                
                controlador.imprimirLibreta();
            }
        }

        #endregion

        #region FUNCIONES AUXILIARES

        private void cargarLibreta(string ruta)
        {
            string msg = controlador.initLibreta(ruta);
            if (msg != null) MessageBox.Show(msg, msg_err_titulo_ventana, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        #endregion

        #region INTERFAZ
        public void rellenaSelectorColumna(string[] columnas)
        {
            cb_columnas.ItemsSource = columnas;
            cb_columnas.SelectedIndex = 0;
        }

        public void imprimirLibreta(DataView dv)
        {
            this.visor.ItemsSource = dv;
        }

        public void busquedaVacia()
        {
            MessageBox.Show(msg_busqueda_vacia, msg_err_titulo_ventana, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion
    }
}
