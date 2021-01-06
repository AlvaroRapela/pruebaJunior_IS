using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.Win32;
using ModeloLibretaTelefonica;

namespace VisorLibretaTelefonica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        libretaTelefonica libreta;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_carga_libreta_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == false) return; //si no seleccionamos fichero, salimos.

            if (cargarLibreta(openFileDialog.FileName)) imprimirLibreta();
        }

        private bool cargarLibreta(string ruta)
        {
            try
            {
                libreta = new libretaTelefonica(ruta);
            }
            catch (IOException error)
            {
                MessageBox.Show(error.Message, "Algo ha ido mal..", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void imprimirLibreta()
        {
            this.visor.ItemsSource = libreta.consultaTabla().DefaultView;
        }

        private void button_buscar_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
