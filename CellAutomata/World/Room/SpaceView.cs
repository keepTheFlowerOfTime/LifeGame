using MyLib.CellAutomata.World.Life;
using MyLib.CellAutomata.World.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/23 9:29:32
*
*/
namespace MyLib.CellAutomata.World.Room
{
    public class SpaceView
    {
        Space m_space;
        public SpaceView(Space space)
        {
            m_space = space;
        }

        public IEnumerable<Unit> Units
        {
            get
            {
                return m_space.Lives;
            }
        }

        public bool HasLife
        {
            get
            {
                return m_space.HasLife;
            }
        }

        public int Count
        {
            get
            {
                return m_space.Count;
            }
        }
    }
}
