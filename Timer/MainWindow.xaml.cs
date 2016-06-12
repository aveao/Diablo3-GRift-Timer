using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime starttime;
        DispatcherTimer dt = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dt.Tick += Dt_Tick;
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            if (!paused)
            {
                textBlock.Text = (DateTime.Now - starttime).ToString();
            }
        }

        bool paused = false;
        bool handleesc = true;
        DateTime pausetime;

        private void textBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            starttime = DateTime.Now + new TimeSpan(0,15,0);
            dt.Start();
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var laststate = Keyboard.GetKeyStates(Key.Escape);
            if ((laststate == KeyStates.None) || (laststate == KeyStates.Toggled))
            {
                handleesc = true;
            }
            if (((Keyboard.GetKeyStates(Key.Escape) == KeyStates.Down) || ((int)Keyboard.GetKeyStates(Key.Escape) == ((int)KeyStates.Down + (int)KeyStates.Toggled))) && handleesc)
            {
                handleesc = false;
                paused = !paused;
                if (paused)
                {
                    pausetime = DateTime.Now;
                }
                else
                {
                    starttime += DateTime.Now - pausetime;
                }
            }   
        }
    }
}
