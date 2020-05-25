using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SeriesCrawler;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<SeriesWebCrawler> crawlers;
        private ICrawlerManager manager;
        private SeriesWebCrawler active;

        public MainWindow()
        {
            InitializeComponent();
            manager = CrawlerManager.GetInstance();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = manager.CrawlersRunning;
            isButtonsEnabeld(false);
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            AddCrawler winAdd = new AddCrawler();
            winAdd.Show();
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            manager.StartBot(active);
        }

        private void OnPause(object sender, RoutedEventArgs e)
        {
            manager.EndBot(active);
        }

        private void OnRemove(object sender, RoutedEventArgs e)
        {
            manager.RemoveBot(active);

        }

        private void datarow_Selected(object sender, RoutedEventArgs e)
        {
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = dataGrid.SelectedItem as SeriesWebCrawler;
            if (temp != null)
            {
                isButtonsEnabeld(true);
                active = temp;
                manager.MarkAsSeen(active);
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