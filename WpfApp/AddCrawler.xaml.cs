using SeriesCrawler;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for AddCrawler.xaml
    /// </summary>
    public partial class AddCrawler : Window
    {
        private static readonly int TIME_1_MIN = 60000;
        private static readonly int TIME_5_MIN = 300000;
        private static readonly int TIME_15_MIN = 900000;
        private static readonly int TIME_1_HOUR = 3600000;
        private static readonly int TIME_1_DAY = 86400000;

        private readonly ICrawlerManager manager;

        public AddCrawler()
        {
            InitializeComponent();
            manager = CrawlerManager.GetInstance();
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            string uri = this.txtURL.Text;
            string keyword = this.txtKeyword.Text;
            var time = this.comboTime.SelectedIndex switch
            {
                0 => TIME_1_MIN,
                1 => TIME_5_MIN,
                2 => TIME_15_MIN,
                3 => TIME_1_HOUR,
                4 => TIME_1_DAY,
                _ => TIME_1_MIN,
            };
            manager.AddBot(uri, keyword, time);

            this.Close();
        }

    }
}
