using System;
using System.Collections.Generic;
using System.Linq;
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

namespace timer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime startTime, 
        pauseTime;
        TimeSpan pauseSpan;
        DispatcherTimer timer1 = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture =
                  new System.Globalization.CultureInfo("en-US");
            timer1_Tick(null, null);
            timer1.Interval = System.TimeSpan.FromMilliseconds(1000);
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }
        void timer1_Tick(object sender, EventArgs e)
        {
            if ((bool)checkBox1.IsChecked)
            {
                TimeSpan s = DateTime.Now - startTime - pauseSpan;
                label1.Text = string.Format("{0}:{1}", s.Minutes * 60 + s.Seconds, s.Milliseconds / 100);
            }
            else
                label1.Text = DateTime.Now.ToLongTimeString();
        }

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBox1.IsChecked)
            {
                startTime = DateTime.Now;
                pauseSpan = TimeSpan.Zero;
                timer1.Interval = TimeSpan.FromMilliseconds(100);
            }
            else
            timer1.Interval = TimeSpan.FromMilliseconds(1000);
            timer1_Tick(null, null);
            button1.IsEnabled = button2.IsEnabled =
            (bool)checkBox1.IsChecked;
            timer1.IsEnabled = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)

        {
            timer1.IsEnabled = ! timer1.IsEnabled;
            if (timer1.IsEnabled)
                pauseSpan += DateTime.Now - pauseTime;
            else
                pauseTime = DateTime.Now;
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            startTime = DateTime.Now;
            pauseSpan = TimeSpan.Zero;
            label1.Text = "0:0";
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            Title = WindowState == WindowState.Minimized ?
            label1.Text : "Clock";
        }
    }
}
