using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FFXIV.WT.Model
{
    public class Duty
    {
        public Guid Key { get; set; }
        public string DutyName { get; set; }
        public int Category { get; set; }
    }
}
