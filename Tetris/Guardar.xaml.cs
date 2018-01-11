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
    /// Lógica de interacción para Guardar.xaml
    /// </summary>
    public partial class Guardar : Window {

        public int hrs { get; set; }
        public int mins { get; set; }
        public int segs { get; set; }
        public int puntos { get; set; }
        Score nuevoPuntaje;

        public Guardar() {
            InitializeComponent();
        }

        public void ponerDatos() {

            //Poner los datos en la ventana antes de que esta aparezca
            lblPuntos.Content = lblPuntos.Content.ToString() + puntos;

            string tiempo = "";

            if (hrs < 10) {
                tiempo += "0" + hrs + ":";
            } else {
                tiempo += hrs + ":";
            }

            if (mins < 10) {
                tiempo += "0" + mins + ":";
            } else {
                tiempo += mins + ":";
            }

            if (segs < 10) {
                tiempo += "0" + segs + ":";
            } else {
                tiempo += segs;
            }

            lblTiempo.Content = tiempo;
            //
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

            if (!esValido()) {
                nuevoPuntaje = new Score("jojos");

            } else {
                nuevoPuntaje = new Score(txtNombre.Text);
            }

            nuevoPuntaje.horas = hrs;
            nuevoPuntaje.minutos = mins;
            nuevoPuntaje.segundos = segs;
            nuevoPuntaje.puntos = puntos;

            FileInfo archivo = new FileInfo("marcador.xml");

            //Guardar archivo
            //Si el archivo de guardado existe
            if (archivo.Exists) {

                XmlSerializer xs = new XmlSerializer((new List<Score>().GetType()));
                
                StreamReader sr = new StreamReader("marcador.xml");
                
                List<Score> registroPuntos = (List<Score>)xs.Deserialize(sr);
                sr.Close();
                Score temporal = new Score();

                //Si hay  menos de 10 marcadores guardados
                if (registroPuntos.Count <  10) {

                    //Agregalo a la lista
                    registroPuntos.Add(nuevoPuntaje);

                    //Ordena la lista por metodo burbuja.
                    for (int i = 0; i < registroPuntos.Count - 1; i++) {

                        for (int j = i + 1;  j < registroPuntos.Count; j++) {

                            if (registroPuntos[j].puntos > registroPuntos[i].puntos) {

                                temporal = registroPuntos[i];
                                registroPuntos[i] = registroPuntos[j];
                                registroPuntos[j] = temporal;
                            }
                            
                        }
                    }

                    //Si hay mas de 10 puntajes.
                    //Agregamelo si solo si supera alguno de los puntajes existentes.
                } else {

                    for (int i = 0; i < 10; i++) {

                        if (nuevoPuntaje.puntos > registroPuntos[i].puntos) {
                            temporal = registroPuntos[i];
                            registroPuntos[i] = nuevoPuntaje;
                            nuevoPuntaje = temporal;
                            
                        }

                    }
                }

                //Guarda el marcador.
                StreamWriter sw = new StreamWriter("marcador.xml");
                xs.Serialize(sw, registroPuntos);
                sw.Close();
                
            
            //Si el archivo de puntaje no existe.
            //Creame un nuevo archivo, agregame el puntaje y guarda el marcador.
            } else {

                XmlSerializer xs = new XmlSerializer((new List<Score>().GetType()));
                StreamWriter sw = new StreamWriter("marcador.xml");
                List<Score> registroPuntos = new List<Score>();
                registroPuntos.Add(nuevoPuntaje);

                xs.Serialize(sw, registroPuntos);

                sw.Close();
            }

            //Cierra la ventana 'Partida'
            Owner.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e) {

            if (esValido()) {

                this.Close();
            } else {
                MessageBox.Show("Nombre invalido. El apodo no puede contener numeros \n o quedar vacio.");
            }
            
        }

        private bool esValido() {

            string nombre = txtNombre.Text;

            if (nombre.Contains('0') || nombre.Contains('1') || nombre.Contains('2') || nombre.Contains('3') || nombre.Contains('4') ) {
                return false;

            } else if (nombre.Contains('5') || nombre.Contains('6') || nombre.Contains('7') || nombre.Contains('8') ) {
                return false;

            } else if (nombre.Contains('9') || nombre == string.Empty) {
                return false;
            }

            return true;
        }
    }
}
