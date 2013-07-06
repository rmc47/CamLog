using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public interface IContest
    {
        int Mutlipliers { get; }
        int QsoPoints { get; }
        int TotalScore { get; }
        bool IsMult(Contact c);
    }
}
