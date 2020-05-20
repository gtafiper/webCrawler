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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string website = this.websiteTextBox.Text;
            string show = this.showTextBox.Text;
            this.pagesListBox.Items.Add(website);
            this.showsListBox.Items.Add(show);
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
                showsListBox.Items.RemoveAt(showsListBox.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Please select a Item to remove");
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
                MessageBox.Show("Please select a Item to remove");
            }
        }
    }
}