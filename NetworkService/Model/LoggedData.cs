using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class LoggedData : BindableBase
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        private double valueMeasure;

        public double ValueMeasure
        {
            get { return valueMeasure; }
            set { valueMeasure = value; }
        }

        public LoggedData(int id, DateTime time, double valueMeasure)
        {
            this.id = id;
            this.time = time;
            this.valueMeasure = valueMeasure;
        }
    }
}
