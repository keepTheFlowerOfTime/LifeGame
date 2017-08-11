using MyLib.CellAutomata.World.Life;
using MyLib.CellAutomata.World.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/19 8:29:41
*
*/
namespace MyLib.CellAutomata.World.Rule
{
    public abstract class Rule
    {
        public abstract int Execute(UnitCollection units, RuleArgs args);
    }

    public class RuleArgs
    {
        IEnumerable<Space> m_nearSpaces;
        public RuleArgs(IEnumerable<Space> nearSpaces)
        {
            m_nearSpaces = nearSpaces;
        }

        public IEnumerable<Space> NearSpaces
        {
            get
            {
                return m_nearSpaces;
            }
        }
    }
}
