using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class DistributedEnergyResources
    {
        public static Data DataIO = new Data();

        public static ObservableCollection<DistributedEnergyResource> DERs = DataIO.LoadData("ders.xml");
    }
}
