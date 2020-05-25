using SeriesCrawler;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for AddCrawler.xaml
    /// </summary>
    public partial class AddCrawler : Window
    {
        ICrawlerManager manager;

        public AddCrawler()
        {
            InitializeComponent();
            manager = CrawlerManager.GetInstance();
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            string uri = this.txtURL.Text;
            string keyword = this.txtKeyword.Text;
            int time;
            switch (this.comboTime.SelectedIndex)
            {
                case 0:
                    time = 60000;
                    break;
                case 1:
                    time = 300000;
                    break;
                case 2:
                    time = 900000;
                    break;
                case 3:
                    time = 3600000;
                    break;
                case 4:
                    time = 86400000;
                    break;
                default:
                    time = 60000;
                    break;
            }

            manager.AddBot(uri, keyword, time);

            this.Close();
        }
    }
}
