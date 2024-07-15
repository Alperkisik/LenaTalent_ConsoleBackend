using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LenaTalent_ConsoleBackend.Dtos
{
    public sealed class EventCombination
    {
        public EventCombination()
        {
            EventIdList = new List<int>();
        }

        public int TotalPoint { get; set; }

        public List<int> EventIdList;
    }
}
