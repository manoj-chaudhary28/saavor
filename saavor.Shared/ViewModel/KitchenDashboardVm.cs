using saavor.Shared.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace saavor.Shared.ViewModel
{
    public class KitchenDashboardVm
    {
        public KitchenCountVm Count { get; set; }
        public List<KitchenDashboardKitchenVm> Kitchen { get; set; }
    }


}
