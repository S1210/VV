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

namespace VV.CustomElements
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public const int YES = 1;
        public const int NO = 0;
        private const string S_YES = "Да";
        private const string S_NO = "Нет";
        public static int RESULT = 0;
        public CustomMessageBox(string message)
        {
            InitializeComponent();
            txtBlk.Text = message;
            RESULT = NO;
        }
        public CustomMessageBox(string message, string contentBtnYes, string contentBtnNo)
        {
            InitializeComponent();
            txtBlk.Text = message;
            btnOk.Visibility = Visibility.Visible;
            btnOk.Content = contentBtnYes;
            btnCancel.Content = contentBtnNo;
            RESULT = NO;
        }

        public static void Show(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                new CustomMessageBox(message).ShowDialog();
            });
        }
        public static void ShowYesNo(string message, string contentBtnYes, string contentBtnNo)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                new CustomMessageBox(message, contentBtnYes, contentBtnNo).ShowDialog();

            });
        }
        public static void ShowYesNo(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                new CustomMessageBox(message, S_YES, S_NO).ShowDialog();

            });
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            RESULT = YES;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            RESULT = NO;
            this.Close();
        }
    }
}
