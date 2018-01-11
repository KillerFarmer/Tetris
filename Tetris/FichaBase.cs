using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris {

    public abstract class FichaBase {

        public List<Cuadro> activos = new List<Cuadro>();
        public bool moverse { get; set; }
        public Grid tablero { get; set; }
        public int[,] conteo;
        public int inclinacion;
        public Cuadro centro;
        public bool sePuede = true;
        

        public FichaBase(Grid tablero, int[,] conteo) {
            this.tablero = tablero;
            this.conteo = conteo;
            moverse = true;
        }

        //public abstract void girar();

        public void moverAbajo() {

            if (!salirseTablero(1) && verificarAbajo()) {

                foreach (Cuadro esteMero in activos) {

                    ModificarConteo(true);
                    Grid.SetRow(esteMero.figura, ++esteMero.fila);
                    ModificarConteo(false);
                }

            } else {

                moverse = false;
            }
        }

        public void moverDerecha() {

            if (!salirseTablero(2) && verificarDerecha()) {

                foreach (Cuadro esteMero in activos) {

                    ModificarConteo(true);
                    Grid.SetColumn(esteMero.figura, ++esteMero.columna);
                    ModificarConteo(false);
                }
            }
        }

        public void moverIzquierda() {

            if (!salirseTablero(3) && verificarIzquierda()) {

                foreach (Cuadro esteMero in activos) {

                    ModificarConteo(true);
                    Grid.SetColumn(esteMero.figura, --esteMero.columna);
                    ModificarConteo(false);
                }
            }
        }

        public bool salirseTablero(int movimiento) {

            foreach (Cuadro esteMero in activos) {

                if (movimiento == 1 && esteMero.fila + 1 > 19) {
                    return true;
                } else if (movimiento == 2 && esteMero.columna + 1 > 9) {
                    return true;
                } else if (movimiento == 3 && esteMero.columna -1 < 0) {
                    return true;
                }
            }

            return false;

        }
        
        public virtual bool verificarAbajo() {

            int extremo = 0;

            foreach (Cuadro ficha in activos) {

                if (ficha.fila > extremo) {
                    extremo = ficha.fila;
                }

            }

            foreach (Cuadro ficha in activos) {

                if (ficha.fila  == extremo && conteo[extremo + 1, ficha.columna] == 1) {
                    return false;
                }
            }

            return true;
        }

        public virtual bool verificarDerecha() {

            int extremo = 0;

            foreach (Cuadro figurita in activos) {

                if (figurita.columna > extremo) {
                    extremo = figurita.columna;
                }
            }

            foreach (Cuadro figurita in activos) {

                if (figurita.columna == extremo && conteo[figurita.fila, extremo+1] == 1) {

                    return false;
                }
            }
            return true;
        }

        public virtual bool verificarIzquierda() {

            int extremo = 9;

            foreach (Cuadro figurita in activos) {

                if (figurita.columna < extremo) {

                    extremo = figurita.columna;
                }
            }

            foreach (Cuadro figurita in activos) {

                if (figurita.columna == extremo && conteo[figurita.fila, extremo-1] == 1) {

                    return false;
                }
            }

            return true;
        }

        public void ModificarConteo(bool reiniciar) {

            if (reiniciar) {

                foreach (Cuadro figurita in activos) {

                    conteo[figurita.fila, figurita.columna] = 0;

                }

            } else {

                foreach (Cuadro figurita in activos) {

                    conteo[figurita.fila, figurita.columna] = 1;

                }
            }
        }

        public SolidColorBrush elegirColor() {

            Random posAlAzar = new Random();
            int numero = posAlAzar.Next(0, 5);

            if (numero == 0) {
                return Brushes.White;

            } else if (numero == 1) {
                return Brushes.Orange;

            } else if (numero == 2) {
                return Brushes.Blue;

            } else if (numero == 3) {
                return Brushes.Green;

            } else {
                return Brushes.Red;
            }

        }
        
    }
}
