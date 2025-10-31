using System;
using System.Threading;
using System.Windows;

namespace ThreadDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartProgress_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(50);
                    Dispatcher.Invoke(() => progressBar.Value = i);
                }
            });
            t.Start();
        }

        private void StartHighPriority_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                for (int i = 0; i < 10; i++)
                {
                    Dispatcher.Invoke(() => statusText.Text = $"Итерация {i + 1}, приоритет: {Thread.CurrentThread.Priority}");
                    Thread.Sleep(200);
                }
            });
            t.Start();
        }

        private void StartWithoutDispatcher_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    progressBar.Value = 50; // ошибка
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Ошибка: " + ex.Message));
                }
            });
            t.Start();
        }
    }
}