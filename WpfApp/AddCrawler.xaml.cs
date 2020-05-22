using AnimuCrawler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for AddCrawler.xaml
    /// </summary>
    public partial class AddCrawler : Window
    {
        BotManager manager;
        public AddCrawler()
        {
            InitializeComponent();
            manager = BotManager.GetInstance();
            
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            string uri = this.txtURL.Text;
            Console.WriteLine(uri);
            string keyword = this.txtKeyword.Text;
            Console.WriteLine(keyword);
            int time = 60000;
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
                    break;
            }

            manager.AddBot(uri, keyword, time);
            this.Close();
        }
    }
}
