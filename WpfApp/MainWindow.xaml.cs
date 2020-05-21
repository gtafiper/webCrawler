using System;
using System.Collections;
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
using AnimuCrawler;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FileReader reader = new FileReader();
        List<string> listOfShows = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            List<string> listOfShows = reader.ReadShows();
            if (listOfShows != null)
            {
                foreach (var item in listOfShows)
                {
                    showsListBox.Items.Add(item);
                }
            }
        }


       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string website = this.websiteTextBox.Text;
           
            string show = this.showTextBox.Text;
            reader.WriteShow(show);
            this.pagesListBox.Items.Add(website);
            
            foreach (var item in reader.ReadShows())
            {
                if (!showsListBox.Items.Contains(item))
                {
                    showsListBox.Items.Add(item);
                }
            }
            
            websiteTextBox.Clear();
            showTextBox.Clear();
        }

        private void pagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void RemoveShowButton(object sender, RoutedEventArgs e)
        {
            if (showsListBox.SelectedIndex > -1)
            {
                reader.DeleteShow(sender.ToString());
                showsListBox.Items.RemoveAt(showsListBox.SelectedIndex);
                
            }
            else
            {
                MessageBox.Show("Please select a Show to remove");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (pagesListBox.SelectedIndex > -1)
            {
                pagesListBox.Items.RemoveAt(pagesListBox.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Please select a Page to remove");
            }
        }

        
    }
}