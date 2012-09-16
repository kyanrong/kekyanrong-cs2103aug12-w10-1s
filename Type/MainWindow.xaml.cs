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

namespace Type
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string INPUT_WELCOME_TEXT = "Start typng...";
        private string[] COMMANDS_ACCEPTED = { "done", "archive", "undo", "edit", "clear" };
        private const string COMMANDS_ESCAPE_TOKEN = ":";
        private const string TAGS_ESCAPE_TOKEN = "#";
        private Key[] START_KEY_COMBINATION = { Key.LeftShift, Key.Space };

        private AutoComplete commandsAutoComplete;
        private AutoComplete tagsAutoComplete; //TODO
        private AutoComplete tasksAutoComplete; //TODO

        private Parser parser;

        private ShortcutKeyHook globalHook;

        private Boolean isForeground;
        private Boolean showingWelcomeText;

        public MainWindow()
        {
            //Forcibly improve the user's life.
            if (Type.Properties.Settings.Default.firstRun)
            {
                Embedder.Embed();
                Type.Properties.Settings.Default.firstRun = false;
            }

            InitializeComponent();

            commandsAutoComplete = new AutoComplete(COMMANDS_ACCEPTED);
            parser = new Parser(COMMANDS_ESCAPE_TOKEN, TAGS_ESCAPE_TOKEN);


            globalHook = new ShortcutKeyHook(this, START_KEY_COMBINATION);

            isForeground = true;
        }

        private void ShowWelcomeText()
        {
            if (!showingWelcomeText)
            {
                textBox1.Text = INPUT_WELCOME_TEXT;
                textBox1.Foreground = Brushes.LightGray;
            }
            showingWelcomeText = true;
        }

        private void HideWelcomeText(string input)
        {
            if (showingWelcomeText)
            {
                textBox1.Text = input;
                textBox1.Foreground = Brushes.Black;
                MoveCursorToBack();
            }
            showingWelcomeText = false;
        }

        private void MoveCursorToBack()
        {
            textBox1.Select(textBox1.Text.Length, 0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowWelcomeText();
            textBox1.Focus();
            HideWindow();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                ShowWelcomeText();
            }
            else
            {
                if (showingWelcomeText)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var change in e.Changes)
                    {
                        int offset = change.Offset;
                        int addedLength = change.AddedLength;
                        if (addedLength > 0)
                        {
                            sb.Append(textBox1.Text.Substring(offset, addedLength));
                        }
                    }
                    HideWelcomeText(sb.ToString());
                }
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    //Should parse and process the command here.
                    parser.parseCommand(textBox1.Text);
                    textBox1.Clear();
                    HideWindow();
                    break;

                case Key.Tab:
                    if (!showingWelcomeText)
                    {
                        if (parser.IsNonImplicitCommand(textBox1.Text))
                        {
                            string commandText = GetTokenWithoutPrefix(textBox1.Text);
                            string acText = commandsAutoComplete.CompleteToCommonPrefix(commandText);
                            textBox1.Text += acText;

                            MoveCursorToBack();
                        }
                        else if (parser.IsTag(textBox1.Text))
                        {
                            //Autocomplete Tag

                        }
                        else
                        {
                            //Autocomplete Task

                        }
                    }
                    break;

                case Key.Escape:
                    HideWindow();
                    break;
            }
        }

        private string GetTokenWithoutPrefix(string text)
        {
            return (text.Substring(1));
        }

        private void HideWindow()
        {
            if (isForeground)
            {
                this.Hide();
                isForeground = false;
            }
        }

        public void ShowWindow()
        {
            if (!isForeground)
            {
                this.Show();
                isForeground = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            globalHook.StopListening();
        }
    }
}
