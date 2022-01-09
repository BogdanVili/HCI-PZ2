using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            }
        }

        public MeasurementGraphViewModel()
        {
            loggedDatas = StaticData.loggedDatas;
            uniqueIds = new List<int>();
            SelectUniqueIds();
            IdSelectionChanged = new MyICommand(OnIdSelectionChanged);
            loggedDatas.CollectionChanged += LoggedDatasChanged;
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
                ValuePosition = new ObservableCollection<int>();
                CirclePosition = new ObservableCollection<int>();
                CircleStrokeThickness = 3;

                foreach (LoggedData l in LoggedDatas.Reverse())
                {
                    if(selectedLogs.Count == 5)
                    {
                        break;
                    }
                    if(l.Id == SelectedId)
                    {
                        SelectedLogs.Add(l);
                        ValuePosition.Add(Int32.Parse(l.ValueMeasure.ToString()) * 25);
                        CirclePosition.Add(Int32.Parse(l.ValueMeasure.ToString()) * 25 - 10);
                    }
                }

                SelectedLogs = new ObservableCollection<LoggedData>(SelectedLogs.Reverse());
                ValuePosition = new ObservableCollection<int>(ValuePosition.Reverse());
                CirclePosition = new ObservableCollection<int>(CirclePosition.Reverse());
            }
        }

        void LoggedDatasChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            OnIdSelectionChanged();
        }

        private ObservableCollection<int> valuePosition;

        public ObservableCollection<int> ValuePosition
        {
            get { return valuePosition; }
            set 
            { 
                valuePosition = value;
                OnPropertyChanged("ValuePosition");
            }
        }

        private ObservableCollection<int> circlePosition;

        public ObservableCollection<int> CirclePosition
        {
            get { return circlePosition; }
            set
            {
                circlePosition = value;
                OnPropertyChanged("CirclePosition");
            }
        }

        private int circleStrokeThickness = 0;

        public int CircleStrokeThickness
        {
            get { return circleStrokeThickness; }
            set 
            { 
                circleStrokeThickness = value;
                OnPropertyChanged("CircleStrokeThickness");
            }
        }

    }
}
