using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LenaTalent_ConsoleBackend.Dtos
{
    public sealed record Event(int Id, TimeSpan Start_Time, TimeSpan End_Time, string Location, int Priority);
}
