using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Xofel.AutoComplete;

namespace Type
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string INPUT_WELCOME_TEXT = "Start typng...";

        private AutoComplete ac;
        private Boolean isShowingWelcomeText;

        public MainWindow()
        {
            InitializeComponent();

            string[] commands = { "done", "clear", "archive", "undo", "edit" };
            ac = new AutoComplete(commands);

            ShowWelcomeText();
        }

        private void ShowWelcomeText()
        {
            if (!isShowingWelcomeText)
            {
                textBox1.Text = INPUT_WELCOME_TEXT;
                textBox1.Foreground = Brushes.LightGray;
            }
            isShowingWelcomeText = true;
        }

        private void HideWelcomeText()
        {
            if (isShowingWelcomeText)
            {
                string changedText = textBox1.Text;
                string input = changedText.Substring(changedText.Length - 1);
                textBox1.Text = input;
                textBox1.Select(input.Length, 0);
                textBox1.Foreground = Brushes.Black;
            }
            isShowingWelcomeText = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                ShowWelcomeText();
            }
            else
            {
                HideWelcomeText();
            }
        }
    }
}
