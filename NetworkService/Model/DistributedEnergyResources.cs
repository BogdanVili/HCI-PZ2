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
        public static ObservableCollection<DistributedEnergyResource> DERs = new ObservableCollection<DistributedEnergyResource>();
    }
}
