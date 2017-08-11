using MyMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

/*
* Author: gxw
* Time: 2017/6/19 8:27:39
*
*/
namespace MyLib.CellAutomata.World.Room
{
    public class Room_2D : IRoom
    {
        SpaceCollection m_spaces;
        public Room_2D(int width, int height)
        {
            m_spaces = new SpaceCollection(height, width);
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    m_spaces[i, j] = CreateSpace(new RoomPoint(i, j));
        }

        public Space this[int x, int y]
        {
            get
            {
                return this[new RoomPoint(x, y)];
            }
        }

        public Space this[RoomPoint point]
        {
            get
            {
                return m_spaces[point.Y, point.X];
            }
        }

        public IEnumerable<Space> GetNeighbors(int x, int y)
        {
            return GetNeighbors(new RoomPoint(x, y));
        }

        /// <summary>
        /// 获取与该点相邻的Space集合，包括斜线相邻
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public IEnumerable<Space> GetNeighbors(RoomPoint location)
        {
            return GetNeighborsProcess(location);
        }

        private IList<Space> GetNeighborsProcess(RoomPoint location)
        {
            IList<Space> spaces = new List<Space>(8);
            var points = CreatePossibleNeighborsPoints(location);
            foreach (var point in points)
                if (IsExist(point)) spaces.Add(this[point]);
            return spaces;
        }

        private RoomPoint[] CreatePossibleNeighborsPoints(RoomPoint point)
        {
            RoomPoint[] result = new RoomPoint[8];
            //index 0-2的point
            for (int i = -1; i <= 1; ++i)
                result[i + 1] = new RoomPoint(point.X + i, point.Y - 1);
            result[3] = new RoomPoint(point.X + 1, point.Y);
            //index 4-6
            for (int i = -1; i <= 1; ++i)
                result[5 + i] = new RoomPoint(point.X - i, point.Y + 1);
            result[7] = new RoomPoint(point.X - 1, point.Y);
            return result;
        }

        public bool IsExist(RoomPoint point)
        {
            if (point.X < 0 || point.X >= Width) return false;
            else if (point.Y < 0 || point.Y >= Height) return false;
            return true;
        }

        public Space CreateSpace(RoomPoint loc)
        {
            return new Space(loc);
        }

        public RoomPointF CreateRoomPointF()
        {
            return new RoomPointF(Dimension);
        }

        public RoomPoint CreateRoomPoint()
        {
            return new RoomPoint(Dimension);
        }

        public int Width
        {
            get
            {
                return m_spaces.GetLength(0);
            }
        }

        public int Height
        {
            get
            {
                return m_spaces.GetLength(1);
            }
        }

        public int Count
        {
            get
            {
                int result = 0;
                foreach (var space in m_spaces)
                    result += space.Count;
                return result;
            }
        }

        public int Dimension
        {
            get
            {
                return m_spaces.Rank;
            }
        }

        public Vector Properties
        {
            get
            {
                Vector result = new Vector(Dimension);
                for (int i = 0; i < result.Dimension; ++i)
                    result[i] = m_spaces.GetLength(i);
                return result;
            }
        }

        public SpaceCollection Spaces
        {
            get
            {
                return m_spaces;
            }
        }
    }

    public struct RoomPoint
    {
        Vector m_vector;

        public RoomPoint(int dimension)
        {
            m_vector = new Vector(dimension);
        }

        public RoomPoint(params int[] args)
        {
            m_vector = new Vector(args);
        }

        private int GetCoefficient(int index)
        {
            return (m_vector.Dimension >= (index + 1) ? (int)m_vector[index] : 0);
        }

        private void SetCoefficient(int index, int value)
        {
            if (m_vector.Dimension >= index) return;
            m_vector[index] = value;
        }

        public int X
        {
            get
            {
                return GetCoefficient(0);
            }

            set
            {
                SetCoefficient(0, value);
            }
        }

        public int Y
        {
            get
            {
                return GetCoefficient(1);
            }

            set
            {
                SetCoefficient(1, value);
            }
        }

        public int Z
        {
            get
            {
                return GetCoefficient(2);
            }
            set
            {
                SetCoefficient(2, value);
            }
        }

        public Vector Coefficients
        {
            get
            {
                return m_vector;
            }
        }
    }

    public struct RoomPointF
    {
        Vector m_vector;

        public RoomPointF(int dimension)
        {
            m_vector = new Vector(dimension);
        }

        public RoomPointF(params float[] args)
        {
            m_vector = new Vector(args);
        }

        public RoomPointF(RoomPoint point)
        {
            m_vector = point.Coefficients.Clone();
        }

        private float GetCoefficient(int index)
        {
            return (m_vector.Dimension >= (index + 1) ? (float)m_vector[index] : 0);
        }

        private void SetCoefficient(int index, float value)
        {
            if (m_vector.Dimension >= index) return;
            m_vector[index] = value;
        }

        public float X
        {
            get
            {
                return GetCoefficient(0);
            }

            set
            {
                SetCoefficient(0, value);
            }
        }

        public float Y
        {
            get
            {
                return GetCoefficient(1);
            }

            set
            {
                SetCoefficient(1, value);
            }
        }

        public float Z
        {
            get
            {
                return GetCoefficient(2);
            }
            set
            {
                SetCoefficient(2, value);
            }
        }
    }
}
