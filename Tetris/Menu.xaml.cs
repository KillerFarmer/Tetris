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
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window {

        Partida aJugar;

        public Menu() {
            InitializeComponent();
        }

        private void btnJugar_Click(object sender, RoutedEventArgs e) {

            aJugar = new Partida();

            aJugar.Owner = this;
            Hide();
            aJugar.Show();

        }

        private void btnTop_Click(object sender, RoutedEventArgs e) {

            Marcador ventana = new Marcador();

            ventana.Owner = this;
            this.Hide();
            ventana.Show();
        }

        private void btnCreditos_Click(object sender, RoutedEventArgs e) {

            Creditos ventana = new Creditos();

            ventana.Owner = this;
            Hide();
            ventana.Show();
        }

       
    }
}
