using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class MeasurementGraphViewModel : BindableBase
    {
        private ObservableCollection<LoggedData> loggedDatas;

        public ObservableCollection<LoggedData> LoggedDatas
        {
            get { return loggedDatas; }
            set 
            { 
                loggedDatas = value;
                OnPropertyChanged("LoggedDatas");
                OnIdSelectionChanged();
            }
        }

        public MeasurementGraphViewModel()
        {
            loggedDatas = StaticData.loggedDatas;
            uniqueIds = new List<int>();
            SelectUniqueIds();
            IdSelectionChanged = new MyICommand(OnIdSelectionChanged);
        }

        private List<int> uniqueIds;

        public List<int> UniqueIds
        {
            get { return uniqueIds; }
            set 
            { 
                uniqueIds = value;
                OnPropertyChanged("UniqueIds");
            }
        }

        private void SelectUniqueIds()
        {
            foreach(LoggedData l in LoggedDatas)
            {
                if(!UniqueIds.Contains(l.Id))
                {
                    UniqueIds.Add(l.Id);
                }
            }
        }

        public MyICommand IdSelectionChanged { get; set; }

        private ObservableCollection<LoggedData> selectedLogs;

        public ObservableCollection<LoggedData> SelectedLogs
        {
            get { return selectedLogs; }
            set 
            { 
                selectedLogs = value;
                OnPropertyChanged("SelectedLogs");
            }
        }

        private int selectedId;

        public int SelectedId
        {
            get { return selectedId; }
            set { selectedId = value; }
        }


        void OnIdSelectionChanged()
        {
            if (!(SelectedId < 1))
            {
                SelectedLogs = new ObservableCollection<LoggedData>();

                foreach (LoggedData l in LoggedDatas.Reverse())
                {
                    if(selectedLogs.Count == 5)
                    {
                        break;
                    }
                    if(l.Id == SelectedId)
                    {
                        SelectedLogs.Add(l);
                    }
                }

                SelectedLogs = (ObservableCollection<LoggedData>)SelectedLogs.Reverse();
            }
        }
    }
}
