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

        public ObservableCollection<AnimuCrawlerBot> Crawlers
        {
            get { return crawlers; }
            set { crawlers = value; }
        }

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
            Manager.AddBot("https://stackoverflow.com/questions/5809816/datagrid-binding-in-wpf", "dialogbox");
            isButtonsEnabeld(false);
        }




        private void OnAdd(object sender, RoutedEventArgs e)
        {
            //AddCrawler winAdd = new AddCrawler();
            //winAdd.Show();
            Manager.AddBot("https://www.wcoanimedub.tv/", "Tower of God");
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            active.StartWatching();
        }

        private void OnPause(object sender, RoutedEventArgs e)
        {
            active.StopWatching();
        }

        private void OnRemove(object sender, RoutedEventArgs e)
        {
            Manager.RemoveBot(active);

        }

        private void dataGrid_Selected(object sender, RoutedEventArgs e)
        {
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = dataGrid.SelectedItem as AnimuCrawlerBot;
            if (temp != null)
            {
                isButtonsEnabeld(true);
                active = temp;
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
    }
}