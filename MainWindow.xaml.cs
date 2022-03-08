using System;
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
using JotUp.Models;
using JotUp.Services;
namespace JotUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Displays all jottings in jotup.
            ShowAllDocument();
            todayDate.Text = DateTime.Now.ToString();
        }
        public void ShowAllDocument()
        {
            var getAll = JotUpDB.GetAllJottings().OrderByDescending(page => page.LastUsageTime);
            DisplayContent.ItemsSource = getAll;
            DisplayContent.DisplayMemberPath =  nameof(JotPage.Title);

        }
        //Starts new jotting.
        private void NewPage_Click(object sender, RoutedEventArgs e)
        {
            GettingDocument.documentId = null;
            DocumentEditor documentEditor = new DocumentEditor();
            JotPage newJotPage = new JotPage();
            GettingDocument.documentTitle = "Untitled";
            GettingDocument.documentContent = newJotPage.Content;
            GettingDocument.dateTime = newJotPage.LastUsageTime;
            this.Close();
            documentEditor.Show(); 
        }
        //Opens previous document for editing
        private void OpenPage_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DisplayContent.SelectedItem;
            if (selectedItem is JotPage)
            {
                DisplayContent_SelectionChanged();
                DocumentEditor documentEditior = new DocumentEditor();
                this.Close();
                documentEditior.ShowDialog();
            }
                        
        }
        //Deletes the selected page.
        private void DeletePage_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DisplayContent.SelectedItem;

            //JotPage page = (JotPage)selectedItem;
            //JotPage page2 = selectedItem as JotPage;

            if (selectedItem is JotPage page3)
            {
                bool deleted = JotUpDB.DeleteJottings(page3.PageId);
                if (deleted) MessageBox.Show("Successfully deleted");
            }
            ShowAllDocument();
        }
        private void DisplayContent_SelectionChanged()
        {
            JotPage SelectedDocument = (JotPage)DisplayContent.SelectedItem;
            GettingDocument.documentTitle = SelectedDocument.Title;
            GettingDocument.documentId = SelectedDocument.PageId;
            GettingDocument.documentContent = SelectedDocument.Content;
            GettingDocument.documentCategory = SelectedDocument.Category;
            GettingDocument.dateTime = SelectedDocument.LastUsageTime;
        }

        private void DisplayContent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = DisplayContent.SelectedItem;
            if (selectedItem is JotPage)
            {
                DisplayContent_SelectionChanged();
                DocumentEditor documentEditior = new DocumentEditor();
                this.Close();
                documentEditior.ShowDialog();
            }
        }
    }
    public static class GettingDocument
    {
        public static string documentTitle { get; set; }
        public static int? documentId { get; set; }
        public static string documentContent { get; set; }
        public static string documentCategory { get; set; }
        public static DateTime dateTime { get; set; }
    }
}