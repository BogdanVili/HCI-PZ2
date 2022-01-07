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
        ObservableCollection<DistributedEnergyResource> DERs = DistributedEnergyResources.DERs;

        public MyICommand AddDER { get; set; }
        public MyICommand DeleteDER { get; set; }


        private DistributedEnergyResourceCurrent currentDER = new DistributedEnergyResourceCurrent();

        public DistributedEnergyResourceCurrent CurrentDER
        {
            get { return currentDER; }
            set
            {
                currentDER = value;
                OnPropertyChanged("CurrentDER");
            }
        }

        private DistributedEnergyResource selectedDER;

        public DistributedEnergyResource SelectedDER
        {
            get { return selectedDER; }
            set
            {
                selectedDER = value;
                OnPropertyChanged("SelectedDER");
            }
        }

        public ObservableCollection<TypeOfEnergyResource> TypeOfDERs { get; set; }

        private string currentDERid;

        public string CurrentDERid
        {
            get { return currentDERid;  }
            set
            {
                currentDERid = value;
                OnPropertyChanged("CurrentDERid");
            }
        }

        private string validationErrorId;

        public string ValidationErrorId
        {
            get { return validationErrorId; }
            set
            {
                validationErrorId = value;
                OnPropertyChanged("ValidationErrorId");
            }
        }

        private Data serializer = new Data();

        public NetworkEntitiesViewModel()
        {
            AddDER = new MyICommand(OnAddDER);

            TypeOfDERs = new ObservableCollection<TypeOfEnergyResource>();
            TypeOfDERs.Add(new TypeOfEnergyResource("Solar Panel", "C:\\Users\\bokic\\Desktop\\HCI PZ2\\NetworkService\\NetworkService\\Images\\solarpanel.png"));
            TypeOfDERs.Add(new TypeOfEnergyResource("Wind Turbine", "C:\\Users\\bokic\\Desktop\\HCI PZ2\\NetworkService\\NetworkService\\Images\\windturbine.png"));

            DeleteDER = new MyICommand(OnDeleteDER);
        }


        public void OnAddDER()
        {
            ValidationErrorId = "";
            
            CurrentDER.Validate();
            if (CurrentDER.IsValid)
            {
                DERs.Add(new DistributedEnergyResource(Int32.Parse(CurrentDER.Id), CurrentDER.Name, CurrentDER.TypeOfDER));
            }
        }

        public void OnDeleteDER()
        {
            if(SelectedDER != null)
            {
                DERs.Remove(SelectedDER);
            }
        }


    }
}
