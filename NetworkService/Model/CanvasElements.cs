using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NetworkService.Model
{
    public class CanvasElements
    {
        private Canvas canvas;

        public Canvas Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        private Border border;

        public Border Border
        {
            get { return border; }
            set { border = value; }
        }

        private DistributedEnergyResource der;

        public DistributedEnergyResource DER
        {
            get { return der; }
            set { der = value; }
        }

        public CanvasElements(Canvas canvas, Border border, DistributedEnergyResource der)
        {
            this.canvas = canvas;
            this.border = border;
            this.der = der;
        }
    }
}
