using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class DistributedEnergyResource : ValidationBase
    {
        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        

        private TypeOfEnergyResource type;

        public TypeOfEnergyResource Type
        {
            get { return type; }
            set 
            { 
                if(type != value)
                { 
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }


        private double value;

        public double Value
        {
            get { return value; }
            set 
            { 
                if(this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                ValidationErrors["Id"] = "Id is required.";
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                ValidationErrors["Name"] = "Name is required.";
            }

            if (string.IsNullOrWhiteSpace(type.Name))
            {
                ValidationErrors["Type"] = "Type is required.";
            }
        }
    }
}
