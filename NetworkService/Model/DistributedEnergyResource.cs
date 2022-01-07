﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    [Serializable]
    public class DistributedEnergyResource : BindableBase
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
        

        private TypeOfEnergyResource typeOfDER;

        public TypeOfEnergyResource TypeOfDER
        {
            get { return typeOfDER; }
            set 
            { 
                if(typeOfDER != value)
                {
                    typeOfDER = value;
                    OnPropertyChanged("TypeOfDER");
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

        public DistributedEnergyResource()
        {
            typeOfDER = new TypeOfEnergyResource();
        }

        public DistributedEnergyResource(int id, string name, TypeOfEnergyResource typeOfDER)
        {
            this.id = id;
            this.name = name;
            this.typeOfDER = typeOfDER;
        }


    }
}
