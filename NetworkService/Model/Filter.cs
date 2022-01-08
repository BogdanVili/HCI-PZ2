using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Filter : BindableBase
    {
        private TypeOfEnergyResource typeFilter;

        public TypeOfEnergyResource TypeFilter
        {
            get { return typeFilter; }
            set
            {
                typeFilter = value;
                OnPropertyChanged("TypeFilter");
            }
        }

        private bool moreOrLessFilter;

        public bool MoreOrLessFilter
        {
            get { return moreOrLessFilter; }
            set
            {
                moreOrLessFilter = value;
                OnPropertyChanged("MoreOrLessFilter");
            }
        }

        private string idFilter;

        public string IdFilter
        {
            get { return idFilter; }
            set
            {
                idFilter = value;
                OnPropertyChanged("IdFilter");
            }
        }

        public Filter()
        {
            TypeFilter = new TypeOfEnergyResource();
        }
    }
}
