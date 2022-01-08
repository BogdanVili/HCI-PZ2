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
        private ObservableCollection<DistributedEnergyResource> ders;

        public ObservableCollection<DistributedEnergyResource> DERs
        {
            get { return ders; }
            set
            {
                ders = value;
                OnPropertyChanged("DERs");
            }
        }

        public MyICommand AddDER { get; set; }
        public MyICommand DeleteDER { get; set; }

        public MyICommand FilterDER { get; set; }

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

        public NetworkEntitiesViewModel()
        {
            DERs = StaticData.DERs;

            AddDER = new MyICommand(OnAddDER, OnUndoAddDER);

            TypeOfDERs = new ObservableCollection<TypeOfEnergyResource>();
            TypeOfDERs.Add(new TypeOfEnergyResource("Solar Panel", "C:\\Users\\bokic\\Desktop\\HCI PZ2\\NetworkService\\NetworkService\\Images\\solarpanel.png"));
            TypeOfDERs.Add(new TypeOfEnergyResource("Wind Turbine", "C:\\Users\\bokic\\Desktop\\HCI PZ2\\NetworkService\\NetworkService\\Images\\windturbine.png"));

            DeleteDER = new MyICommand(OnDeleteDER, OnUndoDeleteDER);

            FilterCurrent.MoreOrLessFilter = true;

            FilterCurrent.TypeFilter = new TypeOfEnergyResource();

            FilterDER = new MyICommand(OnFilterDER);
        }


        public void OnAddDER()
        {   
            CurrentDER.Validate();
            if (CurrentDER.IsValid)
            {
                DERs.Add(new DistributedEnergyResource(Int32.Parse(CurrentDER.Id), CurrentDER.Name, CurrentDER.TypeOfDER));
                addedDERs.Add(Int32.Parse(CurrentDER.Id));
            }
        }

        List<int> addedDERs = new List<int>();

        public void OnUndoAddDER()
        {
            if (addedDERs.Count > 0)
            {
                DistributedEnergyResource der = DERs.FirstOrDefault(d => d.Id == addedDERs.Last());
                DERs.Remove(der);
                addedDERs.Remove(addedDERs.Last());
            }
        }

        List<DistributedEnergyResource> DeletedDERs = new List<DistributedEnergyResource>();
        public void OnDeleteDER()
        {
            if(SelectedDER != null)
            {
                DeletedDERs.Add(SelectedDER);
                DERs.Remove(SelectedDER);
            }
        }

        public void OnUndoDeleteDER()
        {
            if(DeletedDERs.Count > 0)
            {
                DERs.Add(DeletedDERs.Last());
                DeletedDERs.Remove(DeletedDERs.Last());
            }
        }

        private Filter filterCurrent = new Filter();

        public Filter FilterCurrent
        {
            get { return filterCurrent; }
            set
            {
                filterCurrent = value;
                OnPropertyChanged("FilterCurrent");
            }
        }


        public void OnFilterDER()
        {
            if (string.IsNullOrWhiteSpace(FilterCurrent.TypeFilter.Name) && string.IsNullOrWhiteSpace(FilterCurrent.IdFilter))
            {
                return;
            }

            DERs = new ObservableCollection<DistributedEnergyResource>();
            foreach (DistributedEnergyResource item in StaticData.DERs)
            {
                if (!string.IsNullOrWhiteSpace(FilterCurrent.TypeFilter.Name) && string.IsNullOrWhiteSpace(FilterCurrent.IdFilter))
                {
                    if (FilterCurrent.TypeFilter.Name.Equals(item.TypeOfDER.Name))
                    {
                        DERs.Add(new DistributedEnergyResource(item.Id, item.Name, item.TypeOfDER));
                    }
                    continue;
                }

                if(string.IsNullOrWhiteSpace(FilterCurrent.TypeFilter.Name) && !string.IsNullOrWhiteSpace(FilterCurrent.IdFilter))
                {
                    if(FilterCurrent.MoreOrLessFilter && Int32.Parse(FilterCurrent.IdFilter) < item.Id)
                    {
                        DERs.Add(new DistributedEnergyResource(item.Id, item.Name, item.TypeOfDER));
                    }
                    else if(!FilterCurrent.MoreOrLessFilter && Int32.Parse(FilterCurrent.IdFilter) > item.Id)
                    {
                        DERs.Add(new DistributedEnergyResource(item.Id, item.Name, item.TypeOfDER));
                    }
                    continue;
                }

                if (FilterCurrent.TypeFilter.Name.Equals(item.TypeOfDER.Name))
                {
                    if (FilterCurrent.MoreOrLessFilter && Int32.Parse(FilterCurrent.IdFilter) < item.Id)
                    {
                        DERs.Add(new DistributedEnergyResource(item.Id, item.Name, item.TypeOfDER));
                    }
                    else if (!FilterCurrent.MoreOrLessFilter && Int32.Parse(FilterCurrent.IdFilter) > item.Id)
                    {
                        DERs.Add(new DistributedEnergyResource(item.Id, item.Name, item.TypeOfDER));
                    }
                }
            }
        }
    }
}
