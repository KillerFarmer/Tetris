using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace Tetris {

    class FichaLetraL: FichaBase, Movimiento {

        public FichaLetraL(Grid tablero, int[,] conteo): base(tablero, conteo) {

            SolidColorBrush color = elegirColor();

            activos.Add(new Cuadro(0, 6, color));
            centro = new Cuadro(1, 6, color);
            activos.Add(centro);
            activos.Add(new Cuadro(2, 6, color));
            activos.Add(new Cuadro(2, 5, color));

            foreach (Cuadro esteMero in activos) {

                if (conteo[esteMero.fila, esteMero.columna] == 1) {
                    sePuede = false;
                }
            }

            foreach (Cuadro esteMero in activos) {

                Grid.SetColumn(esteMero.figura, esteMero.columna);
                Grid.SetRow(esteMero.figura, esteMero.fila);

                tablero.Children.Add(esteMero.figura);
            }

            inclinacion = 1;
        }

        public void girar() {

            int[,] coordenadas = new int[4, 2];

            int i = 0;
            foreach (Cuadro figurita in activos) {

                if (figurita != centro) {

                    coordenadas[i, 0] = figurita.fila;
                    coordenadas[i, 1] = figurita.columna;

                }
                i++;
            }


            i = 0;
            foreach (Cuadro figurita in activos) {

                if (figurita != centro) {

                    if (figurita.fila - centro.fila == 2 && figurita.columna - centro.columna == 0) {
                        coordenadas[i, 0] -= 2;
                        coordenadas[i, 1] -= 2;

                    } else if (figurita.fila - centro.fila == 1 && figurita.columna - centro.columna == 0) {
                        coordenadas[i, 0] -= 1;
                        coordenadas[i, 1] -= 1;

                    } else if (figurita.fila - centro.fila == -1 && figurita.columna - centro.columna == 0) {
                        coordenadas[i, 0] += 1;
                        coordenadas[i, 1] += 1;

                    } else if (figurita.fila - centro.fila == -2 && figurita.columna - centro.columna == 0) {
                        coordenadas[i, 0] += 2;
                        coordenadas[i, 1] += 2;

                    } else if (figurita.fila - centro.fila == 0 && figurita.columna - centro.columna == 2) {
                        coordenadas[i, 0] += 2;
                        coordenadas[i, 1] -= 2;

                    } else if (figurita.fila - centro.fila == 0 && figurita.columna - centro.columna == 1) {
                        coordenadas[i, 0] += 1;
                        coordenadas[i, 1] -= 1;

                    } else if (figurita.fila - centro.fila == 0 && figurita.columna - centro.columna == -1) {
                        coordenadas[i, 0] -= 1;
                        coordenadas[i, 1] += 1;

                    } else if (figurita.fila - centro.fila == 0 && figurita.columna - centro.columna == -2) {
                        coordenadas[i, 0] -= 2;
                        coordenadas[i, 1] += 2;

                    } else if (figurita.fila - centro.fila == 1 && figurita.columna - centro.columna == 1) {

                        coordenadas[i, 1] -= 2;

                    } else if (figurita.fila -centro.fila == 1 && figurita.columna - centro.columna == -1) {
                        coordenadas[i, 0] -= 2;
                        
                    } else if (figurita.fila - centro.fila == -1 && figurita.columna - centro.columna == -1) {
                        coordenadas[i, 1] += 2;

                    } else if (figurita.fila - centro.fila == -1 && figurita.columna - centro.columna == 1) {
                        coordenadas[i, 0] += 2;
                    }

                }
                i++;
            }

            bool esPosible = true;

            i = 0;
            foreach (Cuadro figurita in activos) {

                if (activos[i] != centro) {

                    if (coordenadas[i, 0] < 0 || coordenadas[i, 0] > 19 || coordenadas[i, 1] < 0 || coordenadas[i, 1] > 9 || conteo[coordenadas[i, 0], coordenadas[i, 1]] == 1) {

                        esPosible = false;
                    }


                }
                i++;

            }

            if (esPosible) {

                i = 0;

                //ModificarConteo(true);
                limpiarConteo();

                foreach (Cuadro figurita in activos) {

                    if (figurita != centro) {

                        figurita.fila = coordenadas[i, 0];
                        figurita.columna = coordenadas[i, 1];


                        Grid.SetRow(figurita.figura, figurita.fila);
                        Grid.SetColumn(figurita.figura, figurita.columna);

                    }
                    i++;

                }

                //ModificarConteo(false);
                guardarConteo();

                if (inclinacion == 1) {
                    inclinacion = 2;

                } else if (inclinacion == 2) {
                    inclinacion = 3;

                } else if (inclinacion == 3) {
                    inclinacion = 4;

                } else {
                    inclinacion = 1;
                }
                
            }

        }

        public override bool verificarAbajo() {

            if (inclinacion == 3) {

                if (conteo[centro.fila, centro.columna + 1] == 1 || conteo[centro.fila + 2, centro.columna] == 1) {
                    return false;

                }
            } else if (inclinacion == 4) {

                if (conteo[centro.fila  + 1, centro.columna - 1] == 1 || conteo[centro.fila + 1, centro.columna] == 1 || conteo[centro.fila + 2, centro.columna + 1] == 1) {
                    return false;
                }
            }
            return base.verificarAbajo();
        }

        public override bool verificarDerecha() {

            if (inclinacion == 2) {

                if (conteo[centro.fila -1, centro.columna] == 1 || conteo[centro.fila, centro.columna + 2] == 1) {
                    return false;
                }

            } else if (inclinacion == 3) {

                if (conteo[centro.fila + 1, centro.columna + 1] == 1 || conteo[centro.fila, centro.columna + 1] == 1 || conteo[centro.fila - 1, centro.columna + 2] == 1) {
                    return false;
                }
            }

            return base.verificarDerecha();
        }

        public override bool verificarIzquierda() {

            if (inclinacion == 1) {

                if (conteo[centro.fila + 1, centro.columna - 2] == 1 || conteo[centro.fila, centro.columna - 1] == 1 || conteo[centro.fila -1, centro.columna - 1] == 1 ) {
                    return false;
                }

            } else if(inclinacion == 4) {

                if (conteo[centro.fila, centro.columna - 2] == 1 || conteo[centro.fila + 1, centro.columna] == 1) {
                    return false;
                }
            }

            return base.verificarIzquierda();
        }

        public void limpiarConteo() {

            foreach (Cuadro figurita in activos) {

                conteo[figurita.fila, figurita.columna] = 0;

            }
        }

        public void guardarConteo() {

            foreach (Cuadro figurita in activos) {

                conteo[figurita.fila, figurita.columna] = 1;

            }
        }
    }
}
