using MyLib.CellAutomata.World.Life;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/19 16:48:15
*
*/
namespace MyLib.CellAutomata.World.Rule
{
    public class LiveRule : Rule
    {
        Func<UnitCollection, RuleArgs, int> m_rule;
        public LiveRule(Func<UnitCollection, RuleArgs, int> rule)
        {
            m_rule = rule;
        }
        public override int Execute(UnitCollection unit, RuleArgs args)
        {
            return m_rule(unit, args);
        }
    }
}
