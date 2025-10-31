using System;
using System.Threading;
using System.Windows;

namespace ThreadPriorityDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartThreads_Click(object sender, RoutedEventArgs e)
        {
            Thread t1 = new Thread(() => Count(txt1)) { Priority = ThreadPriority.Lowest };
            Thread t2 = new Thread(() => Count(txt2)) { Priority = ThreadPriority.Normal };
            Thread t3 = new Thread(() => Count(txt3)) { Priority = ThreadPriority.Highest };

            t1.Start();
            t2.Start();
            t3.Start();

            new Thread(() =>
            {
                t1.Join();
                t2.Join();
                t3.Join();

                Dispatcher.Invoke(() => resultText.Text = "Все потоки завершены ✅");
            }).Start();
        }

        private void Count(System.Windows.Controls.TextBlock txt)
        {
            for (int i = 1; i <= 100; i++)
            {
                int val = i;
                Dispatcher.Invoke(() => txt.Text = val.ToString());
                Thread.Sleep(100);
            }
        }
    }
}