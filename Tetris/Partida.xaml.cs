using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tetris
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Partida : Window
    {

        private List<FichaBase> registroFichas = new List<FichaBase>();
        public int[,] conteo = new int[20, 10];
        public FichaBase esteMero;

        //En desuso
        int anterior = 7, este;
        //

        int nuevo, siguiente, turno;
        int milseg = 0, seg = 0, min = 0, hrs = 0, contador = 0,  limite = 1000;
        bool termino = false, seMovio = false;
        int puntaje = 0, dificultad = 0;
        DispatcherTimer temporarizador = new DispatcherTimer();

        int tonteria = 0;

        public Partida()
        {
            InitializeComponent();
            
            //Reiniciar  tablero a 0
            for (int i = 0; i < 20; i ++) {

                for (int j = 0; j < 10; j++) {

                    conteo[i, j] = 0;
                }
            }
        }

        #region "Metodos auxiliares"

        private void verificarLineas() {

            int lineas = 0;

            //Verificar si una fila entera esta ocupada
            for (int i = 0; i < 20; i++) {

                bool lleno = true;
                for (int j = 0; j < 10; j++) {

                    if (conteo[i, j] == 0) {
                        lleno = false;
                    }
                }

                //Si esta llena una fila
                if (lleno) {
                    
                    //Buscar los cuadritos que se encuentran en esas filas
                    // y pintarlos de gris 'No sirve de nada'
                    foreach (FichaBase fichita in registroFichas) {

                        foreach (Cuadro cuadradito in fichita.activos) {

                            if (cuadradito.fila == i) {

                                cuadradito.figura.Fill = Brushes.Gray;

                            }
                        }
                    }

                    //Buscar los cuadritos que se encuentran en esas filas.
                    
                    for (int k = 0; k < registroFichas.Count; k++) {

                        for (int l = 0; l < registroFichas[k].activos.Count; l++) {

                            if (registroFichas[k].activos[l].fila == i) {

                                //1.- Eliminar la posicion del cuadrito en el array 'conteo'.
                                //2.- Eliminar el cuadrito del grid.
                                //3.- Eliminar el cuadrito de la Ficha que lo contiene.

                                conteo[registroFichas[k].activos[l].fila, registroFichas[k].activos[l].columna] = 0;
                                tablero.Children.Remove(registroFichas[k].activos[l].figura);
                                registroFichas[k].activos.Remove(registroFichas[k].activos[l]);
                                l--;
                            }
                        }

                        //Si al hacer las operaciones anteriores la Ficha ya no posee cuadritos.
                        //Eliminar la ficha de 'registroFichas.'
                        if (registroFichas[k].activos.Count == 0) {
                            registroFichas.Remove(registroFichas[k]);
                            k--;
                        }
                    }

                    //Mover todas las fichas que estan por encima una fila hacia abajo
                    foreach (FichaBase fichita in registroFichas) {

                        fichita.ModificarConteo(true);

                        foreach (Cuadro cuadrito in fichita.activos) {

                            if (cuadrito.fila < i) {
                                Grid.SetRow(cuadrito.figura, ++cuadrito.fila);
                            }
                        }
                        fichita.ModificarConteo(false);
                    }

                    lineas++;
                   
                }

            }

            //Verificar filas consecutivas para conteo.
            if (lineas == 1) {
                lblPuntaje.Content = (puntaje += 100);

            }  else if (lineas == 2) {
                lblPuntaje.Content = (puntaje += 300);

            } else if (lineas == 3) {
                lblPuntaje.Content = (puntaje += 500);

            } else if (lineas == 4) {
                lblPuntaje.Content = (puntaje += 800);

            } else if (lineas == 5) {
                lblPuntaje.Content = (puntaje += 900);

            } else if (lineas == 6) {
                lblPuntaje.Content = (puntaje += 1100);

            } else if (lineas == 7) {
                lblPuntaje.Content = (puntaje += 1300);

            } else if (lineas == 8) {
                lblPuntaje.Content = (puntaje += 1600);

            }

            cambiaDificultad();
        }

        //En desuso
        private void crearFicha() {

            do {
                Random aleatorio = new Random();
                este = aleatorio.Next(0, 6);

            } while (anterior == este);

            if (este == 0) {

                esteMero = new FichaCuadrado(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;

            } else if (este == 1) {

                esteMero = new FichaLinea(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;

            } else if (este == 2) {

                esteMero = new FichaLetraL(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;

            } else if (este == 3) {

                esteMero = new FichaTanque(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;
            } else if (este == 4) {

                esteMero = new FichaSerpiente(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;

            } else {

                esteMero = new FichaSerpienteInvertida(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;
            }

            if (!esteMero.sePuede) {

                terminarPartida();
            }
        }
        //

        private void cambiaDificultad() {

            if (dificultad == 0 && puntaje >= 1000) {
                dificultad++;
                limite -= 100;

            } else if (dificultad == 1 && puntaje >= 2000) {
                dificultad++;
                limite -= 100;

            } else if (dificultad == 2 && puntaje >= 3000) {
                dificultad++;
                limite -= 100;

            } else if (dificultad == 3 && puntaje >= 5000) {
                dificultad++;
                limite -= 200;

            } else if (dificultad == 4 && puntaje >= 7000) {
                dificultad++;
                limite -= 100;

            } else if (dificultad == 5 && puntaje >= 9000) {
                dificultad++;
                limite -= 100;

            } else if (dificultad == 6 && puntaje >= 10000) {
                dificultad++;
                limite -= 100;

            } else if (dificultad == 7 && puntaje >= 12000) {
                dificultad++;
                limite -= 100;

            } else if (dificultad == 8 && puntaje >= 15000) {
                dificultad++;
                limite -= 100;
            }
        }

        private void terminarPartida() {

            termino = true;

            if (tonteria == 0) {

                Guardar ventana = new Guardar();
                ventana.Owner = this;

                ventana.hrs = this.hrs;
                ventana.mins = this.min;
                ventana.segs = this.seg;
                ventana.puntos = this.puntaje;
                ventana.ponerDatos();
                temporarizador.Stop();
                this.Hide();
                ventana.Show();

                tonteria++;
            }
        }
        
        private void iniciarFichas() {

            Random randy = new Random(Guid.NewGuid().GetHashCode());
            int eleccion = randy.Next(0,6);
            int eleccionS = 0;
            do {
                eleccionS = randy.Next(0, 6);
            } while (eleccion == eleccionS);

            //Elegir primera figura
            if (eleccion == 0) {

                esteMero = new FichaCuadrado(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;

            } else if (eleccion == 1) {

                esteMero = new FichaLinea(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;

            } else if (eleccion == 2) {

                esteMero = new FichaLetraL(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;

            } else if (eleccion == 3) {

                esteMero = new FichaTanque(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;
            } else if (eleccion == 4) {

                esteMero = new FichaSerpiente(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;

            } else {

                esteMero = new FichaSerpienteInvertida(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                anterior = este;
            }

            //Dibujar Siguiente figura.
            dibujarSiguienteFigura(eleccionS);

            siguiente = eleccionS;

        }

        private void crearFicha2() {

            //Elegir cual sera la figura del siguiente turno.
            //Verificar que no sea la misma del turno anterior.
            do {
                Random aleatorio = new Random();
                nuevo = aleatorio.Next(0, 6);

            } while (nuevo == siguiente);
            turno = 0;

            //Dibujar la siguiente figura.
            if (siguiente == 0) {

                esteMero = new FichaCuadrado(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                

            } else if (siguiente == 1) {

                esteMero = new FichaLinea(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                

            } else if (siguiente == 2) {

                esteMero = new FichaLetraL(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                

            } else if (siguiente == 3) {

                esteMero = new FichaTanque(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                

            } else if (siguiente == 4) {

                esteMero = new FichaSerpiente(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                

            } else {

                esteMero = new FichaSerpienteInvertida(tablero, conteo);

                foreach (Cuadro ficha in esteMero.activos) {
                    conteo[ficha.fila, ficha.columna] = 1;
                }

                registroFichas.Add(esteMero);
                
            }

            //Dibujar figura del siguiente turno
            dibujarSiguienteFigura(nuevo);

            siguiente = nuevo;

            //Si no se puede dibujar la figura
            //Termina la partida
            if (!esteMero.sePuede) {

                terminarPartida();
            }
        }

        private void dibujarSiguienteFigura(int eleccionS) {

            //Dibujar la figura que correspondera a la que sigue.
            if (eleccionS == 0) {

                ImageSource i;
                i = new BitmapImage(new Uri(@"cuadro.png", UriKind.Relative));
                Isiguiente.Source = i;

            } else if (eleccionS == 1) {

                ImageSource i;
                i = new BitmapImage(new Uri(@"larga.png", UriKind.Relative));
                Isiguiente.Source = i;

            } else if (eleccionS == 2) {

                ImageSource i;
                i = new BitmapImage(new Uri(@"ele.png", UriKind.Relative));
                Isiguiente.Source = i;

            } else if (eleccionS == 3) {

                ImageSource i;
                i = new BitmapImage(new Uri(@"tanque.png", UriKind.Relative));
                Isiguiente.Source = i;

            } else if (eleccionS == 4) {

                ImageSource i;
                i = new BitmapImage(new Uri(@"sInvertida.png", UriKind.Relative));
                Isiguiente.Source = i;

            } else {

                ImageSource i;
                i = new BitmapImage(new Uri(@"ser.png", UriKind.Relative));
                Isiguiente.Source = i;
            }
        }
        
        #endregion

        #region "Eventos"

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                temporarizador.Interval = new TimeSpan(0, 0, 0, 0, 100);

                temporarizador.Tick += (s, a) => {

                    if (!termino) {

                        //Cronometro.
                        milseg+=100;
                        contador += 100;
                        lblmiliSegundos.Content = milseg;
                        
                        if (milseg == 1000) {

                            lblmiliSegundos.Content = (milseg = 0);
                            lblSegundos.Content = ++seg;

                        }

                        if (seg == 60) {

                            lblSegundos.Content = (seg = 0);
                            lblMinutos.Content = ++min;
                        }

                        if (min == 60) {

                            lblMinutos.Content = (min = 0);
                            lblHoras.Content = ++hrs;
                        }

                        //En caso de que la ficha no se movio.
                        if (!seMovio && contador >= limite) {
                            
                            // Hacer un casting a la figura para ejecutar su metodo 
                            // moverAbajo();
                            if (esteMero.GetType() == typeof(FichaLinea)) {

                                FichaLinea casting = (FichaLinea)esteMero;
                                casting.moverAbajo();

                            } else if (esteMero.GetType() == typeof(FichaTanque)) {

                                FichaTanque casting = (FichaTanque)esteMero;
                                casting.moverAbajo();

                            } else if (esteMero.GetType() == typeof(FichaLetraL)) {

                                FichaLetraL casting = (FichaLetraL)esteMero;
                                casting.moverAbajo();
                                
                            } else if (esteMero.GetType() == typeof(FichaSerpiente)) {

                                FichaSerpiente casting = (FichaSerpiente)esteMero;
                                casting.moverAbajo();

                            } else if (esteMero.GetType() == typeof(FichaSerpienteInvertida)) {

                                FichaSerpienteInvertida casting = (FichaSerpienteInvertida)esteMero;
                                casting.moverAbajo();
                                
                            } else if (esteMero.GetType() == typeof(FichaCuadrado)) {

                                FichaCuadrado casting = (FichaCuadrado)esteMero;
                                casting.moverAbajo();
                                
                            }

                            
                            //En caso de que el movimientoAbajo anterior no fuera posible.
                            if (!esteMero.moverse) {
                                verificarLineas();
                                bool finish = false;
                                
                                //Verificar si se estanco en la fila 0.
                                for (int j = 0; j < 10; j++) {
                                    if (conteo[0, j] == 1 && turno != 0) {
                                        finish = true;
                                    }
                                }

                                //Si es asi terminar la partida.
                                if (finish) {
                                    terminarPartida();
                                
                                //De lo contrario crear una nueva figura.
                                } else {
                                    crearFicha2();
                                }

                            }

                            turno++;
                            contador = 0;

                        } else {
                            seMovio = false;
                            
                        }
                    }

                };

                temporarizador.Start();
                //crearFicha();
                iniciarFichas();
            }
            catch (Exception ex)
            {
                string ilk = ex.Message;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e){

            if (!termino) {

                //Identificar que boton se dio click para decidir que hacer
                //Hacer un movimiento hacia abajo.
                if (e.Key == Key.Down) {

                    turno++;

                    if (esteMero != null) {

                        //Hacer un casting a la pieza origen para hacer el
                        //correcto metodo moverAbajo()
                        if (esteMero.GetType() == typeof(FichaLinea)) {
                            FichaLinea casting = (FichaLinea)esteMero;
                            casting.moverAbajo();

                        } else if (esteMero.GetType() == typeof(FichaTanque)) {
                            FichaTanque casting = (FichaTanque)esteMero;
                            casting.moverAbajo();

                        } else if (esteMero.GetType() == typeof(FichaLetraL)) {
                            FichaLetraL casting = (FichaLetraL)esteMero;
                            casting.moverAbajo();

                        } else if (esteMero.GetType() == typeof(FichaSerpiente)) {
                            FichaSerpiente casting = (FichaSerpiente)esteMero;
                            casting.moverAbajo();

                        } else if (esteMero.GetType() == typeof(FichaSerpienteInvertida)) {
                            FichaSerpienteInvertida casting = (FichaSerpienteInvertida)esteMero;
                            casting.moverAbajo();

                        } else if (esteMero.GetType() == typeof(FichaCuadrado)) {
                            FichaCuadrado casting = (FichaCuadrado)esteMero;
                            casting.moverAbajo();
                        }

                        
                        if (!esteMero.moverse) {
                            verificarLineas();


                            ///////

                            bool finish = false;

                            for (int j = 0; j < 10; j++) {

                                if (conteo[0, j] == 1) {
                                    finish = true;
                                }
                            }

                            if (finish) {
                                terminarPartida();

                            } else {
                               
                                crearFicha2();
                            }

                           

                            
                        }

                        contador = 0;
                        seMovio = true;
                    }


                } else if (e.Key == Key.Right) {

                    if (esteMero != null) {

                        if (esteMero.GetType() == typeof(FichaLinea)) {
                            FichaLinea casting = (FichaLinea)esteMero;
                            casting.moverDerecha();

                        } else if (esteMero.GetType() == typeof(FichaTanque)) {
                            FichaTanque casting = (FichaTanque)esteMero;
                            casting.moverDerecha();

                        } else if (esteMero.GetType() == typeof(FichaLetraL)) {
                            FichaLetraL casting = (FichaLetraL)esteMero;
                            casting.moverDerecha();

                        } else if (esteMero.GetType() == typeof(FichaSerpiente)) {
                            FichaSerpiente casting = (FichaSerpiente)esteMero;
                            casting.moverDerecha();

                        } else if (esteMero.GetType() == typeof(FichaSerpienteInvertida)) {
                            FichaSerpienteInvertida casting = (FichaSerpienteInvertida)esteMero;
                            casting.moverDerecha();

                        } else if (esteMero.GetType() == typeof(FichaCuadrado)) {
                            FichaCuadrado casting = (FichaCuadrado)esteMero;
                            casting.moverDerecha();
                        }


                    }


                } else if (e.Key == Key.Left) {

                    if (esteMero != null) {
                        //esteMero.moverIzquierda();

                        if (esteMero.GetType() == typeof(FichaLinea)) {
                            FichaLinea casting = (FichaLinea)esteMero;
                            casting.moverIzquierda();

                        } else if (esteMero.GetType() == typeof(FichaTanque)) {
                            FichaTanque casting = (FichaTanque)esteMero;
                            casting.moverIzquierda();

                        } else if (esteMero.GetType() == typeof(FichaLetraL)) {
                            FichaLetraL casting = (FichaLetraL)esteMero;
                            casting.moverIzquierda();

                        } else if (esteMero.GetType() == typeof(FichaSerpiente)) {
                            FichaSerpiente casting = (FichaSerpiente)esteMero;
                            casting.moverIzquierda();

                        } else if (esteMero.GetType() == typeof(FichaSerpienteInvertida)) {
                            FichaSerpienteInvertida casting = (FichaSerpienteInvertida)esteMero;
                            casting.moverIzquierda();

                        } else if (esteMero.GetType() == typeof(FichaCuadrado)) {
                            FichaCuadrado casting = (FichaCuadrado)esteMero;
                            casting.moverIzquierda();
                        }


                    }

                } else if (e.Key == Key.Up) {

                    if (esteMero != null) {

                        if (esteMero.GetType() == typeof(FichaLinea)) {
                            FichaLinea casting = (FichaLinea)esteMero;
                            casting.girar();

                        } else if (esteMero.GetType() == typeof(FichaTanque)) {
                            FichaTanque casting = (FichaTanque)esteMero;
                            casting.girar();

                        } else if (esteMero.GetType() == typeof(FichaLetraL)) {
                            FichaLetraL casting = (FichaLetraL)esteMero;
                            casting.girar();

                        } else if (esteMero.GetType() == typeof(FichaSerpiente)) {
                            FichaSerpiente casting = (FichaSerpiente)esteMero;
                            casting.girar();

                        } else if (esteMero.GetType() == typeof(FichaSerpienteInvertida)) {
                            FichaSerpienteInvertida casting = (FichaSerpienteInvertida)esteMero;
                            casting.girar();
                        }
                    }

                } else if (e.Key == Key.Space) {

                    if (esteMero != null) {

                        while (esteMero.moverse) {

                            if (esteMero.GetType() == typeof(FichaLinea)) {
                                FichaLinea casting = (FichaLinea)esteMero;
                                casting.moverAbajo();

                            } else if (esteMero.GetType() == typeof(FichaTanque)) {
                                FichaTanque casting = (FichaTanque)esteMero;
                                casting.moverAbajo();

                            } else if (esteMero.GetType() == typeof(FichaLetraL)) {
                                FichaLetraL casting = (FichaLetraL)esteMero;
                                casting.moverAbajo();

                            } else if (esteMero.GetType() == typeof(FichaSerpiente)) {
                                FichaSerpiente casting = (FichaSerpiente)esteMero;
                                casting.moverAbajo();

                            } else if (esteMero.GetType() == typeof(FichaSerpienteInvertida)) {
                                FichaSerpienteInvertida casting = (FichaSerpienteInvertida)esteMero;
                                casting.moverAbajo();

                            } else if (esteMero.GetType() == typeof(FichaCuadrado)) {
                                FichaCuadrado casting = (FichaCuadrado)esteMero;
                                casting.moverAbajo();
                            }
                        }

                        

                        verificarLineas();
                        /////

                        //crearFicha();
                        bool finish = false;

                        for (int j = 0; j < 10; j++) {

                            if (conteo[0, j] == 1) {
                                finish = true;
                            }
                        }

                        if (finish) {
                            terminarPartida();

                        } else {
                            //crearFicha();
                            crearFicha2();

                        }
                        
                    }
                } else if (e.Key == Key.C) {


                    //Verificar cuales casillas estan reconocidas como ocupadas
                    // Y cuales no -- "Testeto"
                    string array = "";
                    for (int i = 0; i < 20; i++) {
                        for (int j = 0; j < 10; j++) {

                            array += conteo[i, j] + " ";
                        }
                        array += "\n";
                    }
                    MessageBox.Show(array);

                    ////
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

            Owner.Show();
        }
    }


    #endregion


}
