using MyLib.CellAutomata.World.Life;
using MyLib.CellAutomata.World.Room;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeGameShow
{
    public partial class MainForm : Form
    {
        LifeGameView m_lifeGameView;
        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Load += (sender, e) => InitControl();
            Load += (sender, e) => LoadFinish();
            FormClosed += (sender, e) => FormClosingDoing();
            m_lifeGameView = new LifeGameView();
            m_lifeGameView.Update += (sender, e) => UpdateImage((Image)sender);
        }

        private void InitControl()
        {
            InitControlSetting();
            InitControlClickEvent();
        }

        private void InitControlSetting()
        {
            c_lifeGameShow.Image = m_lifeGameView.Image;
            m_lifeGameView.AddRootLife(new Unit(), new RoomPoint(9, 12));
            m_lifeGameView.AddRootLife(new Unit(), new RoomPoint(10, 15));
            m_lifeGameView.AddRootLife(new Unit(), new RoomPoint(11, 15));
            m_lifeGameView.AddRootLife(new Unit(), new RoomPoint(13, 10));
            m_lifeGameView.AddRootLife(new Unit(), new RoomPoint(8, 11));
            m_lifeGameView.AddRootLife(new Unit(), new RoomPoint(8, 12));
            m_lifeGameView.AddRootLife(new Unit(), new RoomPoint(12, 13));
        }

        private void InitControlClickEvent()
        {
            c_startButton.Click += (sender, e) =>
              {
                  m_lifeGameView.Start();
              };

            c_lifeGameShow.MouseClick += LifeGameView_Click;
        }

        private void LoadFinish()
        {

        }

        private void UpdateImage(Image image)
        {
            if (InvokeRequired)
            {
                Action<Image> handle = new Action<Image>(UpdateImage);
                BeginInvoke(handle, image);
            }
            else
                c_lifeGameShow.Image = image;
        }

        /// <summary>
        /// 获取鼠标处于网格的位置
        /// </summary>
        /// <returns></returns>
        private RoomPoint? GetMouseClickPoint(Point mouseLoc)
        {
            Point deviationPoint = new Point(mouseLoc.X - c_lifeGameShow.Location.X, mouseLoc.Y - c_lifeGameShow.Location.Y);
            if (deviationPoint.X < 0 || deviationPoint.Y < 0) return null;
            else
            {
                int x = (int)((deviationPoint.X / m_lifeGameView.WidthUnit)+0.5f);
                int y = (int)((deviationPoint.Y / m_lifeGameView.HeightUnit)+0.5f);
                RoomPoint result = new RoomPoint(x, y);
                return result;
            }
        }

        private void LifeGameView_Click(object sender, MouseEventArgs e)
        {
            var point = GetMouseClickPoint(e.Location);
            if (e.Button == MouseButtons.Left && !m_lifeGameView.WorldProperties.IsStart)
            {             
                if (point != null)
                    m_lifeGameView.AddRootLife(point.Value);
            }
            else if (e.Button == MouseButtons.Right && !m_lifeGameView.WorldProperties.IsStart)
            {
                if (point != null) m_lifeGameView.RemoveLife(point.Value);
            }

        }

        private void FormClosingDoing()
        {
            m_lifeGameView.Close();
            LoadingForm.Instance.Close();
        }

        #region 具体操作


        #endregion
    }
}
