using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class DistributedEnergyResourceCurrent : ValidationBase
    {
        private string id;

        public string Id
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


        private TypeOfEnergyResource typeOfDER;

        public TypeOfEnergyResource TypeOfDER
        {
            get { return typeOfDER; }
            set
            {
                if (typeOfDER != value)
                {
                    typeOfDER = value;
                    OnPropertyChanged("TypeOfDER");
                }
            }
        }

        public DistributedEnergyResourceCurrent()
        {
            typeOfDER = new TypeOfEnergyResource();
        }

        protected override void ValidateSelf()
        {
            int temp;
            if (!Int32.TryParse(id, out temp))
            {
                ValidationErrors["Id"] = "Id must be an integer.";
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                ValidationErrors["Id"] = "Id is required.";
            }

            if (StaticData.DERs.FirstOrDefault(d => d.Id == Int32.Parse(Id)) != null)
            {
                ValidationErrors["Id"] = "Id already exists.";
            }

                if (string.IsNullOrWhiteSpace(name))
            {
                ValidationErrors["Name"] = "Name is required.";
            }

            if (string.IsNullOrWhiteSpace(typeOfDER.Name))
            {
                ValidationErrors["TypeOfDER"] = "Type is required.";
            }


        }
    }
}
