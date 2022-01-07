using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkService.Model
{
    public class StaticData
    {
        public static Data DataIO = new Data();

        public static ObservableCollection<DistributedEnergyResource> DERs = DataIO.LoadData("ders.xml");

        public static Stack<ICommandUndo> myICommands = new Stack<ICommandUndo>();

        public static ObservableCollection<LoggedData> loggedDatas = DataIO.LoadLogs("Log.txt");
    }
}
