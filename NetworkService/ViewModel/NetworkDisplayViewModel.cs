using NetworkService.Model;
using NetworkService.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetworkService.ViewModel
{
    public class NetworkDisplayViewModel : BindableBase
    {
        private ObservableCollection<DistributedEnergyResource> ders = new ObservableCollection<DistributedEnergyResource>();

        public ObservableCollection<DistributedEnergyResource> DERs
        {
            get { return ders; }
            set
            {
                ders = value;
                OnPropertyChanged("DERs");
            }
        }
        public MyICommand<object> GetCanvas { get; set; }
        public MyICommand<object> GetListView { get; set; }
        public MyICommand<object> GetBorder { get; set; }
        public MyICommand<DragEventArgs> GetDragEventArgs { get; set; }
        public MyICommand ListView_SelectionChanged { get; set; }
        public MyICommand ListView_MouseLeftButtonUp { get; set; }
        public MyICommand DragOver { get; set; }
        public MyICommand Drop { get; set; }

        public NetworkDisplayViewModel()
        {
            foreach(DistributedEnergyResource item in StaticData.DERs)
            {
                DERs.Add(new DistributedEnergyResource(item));
            }

            CanvasElements = new List<CanvasElements>();

            //loggedDatas.CollectionChanged += ChangeBorder;

            GetCanvas = new MyICommand<object>(OnGetCanvas);
            GetListView = new MyICommand<object>(OnGetListView);
            GetBorder = new MyICommand<object>(OnGetBorder);
            GetDragEventArgs = new MyICommand<DragEventArgs>(OnGetDragEventArgs);
            ListView_SelectionChanged = new MyICommand(OnListView_SelectionChanged);
            ListView_MouseLeftButtonUp = new MyICommand(OnListView_MouseLeftButtonUp);
            DragOver = new MyICommand(OnDragOver);
            Drop = new MyICommand(OnDrop);

            for(int i=0; i<16; i++)
            {
                CanvasNames.Add("Canvas" + i.ToString());
            }
        }

        private Canvas canvas;

        public Canvas Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }


        private void OnGetCanvas(object parameter)
        {
            Canvas = (Canvas)parameter;
        }

        private ListView listView;

        public ListView ListView
        {
            get { return listView; }
            set { listView = value; }
        }

        private void OnGetListView(object parameter)
        {
            ListView = (ListView)parameter;
        }

        private Border border;

        public Border Border
        {
            get { return border; }
            set { border = value; }
        }

        private void OnGetBorder(object parameter)
        {
            Border = (Border)parameter;
        }


        private DragEventArgs dragEventArgs;

        public DragEventArgs DragEventArgs
        {
            get { return dragEventArgs; }
            set { dragEventArgs = value; }
        }

        private void OnGetDragEventArgs(DragEventArgs e)
        {
            DragEventArgs = e;
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

        private bool dragging = false;

        //List
        private void OnListView_SelectionChanged()
        {
            if (!dragging)
            {
                dragging = true;
                DragDrop.DoDragDrop(ListView, SelectedDER, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        //List
        private void OnListView_MouseLeftButtonUp()
        {
            ListView.SelectedItem = null;
            dragging = false;
        }

        //Canvas
        private void OnDragOver()
        {
            if (Canvas.Resources["taken"] != null)
            {
                DragEventArgs.Effects = DragDropEffects.None;
            }
            else
            {
                DragEventArgs.Effects = DragDropEffects.Move;
            }
            DragEventArgs.Handled = true;
        }

        //Canvas
        private void OnDrop()
        {
            if (SelectedDER.TypeOfDER.Name != "")
            {
                if (Canvas.Resources["taken"] == null)
                {
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(SelectedDER.TypeOfDER.ImageSource);
                    logo.EndInit();
                    Canvas.Background = new ImageBrush(logo);
                    ((TextBlock)Canvas.Children[0]).Text = SelectedDER.Id.ToString() + " : " + SelectedDER.Name;
                    Canvas.Resources.Add("taken", true);
                    Canvas.Resources.Add("DER", SelectedDER);
                    if(SelectedDER.ValueMeasure > 5)
                    {
                        Border.BorderBrush = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        Border.BorderBrush = new SolidColorBrush(Colors.Green);
                    }
                    CanvasElements.Add(new CanvasElements(Canvas.Name, Border.Name, new DistributedEnergyResource(SelectedDER)));
                    DERs.Remove(SelectedDER);
                }
                ListView.SelectedItem = null;
                dragging = false;
            }
            DragEventArgs.Handled = true;
        }

        public List<CanvasElements> CanvasElements { get; set; }

        ObservableCollection<LoggedData> loggedDatas = StaticData.loggedDatas;



        private List<string> canvasNames = new List<string>();

        public List<string> CanvasNames
        {
            get { return canvasNames; }
            set { canvasNames = value; }
        }

        public Dictionary<string, SolidColorBrush> BrushesDictionary = new Dictionary<string, SolidColorBrush>();

        private void ChangeBorder(object sender, NotifyCollectionChangedEventArgs args)
        {
            foreach (LoggedData log in StaticData.loggedDatas)
            {
                if (CanvasElements.Find(c => c.DER.Id == log.Id) != null)
                {
                    CanvasElements ce = CanvasElements.Find(c => c.DER.Id == log.Id);
                    ce.DER.ValueMeasure = log.ValueMeasure;

                    if (ce.DER.ValueMeasure > 5)
                    {
                        BrushesDictionary[ce.GetBrushName()] = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        BrushesDictionary[ce.GetBrushName()] = new SolidColorBrush(Colors.Green);
                    }
                }
            }
        }

        private SolidColorBrush brush0;

        public SolidColorBrush Brush0
        {
            get { return brush0; }
            set
            {
                brush0 = value;
                OnPropertyChanged("Brush0");
            }
        }

    }

}
