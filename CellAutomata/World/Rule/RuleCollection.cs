using MyLib.CellAutomata.World.Life;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/19 8:32:36
*
*/
namespace MyLib.CellAutomata.World.Rule
{
    public class RuleCollection:ICollection<Rule>
    {
        ICollection<Rule> m_rules;
        public RuleCollection()
        {
            m_rules = new List<Rule>();
        }

        public int[] Execute(UnitCollection units,RuleArgs args)
        {
            int[] result = new int[m_rules.Count];
            int pos = 0;
            foreach (Rule rule in m_rules)
                result[pos++] = rule.Execute(units, args);
            return result;
        }

        public int Count
        {
            get
            {
                return m_rules.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return m_rules.IsReadOnly;
            }
        }

        public void Add(Rule item)
        {
            m_rules.Add(item);
        }

        public void Clear()
        {
            m_rules.Clear();
        }

        public bool Contains(Rule item)
        {
            return m_rules.Contains(item);
        }

        public void CopyTo(Rule[] array, int arrayIndex)
        {
            m_rules.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Rule> GetEnumerator()
        {
            return m_rules.GetEnumerator();
        }

        public bool Remove(Rule item)
        {
            return m_rules.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_rules.GetEnumerator();
        }
    }
}
