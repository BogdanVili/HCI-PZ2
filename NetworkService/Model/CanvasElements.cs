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
        private string canvas;

        public string Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        private string border;

        public string Border
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

        public CanvasElements(string canvas, string border, DistributedEnergyResource der)
        {
            this.canvas = canvas;
            this.border = border;
            this.der = der;
        }

        public string GetBrushName()
        {
            return "Brush" + Canvas.Replace("Canvas", "");
        }
    }
}
