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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Tetris {
    /// <summary>
    /// Lógica de interacción para Marcador.xaml
    /// </summary>
    /// Este archivo tiene como unica funcion mostrar los puntajes almacenados.
    public partial class Marcador : Window {

        List<Score> registroPuntos;

        public Marcador() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            FileInfo archivo = new FileInfo("marcador.xml");

            if (archivo.Exists) {

                XmlSerializer xs = new XmlSerializer((new List<Score>()).GetType());
                StreamReader sw = new StreamReader("marcador.xml");

                registroPuntos = (List<Score>)xs.Deserialize(sw);
                lbxTop.ItemsSource = registroPuntos;
                sw.Close();

            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

            Owner.Show();
        }
    }
}
