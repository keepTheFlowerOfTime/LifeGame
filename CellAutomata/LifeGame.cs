using MyLib.CellAutomata.World;
using MyLib.CellAutomata.World.Life;
using MyLib.CellAutomata.World.Room;
using MyLib.CellAutomata.World.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/19 8:33:27
*
*/
namespace MyLib.CellAutomata
{
    public class LifeGame:IWorldView
    {
        public const int STATUS_DEATH_TRUE = 1;
        public const int STATUS_DEATH_FALSE = 0;
        public const int STATUS_REBORN_TRUE = 1;
        public const int STATUS_REBORN_FALSE = 0;
        public const int STATUS_LIVE_TRUE = 1;
        public const int STATUS_LIVE_FALSE = 0;
        World.WorldMap m_world;
        IEnumerable<KeyValuePair<Unit, RoomPoint>> m_lifeSeed;
        WorldView m_worldView;
        public LifeGame()
        {
            InitWorld();
            
        }

        public void Reset()
        {
            m_world.Reset();
        }

        public void Start()
        {
            m_world.RootLife = LifeSeed;
            m_world.Start();
        }

        public void Start(int startDelay,int interval)
        {
            m_world.RootLife = LifeSeed;
            m_world.Start(new WorldMap.DefaultEvolveControler(World, startDelay, interval));
        }

        public void End()
        {
            m_world.Stop();
        }

        private void InitWorld()
        {
            WorldEditor editor = new WorldEditor();
            //设置生命游戏 死亡规则
            editor.DeathRules.Add(new DeathRule((unit, args) =>
            {
                var spaces= args.NearSpaces;
                int nearLivesCount = 0;
                foreach (var space in spaces)
                    if (space.HasLife) ++nearLivesCount;
                if (nearLivesCount >= 4||nearLivesCount<=1) return STATUS_DEATH_TRUE;
                else return STATUS_DEATH_FALSE;
            }));

            //设置生命游戏 复活规则
            editor.RebornRules.Add(new RebornRule((unit, args) =>
            {
                var spaces = args.NearSpaces;
                int nearLivesCount = 0;
                foreach (var space in spaces)
                    if (space.HasLife) ++nearLivesCount;
                if (nearLivesCount == 3) return STATUS_REBORN_TRUE;
                else return STATUS_REBORN_FALSE;
            }));


            m_world = editor.CreatWorld();
            InitWorldSetting();
        }

        private void InitWorldSetting()
        {
            //当死亡规则触发时，所有该区域的生物死亡
            World.Death += (location, e) =>
              {
                  e.Location.KillAll();
              };

            //当重生规则触发时，区域内的生命诞生
            World.Reborn += (location, e) =>
              {
                  e.Location.AddLife(new Unit());
              };
        }

        private World.WorldMap World
        {
            get
            {
                return m_world;
            }
        }

        public WorldView WorldView
        {
            get
            {
                if (m_worldView == null) m_worldView = World.CreateView();
                return m_worldView;
            }
        }

        public IEnumerable<KeyValuePair<Unit, RoomPoint>> LifeSeed
        {
            get
            {
                return m_lifeSeed;
            }

            set
            {
                m_lifeSeed = value;
            }
        }

        public int Width
        {
            get
            {
                return ((IWorldView)m_worldView).Width;
            }
        }

        public int Height
        {
            get
            {
                return ((IWorldView)m_worldView).Height;
            }
        }

        public bool IsStart
        {
            get
            {
                return ((IWorldView)m_worldView).IsStart;
            }
        }

        public SpaceView this[RoomPoint loc]
        {
            get
            {
                return ((IWorldView)m_worldView)[loc];
            }
        }

        public SpaceView this[int x, int y]
        {
            get
            {
                return ((IWorldView)m_worldView)[x, y];
            }
        }
    }
}
