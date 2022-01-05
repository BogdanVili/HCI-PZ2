using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class TypeOfEnergyResource
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string imageSource;

        public string ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }
    }
}
