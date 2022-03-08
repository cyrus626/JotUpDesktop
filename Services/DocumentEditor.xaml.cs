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
using System.Windows.Shapes;
using JotUp.Models;
using JotUp.Models.Unsaved;
namespace JotUp.Services
{
    /// <summary>
    /// Interaction logic for DocumentEditor.xaml
    /// </summary>
    public partial class DocumentEditor : Window
    {
        int? jotterId = GettingDocument.documentId;
        //public static string UnsavedTitle { get; set; }
        //public static string UnsavedContent { get; set; }
        //public static string UnsavedCategory { get; set; }
        //public static DateTime LastUsageTime { get; set; }
        public DocumentEditor()
        {
            //Document Editor should be able edit any document 
            //either it new or existing before
            InitializeComponent();
            ShowPageContent();
        }

        private void ShowPageContent()
        {
            #region
            //Showing the title and contents
            
            string jotterTitle = GettingDocument.documentTitle;
            string jotterContents = GettingDocument.documentContent;
            if (jotterId == null)
            {
                DocumentTitle.Text = "New document";
                PageContents.AppendText("Enter texts, use freely");
                jotterId = null;
                displayTime.Text = " Never saved!";
            }
            else
            {
                DocumentTitle.Text = jotterTitle;
                PageContents.AppendText(jotterContents);
                CategoryList.Text = GettingDocument.documentCategory;
                displayTime.Text = " " + GettingDocument.dateTime.ToString();
            }
            #endregion
        }

        private void DiscardButton_Click(object sender, RoutedEventArgs e)
        {
            //Any recent insertions will not be upadated.
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }

        // Convertion of Rich text box contents to string.
        private string ConvertDocumentToString(RichTextBox richTextBox)
        {
            TextRange textRange = new TextRange(
                richTextBox.Document.ContentStart, 
                richTextBox.Document.ContentEnd);
            return textRange.Text;
        }
        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Saves or updates a new document.
            JotPage jotPage = new JotPage();
            string title = DocumentTitle.Text.Trim().ToString();
            jotPage.Title = title;
            jotPage.Content = ConvertDocumentToString(PageContents).ToString();
            jotPage.Category = CategoryList.Text.ToString();
            jotPage.LastUsageTime = DateTime.Now;
            // If Id is null the DB will create new one else use the one available.
            if (jotterId != null)
            {
                jotPage.PageId = (int)jotterId; 
            }

            JotUpDB.StoreJottings(jotPage);
            string message = $"Saved!";
            MessageBox.Show(message, title);
        }
        //private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Binding binding = new Binding();
        //    binding.Source= CategoryList;
        //    binding.Path = new PropertyPath("SelectedItem");
        //}
    }
}
