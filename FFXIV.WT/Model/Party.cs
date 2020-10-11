using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FFXIV.WT.Model
{
    public class Party
    {
        public List<Guid> SelectedDuties { get; set; }
        public Guid RoomKey { get; set; }
    }
}
