using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace Tetris
{
    public class Cuadro
    {

        public int columna { get; set; }
        public int fila { get; set; }
        public Rectangle figura { get; set; }

        public Cuadro()
        {
            figura = new Rectangle();
            figura.Fill = Brushes.White;
            figura.Margin = new Thickness(2);
            figura.Height = 30;
            figura.Width = 30;

            columna = 5;
            fila = 2;
        }

        public Cuadro(int fila, int columna, SolidColorBrush color)
        {
            figura = new Rectangle();
            figura.Fill = color;
            figura.Margin = new Thickness(2);
            figura.Height = 30;
            figura.Width = 30;

            this.columna = columna;
            this.fila = fila;
        }

        public void cambiarCordenadas(int fila, int columna) {

            this.fila = fila;
            this.columna = columna;
        }



    }
}
