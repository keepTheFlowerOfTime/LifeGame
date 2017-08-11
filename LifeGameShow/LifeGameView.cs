using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib.CellAutomata;
using MyLib.CellAutomata.World.Room;
using MyLib.CellAutomata.World.Life;
using MyLib.CellAutomata.World;

/*
* Author: gxw
* Time: 2017/6/23 10:45:06
*
*/
namespace LifeGameShow
{
    public class LifeGameView
    {
        static class Caches
        {
            public static readonly Color LIVES_COLOR = Color.Green;
            public static readonly Color EMPTY_COLOR = Color.White;
            public static readonly Color STRING_COLOR = Color.Black;
            public static readonly Color BOUNDRY_COLOR = Color.Black;
            public static readonly SolidBrush LIVES_BRUSH;
            public static readonly SolidBrush EMPTY_BRUSH;
            public static readonly SolidBrush STRING_BRUSH;
            public static readonly SolidBrush BOUNDRY_BRUSH;
            static Caches()
            {
                LIVES_BRUSH = new SolidBrush(LIVES_COLOR);
                EMPTY_BRUSH = new SolidBrush(EMPTY_COLOR);
                STRING_BRUSH = new SolidBrush(STRING_COLOR);
                BOUNDRY_BRUSH = new SolidBrush(BOUNDRY_COLOR);
            }
        }

        public const float DEVIATION = 3;
        public const int DEFAULT_PICTURE_WIDTH = 600;
        public const int DEFAULT_PICTURE_HEIGHT = 600;

        LifeGame m_lifeGameCore;
        Dictionary<RoomPoint, ICollection<Unit>> m_rootLives;
        Size m_pictureSize;
        public event EventHandler Update;

        public LifeGameView()
        {
            m_lifeGameCore = new LifeGame();
            m_rootLives = new Dictionary<RoomPoint, ICollection<Unit>>();
            Size = new Size(DEFAULT_PICTURE_WIDTH, DEFAULT_PICTURE_HEIGHT);
            m_lifeGameCore.WorldView.Update += (sender, e) => ShowNowLifeGameStatus();
        }

        private IEnumerable<KeyValuePair<Unit, RoomPoint>> ToLifeSeed()
        {
            LinkedList<KeyValuePair<Unit, RoomPoint>> lifeSeed = new LinkedList<KeyValuePair<Unit, RoomPoint>>();
            foreach (var pair in m_rootLives)
            {
                ICollection<Unit> spaceRootUnits = pair.Value;
                foreach (var unit in spaceRootUnits)
                    lifeSeed.AddLast(new KeyValuePair<Unit, RoomPoint>(unit, pair.Key));
            }
            return lifeSeed;
        }

        public void Start()
        {
            m_lifeGameCore.LifeSeed = ToLifeSeed();
            m_lifeGameCore.Start();
        }

        public void Start(int startDelay, int interval)
        {
            m_lifeGameCore.LifeSeed = ToLifeSeed();
            m_lifeGameCore.Start(startDelay, interval);
        }

        public void Close()
        {
            m_lifeGameCore.End();
        }

        public void AddRootLife(Unit unit, RoomPoint loc)
        {
            bool isExist = m_rootLives.ContainsKey(loc);
            if (!isExist)
                m_rootLives[loc] = new LinkedList<Unit>();
            m_rootLives[loc].Add(unit);
            ShowBaseLifeGameStatus();
        }

        public void AddRootLife(RoomPoint loc)
        {
            AddRootLife(new Unit(), loc);
        }

        public void AddRootLife(Point loc)
        {
            AddRootLife(new Unit(), new RoomPoint(loc.X, loc.Y));
        }

        public void RandomAddRootLife(int lives)
        {
            Random randomGenerator = new Random();
            
        }

        public void RemoveLife(RoomPoint loc)
        {
            m_rootLives.Remove(loc);
            ShowBaseLifeGameStatus();
        }

        public void RemoveLife(Point loc)
        {
            RemoveLife(new RoomPoint(loc.X, loc.Y));
        }

        private void ShowLifeGameStatus()
        {
            if (m_lifeGameCore.IsStart)
                ShowNowLifeGameStatus();
            else
                ShowBaseLifeGameStatus();
        }

        private void ShowNowLifeGameStatus()
        {
            Image image = new Bitmap(Image);
            Graphics graphics = Graphics.FromImage(image);
            SolidBrush livesBrush = new SolidBrush(Caches.LIVES_COLOR);
            SolidBrush emptyBrush = new SolidBrush(Caches.EMPTY_COLOR);
            for (int i = 0; i < m_lifeGameCore.WorldView.Width; ++i)
                for (int j = 0; j < m_lifeGameCore.WorldView.Height; ++j)
                    DrawSpace(graphics, m_lifeGameCore.WorldView[i, j], i, j);
            graphics.Dispose();
            Image = image;
            NotifyControl();
        }

        private void ShowBaseLifeGameStatus()
        {
            float widthUnit = WidthUnit;
            float heightUnit = HeightUnit;

            Image image = new Bitmap(Size.Width, Size.Height);
            Graphics graphics = Graphics.FromImage(image);

            foreach (var pair in m_rootLives)
                DrawSpace(graphics, pair.Value.Count != 0, pair.Key.X, pair.Key.Y);

            for (int i = 0; i <= m_lifeGameCore.WorldView.Width; ++i)
            {
                float widthPos = i * widthUnit;
                graphics.DrawLine(Pens.Black, new PointF(widthPos, 0), new PointF(widthPos, Size.Height - DEVIATION));
            }

            for (int i = 0; i <= m_lifeGameCore.WorldView.Height; ++i)
            {
                float heightPos = i * heightUnit;
                graphics.DrawLine(Pens.Black, new PointF(0, heightPos), new PointF(Size.Width - DEVIATION, heightPos));
            }
            graphics.Dispose();
            Image = image;
            NotifyControl();
        }

        private void NotifyControl()
        {
            Update?.Invoke(Image, new EventArgs());
        }

        private void DrawSpace(Graphics graphics, SpaceView spaceView, int x, int y)
        {
            DrawSpace(graphics, spaceView.HasLife, x, y);
            PointF corePoint = new PointF(WidthUnit * (x + 0.5f), HeightUnit * (y + 0.5f));

            if (spaceView.HasLife)
                graphics.DrawString(spaceView.Count.ToString(), SystemFonts.DefaultFont, Caches.STRING_BRUSH, corePoint.X, corePoint.Y);
        }

        private void DrawSpace(Graphics graphics, bool hasLife, int x, int y)
        {
            var rect = new RectangleF(new PointF(WidthUnit * x, HeightUnit * y), new SizeF(WidthUnit, HeightUnit));
            if (hasLife)
                graphics.FillRectangle(Caches.LIVES_BRUSH, rect);
            else
                graphics.FillRectangle(Caches.EMPTY_BRUSH, rect);
            graphics.DrawRectangles(new Pen(Caches.BOUNDRY_BRUSH), new RectangleF[] { rect });
            PointF corePoint = new PointF(WidthUnit * (x + 0.5f), HeightUnit * (y + 0.5f));
        }

        public Size Size
        {
            get
            {
                return m_pictureSize;
            }
            set
            {
                m_pictureSize = value;
            }
        }

        public float HeightUnit
        {
            get
            {
                return (Size.Height - DEVIATION) / m_lifeGameCore.WorldView.Height;
            }
        }

        public float WidthUnit
        {
            get
            {
                return (Size.Width - DEVIATION) / m_lifeGameCore.WorldView.Width;
            }
        }

        public Image Image
        {
            get;
            private set;
        }

        public IWorldView WorldProperties
        {
            get
            {
                return m_lifeGameCore;
            }
        }

        public RoomPoint CorePoint
        {
            get
            {
                return m_lifeGameCore.WorldView.CorePoint;
            }
        }
    }
}
