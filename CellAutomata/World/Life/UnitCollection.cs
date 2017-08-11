using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/19 16:44:50
*
*/
namespace MyLib.CellAutomata.World.Life
{
    public class UnitCollection:ICollection<Unit>
    {
        ICollection<Unit> m_units;
        public UnitCollection()
        {
            m_units = new LinkedList<Unit>();
        }

        public int Count
        {
            get
            {
                return m_units.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return m_units.IsReadOnly;
            }
        }

        public void Add(Unit item)
        {
            m_units.Add(item);
        }

        public void Clear()
        {
            m_units.Clear();
        }

        public bool Contains(Unit item)
        {
            return m_units.Contains(item);
        }

        public void CopyTo(Unit[] array, int arrayIndex)
        {
            m_units.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Unit> GetEnumerator()
        {
            return m_units.GetEnumerator();
        }

        public bool Remove(Unit item)
        {
            return m_units.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_units.GetEnumerator();
        }
    }
}
