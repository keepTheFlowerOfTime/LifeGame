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
* Time: 2017/6/20 14:45:42
*
*/
namespace MyLib.CellAutomata.World
{
    public class WorldMap
    {
        public class DefaultEvolveControler : BaseEvolveControler
        {
            public const int DEFAULT_START_DELAY = 500;
            public const int DEFAULT_INTERVAL = 500;

            WorldMap m_world;
            public DefaultEvolveControler(WorldMap world):base(DEFAULT_START_DELAY,DEFAULT_INTERVAL)
            {
                m_world = world;
            }

            public DefaultEvolveControler(WorldMap world,int startDelay,int interval):base(startDelay,interval)
            {
                m_world = world;
            }

            protected override void DoWork()
            {
                m_world.Evolve();
            }
        }
        RuleCollection m_deathRules;
        RuleCollection m_liveRules;
        RuleCollection m_rebornRules;
        IRoom m_room;
        BaseEvolveControler m_controler;
        ICollection<WorldView> m_worldViews;
        IEnumerable<KeyValuePair<Unit, RoomPoint>> m_rootLife;

        public event EventHandler<SpaceEvolveEventArgs> Death;
        public event EventHandler<SpaceEvolveEventArgs> Reborn;
        public event EventHandler<SpaceEvolveEventArgs> Live;

        


        internal WorldMap(Room_2D room, RuleCollection deathRules, RuleCollection rebornRules, RuleCollection liveRules)
        {
            m_room = room;
            m_deathRules = deathRules;
            m_rebornRules = rebornRules;
            m_liveRules = liveRules;
            m_worldViews = new LinkedList<WorldView>();
        }

        /// <summary>
        /// 将Root生命加入世界，并开始整个世界的演变
        /// </summary>
        public void Start(BaseEvolveControler controler)
        {
            if (IsStart) return;
            IsStart = true;
            foreach (var pair in RootLife)
                m_room[pair.Value].AddLife(pair.Key);
            NotifyChanged();
            m_controler = controler;
            m_controler.Start();
        }
        
        /// <summary>
        /// 使用默认的控制器开启世界的演变
        /// </summary>
        public void Start()
        {
            Start(new DefaultEvolveControler(this));
        }

        /// <summary>
        /// 中止演变进程,清除世界中所有的生物
        /// </summary>
        public void Reset()
        {
            if (!IsStart) return;     
            Stop();     
            foreach (var space in Room.Spaces)
                space.KillAll();
            NotifyChanged();
            IsStart = false;
        }

        /// <summary>
        /// 演变指定的次数
        /// </summary>
        /// <param name="count"></param>
        public void Evolve(int count=1)
        {
            while (count-- > 0)
                EvolveProcess();
            NotifyChanged();
        }

        /// <summary>
        /// 中止世界的演变
        /// </summary>
        public void Stop()
        {
            if (!IsStart) return;
            m_controler?.Close();
        }

        /// <summary>
        /// 当调用Stop函数后，可以使用该函数重启世界的演变进程
        /// </summary>
        public void ReStart()
        {
            m_controler.Start();
        }

        public WorldView CreateView()
        {
            var view = new WorldView(this);
            m_worldViews.Add(view);
            return view;
        }

        private void EvolveProcess()
        {
            var evolveResult = ComputeEvolveResult();
            EvolveByResult(evolveResult);
        }

        private EvolveResult ComputeEvolveResult()
        {
            Dictionary<Space, SpaceEvolveResult> result = new Dictionary<Space, SpaceEvolveResult>();
            foreach(var space in Room.Spaces)
            {
                var nearSpaces = Room.GetNeighbors(space.Location);
                var args = new RuleArgs(nearSpaces);
                result[space] = new SpaceEvolveResult();
                result[space].NearSpaces = nearSpaces;
                if (space.HasLife)
                {
                    result[space].LiveRuleFitResult = m_liveRules.Execute(space.Lives, args);
                    result[space].DeathRuleFitResult = m_deathRules.Execute(space.Lives, args);
                }
                else result[space].RebornRuleFitResult = m_rebornRules.Execute(space.Lives, args);
            }
            return new EvolveResult(result);
        }

        protected void NotifyChanged()
        {
            foreach (var view in Views)
                view.Notify();
        }

        private void EvolveByResult(EvolveResult args)
        {
            foreach (var pair in args.Result)
            {
                bool deathEventIsHappended = pair.Value.DeathRuleFitResult.Contains(LifeGame.STATUS_DEATH_TRUE);
                bool rebornEventIsHappened = pair.Value.RebornRuleFitResult.Contains(LifeGame.STATUS_REBORN_TRUE);
                bool liveEventIsHappened = pair.Value.LiveRuleFitResult.Contains(LifeGame.STATUS_LIVE_TRUE);
                var eventArgs = new SpaceEvolveEventArgs()
                {
                    Location = pair.Key,
                    NearSpaces = pair.Value.NearSpaces
                };
                if (liveEventIsHappened) LiveEventHappened(this, eventArgs);
                if (deathEventIsHappended) DeathEventHappened(this,eventArgs);
                if (rebornEventIsHappened) RebornEventHappened(this, eventArgs);
            }
        }

        private void DeathEventHappened(object world, SpaceEvolveEventArgs args)
        {
            Death?.Invoke(world, args);
        }

        private void LiveEventHappened(object world, SpaceEvolveEventArgs args)
        {
            Live?.Invoke(world, args);
        }

        private void RebornEventHappened(object world, SpaceEvolveEventArgs args)
        {
            Reborn?.Invoke(world, args);
        }

        public IRoom Room
        {
            get
            {
                return m_room;
            }
        }

        public IEnumerable<WorldView> Views
        {
            get
            {
                return m_worldViews;
            }
        }

        public IEnumerable<KeyValuePair<Unit,RoomPoint>> RootLife
        {
            get
            {
                return m_rootLife;
            }

            set
            {
                m_rootLife = value;
            }
        }

        public bool IsStart
        {
            get;
            set;
        }
    }

    class SpaceEvolveResult
    {
        public IEnumerable<Space> NearSpaces
        {
            get; set;
        }

        public int[] DeathRuleFitResult
        {
            get;
            set;
        } = new int[0];

        public int[] RebornRuleFitResult
        {
            get;
            set;
        } = new int[0];

        public int[] LiveRuleFitResult
        {
            get;
            set;
        } = new int[0];
    }

    class EvolveResult
    {
        public EvolveResult(Dictionary<Space, SpaceEvolveResult> result)
        {
            Result = result;
        }
        public Dictionary<Space, SpaceEvolveResult> Result
        {
            get;
            private set;
        }
    }

    public class SpaceEvolveEventArgs : EventArgs
    {
        public Space Location
        {
            get;
            set;
        }

        public IEnumerable<Space> NearSpaces
        {
            get;
            set;
        }
    }
}
