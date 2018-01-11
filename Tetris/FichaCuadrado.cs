using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris {

    public class FichaCuadrado : FichaBase, Movimiento{



        public FichaCuadrado(Grid tablero, int[,] conteo): base(tablero, conteo) {

            SolidColorBrush color = elegirColor();

            activos.Add(new Cuadro(0, 5, color));
            activos.Add(new Cuadro(0, 6, color));
            activos.Add(new Cuadro(1, 5, color));
            activos.Add(new Cuadro(1, 6, color));

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

          

        }

        public void girar() {

            
        }

        public void limpiarConteo() {

        }

        public void guardarConteo() {

        }
    }
}
