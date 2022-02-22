using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace garden
{
    public class SnapShot
    {
        public string FileName { get; set; }
        public DateTime TimeValue { get; set; }

        // name is an optional parameter (this means it can be used only in C# 4)
        public SnapShot(string name, DateTime timee)
        {
            this.FileName = name;
            this.TimeValue = timee;
        }

        // whatever
    }
}
