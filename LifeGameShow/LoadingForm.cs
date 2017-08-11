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
    public partial class LoadingForm : Form
    {
        static LoadingForm s_instance;
        BackgroundWorker m_loadingWorker;
        public static LoadingForm Instance
        {
            get
            {
                return s_instance;
            }
        }
        public LoadingForm()
        {
            s_instance = this;
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Load += (sender, e) => InitControl();
        }

        private void InitControl()
        {
            InitControlSetting();
            InitControlClickEvent();
        }

        private void InitControlSetting()
        {
            m_loadingWorker = new BackgroundWorker();
            m_loadingWorker.RunWorkerCompleted += (sender, e) => LoadFinish();
            m_loadingWorker.RunWorkerAsync();
        }

        private void InitControlClickEvent()
        {

        }

        private void LoadFinish()
        {
            Hide();
            MainForm form = new MainForm();
            form.Show();          
        }
    }
}
