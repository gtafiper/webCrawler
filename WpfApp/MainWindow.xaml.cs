using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AnimuCrawler;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<AnimuCrawlerBot> crawlers;
        private BotManager manager;
        private AnimuCrawlerBot active;

        public BotManager Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            Manager = BotManager.GetInstance();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = Manager.CrawlersRunning;
            isButtonsEnabeld(false);
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            AddCrawler winAdd = new AddCrawler();
            winAdd.Show();
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            Manager.StartBot(active);
        }

        private void OnPause(object sender, RoutedEventArgs e)
        {
            Manager.EndBot(active);
        }

        private void OnRemove(object sender, RoutedEventArgs e)
        {
            Manager.RemoveBot(active);

        }

        private void datarow_Selected(object sender, RoutedEventArgs e)
        {
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = dataGrid.SelectedItem as AnimuCrawlerBot;
            if (temp != null)
            {
                isButtonsEnabeld(true);
                active = temp;
                active.Seen();
            }
            else {
                isButtonsEnabeld(false);
            }
        }

        private void isButtonsEnabeld(bool result) {
            this.btnStart.IsEnabled = result;
            this.btnPause.IsEnabled = result;
            this.btnRemove.IsEnabled = result;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            manager.EndAllBots();
        }
    }
}