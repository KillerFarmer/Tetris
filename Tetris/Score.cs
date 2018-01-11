using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {

    public class Score {

        public string nombre { get; set; }
        public int horas { get; set; }
        public int minutos { get; set; }
        public int segundos { get; set; }
        public int puntos { get; set; }

        public Score() { }

        public Score(string nombre) {
            this.nombre = nombre;
        }

        public override string ToString() {
            string h, m, s;
            h = horas + string.Empty;
            m = minutos + string.Empty;
            s = segundos + string.Empty;

            if (h.Length == 1) {
                h = "0" + horas;
            } 

            if (m.Length == 1) {
                m = "0" + minutos;
            } 

            if (s.Length == 1) {
                s = "0" + segundos;
            } 

            return nombre + "    " + h + ":" + m + ":" + s + "     " + puntos;
        }
    }
}
