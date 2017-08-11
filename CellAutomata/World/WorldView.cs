using MyLib.CellAutomata.World.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/23 9:25:48
*
*/
namespace MyLib.CellAutomata.World
{
    /// <summary>
    /// 世界类的视图,无法修改
    /// </summary>
    public class WorldView:IWorldView
    {
        WorldMap m_world;
        public event EventHandler Update;
        internal WorldView(WorldMap world)
        {
            m_world = world;
        }

        internal void Notify()
        {
            Update?.Invoke(this, new EventArgs());
        }

        public SpaceView this[int x,int y]
        {
            get
            {
                return this[new RoomPoint(x, y)];
            }
        }

        public SpaceView this[RoomPoint loc]
        {
            get
            {
                return new SpaceView(m_world.Room[loc]);
            }
        }

        public int Dimension
        {
            get
            {
                return m_world.Room.Dimension;
            }
        }

        public MyMath.Vector Property
        {
            get
            {
                return m_world.Room.Properties;
            }
        }

        /// <summary>
        /// 仅用于2D世界
        /// </summary>
        public int Width
        {
            get
            {
                if (Dimension != 2) return 0;
                return (int)m_world.Room.Properties[0];
            }
        }

        /// <summary>
        /// 仅用于2D世界
        /// </summary>
        public int Height
        {
            get
            {
                if (Dimension != 2) return 0;
                return (int)m_world.Room.Properties[1];
            }
        }

        public RoomPoint CorePoint
        {
            get
            {
                RoomPoint point = m_world.Room.CreateRoomPoint();
                for (int i = 0; i < point.Coefficients.Dimension; ++i)
                    point.Coefficients[i] = (m_world.Room.Properties[i]+1)/2;
                return point;
            }
        }

        public bool IsStart
        {
            get
            {
                return m_world.IsStart;
            }
        }
    }
}
