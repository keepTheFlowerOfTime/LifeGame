using MyLib.CellAutomata.World.Rule;
using MyLib.CellAutomata.World.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/19 8:26:06
*
*/
namespace MyLib.CellAutomata.World
{
    public class WorldEditor
    {
        const int DEFAULT_WIDTH = 24;
        const int DEFAULT_HEIGHT = 24;

        Room_2D m_rootRoom;
        RuleCollection m_liveRules;
        RuleCollection m_deathRules;
        RuleCollection m_rebornRules;

        public WorldEditor()
        {
            m_rootRoom = new Room_2D(DEFAULT_WIDTH, DEFAULT_HEIGHT);
            m_liveRules = new RuleCollection();
            m_deathRules = new RuleCollection();
            m_rebornRules = new RuleCollection();
        }

        public void Resize(int width,int height)
        {
            m_rootRoom = new Room_2D(width, height);
        }

        public void Reset()
        {
            m_rootRoom = new Room_2D(m_rootRoom.Width, m_rootRoom.Height);
        }

        public WorldMap CreatWorld()
        {
            return new WorldMap(m_rootRoom, m_deathRules, m_rebornRules, m_liveRules);
        }

        #region 属性
        public RuleCollection LiveRules
        {
            get
            {
                return m_liveRules;
            }
        }

        public RuleCollection DeathRules
        {
            get
            {
                return m_deathRules;
            }
        }

        public RuleCollection RebornRules
        {
            get
            {
                return m_rebornRules;
            }
        }

        #endregion 
    }
}
