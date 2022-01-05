using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class NetworkEntitiesViewModel : BindableBase
    {
        public ObservableCollection<DistributedEnergyResource> DERs { get; set; }
        public MyICommand AddDER { get; set; }

        private DistributedEnergyResource currentDER = new DistributedEnergyResource();

        public ObservableCollection<TypeOfEnergyResource> TypeOfDERs { get; set; } 

        public DistributedEnergyResource CurrentDER
        {
            get { return currentDER; }
            set 
            {
                currentDER = value;
                OnPropertyChanged("CurrentDistributedEnergyResource");
            }
        }

        public NetworkEntitiesViewModel()
        {
            DERs = new ObservableCollection<DistributedEnergyResource>();

            AddDER = new MyICommand(OnAddDER);

            TypeOfDERs = new ObservableCollection<TypeOfEnergyResource>();
            TypeOfDERs.Add(new TypeOfEnergyResource("Solar Panel", "C:\\Users\\bokic\\Desktop\\HCI PZ2\\NetworkService\\NetworkService\\Images\\solarpanel.png"));
            TypeOfDERs.Add(new TypeOfEnergyResource("Wind Turbine", "C:\\Users\\bokic\\Desktop\\HCI PZ2\\NetworkService\\NetworkService\\Images\\windturbine.png"));
        }

        public void OnAddDER()
        {
            CurrentDER.Validate();
            if(CurrentDER.IsValid)
            {
                DERs.Add(new DistributedEnergyResource(CurrentDER.Id, CurrentDER.Name, CurrentDER.TypeOfDER));
            }
        }



    }
}
