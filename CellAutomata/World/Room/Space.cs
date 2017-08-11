using MyLib.CellAutomata.World.Life;
using MyLib.CellAutomata.World.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/20 14:54:58
*
*/
namespace MyLib.CellAutomata.World.Room
{
    /// <summary>
    /// Room由一块块的Space组成,目前认为Room等同于一个Space的二维数组
    /// </summary>
    public class Space
    {
        UnitCollection m_units;
        internal Space(RoomPoint loc)
        {
            m_units = new UnitCollection();
            Location = loc;
        }

        public void KillAll()
        {
            m_units.Clear();
        }

        public void AddLife(Unit unit)
        {
            m_units.Add(unit);
        }

        /// <summary>
        /// 返回该空间包含的生命数
        /// </summary>
        public int Count
        {
            get
            {
                return m_units.Count;
            }
        }

        public RoomPoint Location
        {
            get;
            private set;
        }

        /// <summary>
        /// 返回该空间中是否包含生命
        /// </summary>
        public bool HasLife
        {
            get
            {
                return Count != 0;
            }
        }

        public UnitCollection Lives
        {
            get
            {
                return m_units;
            }
        }
    }
}
