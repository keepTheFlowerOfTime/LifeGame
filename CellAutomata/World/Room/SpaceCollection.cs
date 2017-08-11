using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/8/11 10:52:41
*
*/
namespace MyLib.CellAutomata.World.Room
{
    public class SpaceCollection:IEnumerable<Space>
    {
        Array m_spaces;
        int m_dimension;
        /// <summary>
        /// 设定Space各个维度的数值，有多少个数值，便代表多少维度
        /// </summary>
        /// <param name="args"></param>
        public SpaceCollection(params int[] args)
        {
            m_dimension = args.Length;
            m_spaces = Array.CreateInstance(typeof(Space),args);
        }


        internal Space this[params int[] loc]
        {
            get
            {
                int[] args = new int[m_dimension];
                for (int i = 0; i < m_dimension; ++i) args[i] = loc[i];
                return (Space)m_spaces.GetValue(args);
            }
            set
            {
                int[] args = new int[m_dimension];
                for (int i = 0; i < m_dimension; ++i) args[i] = loc[i];
                m_spaces.SetValue(value,args);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_spaces.GetEnumerator();
        }

        public int GetLength(int dimension)
        {
            if (dimension >= Rank) return 0;
            return m_spaces.GetLength(dimension);
        }

        IEnumerator<Space> IEnumerable<Space>.GetEnumerator()
        {
            return m_spaces.Cast<Space>().GetEnumerator();
        }

        public int Rank
        {
            get
            {
                return m_spaces.Rank;
            }
        }
    }
}
