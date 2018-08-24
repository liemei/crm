using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnuxq.crm.ViewModel
{
    [Serializable]
    public class Data
    {
        public string type { get; set; }
        public string name { get; set; }
    }
    [Serializable]
    public class Mark
    {
        public List<Data> data { get; set; }
    }
    [Serializable]
    public class Serie
    {
        public string name { get; set; }

        public string type { get; set; }

        public string[] data { get; set; }

        public Mark markPoint { get; set; }

        public Mark markLine { get; set; }
    }
}
