using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NetworkService.ViewModel
{
    public class NetworkDisplayViewModel : BindableBase
    {
        public MyICommand<object> GetSender { get; set; }
        public MyICommand<MouseButtonEventArgs> GetMouseButtonEventArgs { get; set; }
        public MyICommand<DragEventArgs> GetDragEventArgs { get; set; }

        public MyICommand ClickCommand { get; set; }
        public NetworkDisplayViewModel()
        {
            GetSender = new MyICommand<object>(OnGetSender);
            GetMouseButtonEventArgs = new MyICommand<MouseButtonEventArgs>(OnGetMouseButtonEventArgs);
            GetDragEventArgs = new MyICommand<DragEventArgs>(OnGetDragEventArgs);
            ClickCommand = new MyICommand(OnClickCommand);
        }

        private object parameter;

        public object Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }


        private void OnGetSender(object parameter)
        {
            Parameter = parameter;
        }

        private MouseButtonEventArgs mouseButtonEventArgs;

        public MouseButtonEventArgs MouseButtonEventArgs
        {
            get { return mouseButtonEventArgs; }
            set { mouseButtonEventArgs = value; }
        }

        private void OnGetMouseButtonEventArgs(MouseButtonEventArgs e)
        {
            MouseButtonEventArgs = e;
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

        private void OnClickCommand()
        {
            int i = 0;
        }

    }

}
