using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/6/23 8:50:48
*
*/
namespace MyLib.CellAutomata.World
{
    public abstract class BaseEvolveControler
    {
        int m_startDelay;
        bool m_flag;
        Thread m_thread;
        public BaseEvolveControler(int startDelay,int interval)
        {
            m_startDelay = startDelay;
            Interval = interval;
        }

        public void Start()
        {
            if (m_flag) return;
            m_flag = true;
            Thread.Sleep(m_startDelay);        
            ControlProcess();
        }

        protected virtual void ControlProcess()
        {
            m_thread = new Thread(() =>
              {
                  while (m_flag)
                  {
                      DoWork();
                      DoInterval();
                  }
              });
            m_thread.Start();
        }

        protected abstract void DoWork();

        protected virtual void DoInterval()
        {
            Thread.Sleep(Interval);
        }

        public void Close()
        {
            if (!m_flag) return;
            m_flag = false;
            try
            {
                m_thread.Join();
            }
            catch(ThreadInterruptedException)
            {

            }
            catch(ThreadStateException)
            {

            }
        }

        public int Interval
        {
            get;
            set;
        }
    }
}
