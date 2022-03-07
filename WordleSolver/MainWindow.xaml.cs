using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WordleSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> USDict = new List<string>();
        public List<LetterState> Letters = new List<LetterState>();
        public List<WordMatch> Words = new List<WordMatch>();

        public MainWindow()
        {
            InitializeComponent();

            // parse dictionary
            USDict = File.ReadAllLines("5LetterDictionary.txt").ToList();

            // build letter matrix
            LetterState.Init(this);

            
            //dgResults.ItemsSource = LetterState.RecalcMatches(this);
        }

        /// <summary>
        /// Only allow letters in textboxes on previewkeydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            // is it an actual letter?
            var isLetter = !regex.IsMatch(e.Text);

            if (isLetter)
            {
                // check whether it's not excluded
                var excludedCheck = LetterState.IsLetterExcluded(e.Text.ToUpper());
                if (excludedCheck)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Fired when an exclude toggle is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExcludeToggle_Click(object sender, RoutedEventArgs e)
        {
            string sourceName = ((FrameworkElement)e.Source).Name;
            string senderName = ((FrameworkElement)sender).Name;

            var togg = (ToggleButton)sender as ToggleButton;

            LetterState.SetExcludeToggle(sourceName, togg.IsChecked.Value);

            RecalculateWords();

            /*
            Debug.WriteLine($"Routed event handler attached to {senderName}, " +
                $"triggered by the Click routed event raised by {sourceName}.");
                */
        }

        /// <summary>
        /// Fired when text has finally changed in the GREEN textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbg_TextChanged(object sender, TextChangedEventArgs e)
        {
            // is this a letter or null?
            var tb = (TextBox)sender as TextBox;
            if (tb.Text.Trim() == "")
            {
                // check and remove any green entries from this position
                switch (tb.Name)
                {
                    case "tbg0":
                        LetterState.MatchedIndex0 = null;
                        break;
                    case "tbg1":
                        LetterState.MatchedIndex1 = null;
                        break;
                    case "tbg2":
                        LetterState.MatchedIndex2 = null;
                        break;
                    case "tbg3":
                        LetterState.MatchedIndex3 = null;
                        break;
                    case "tbg4":
                        LetterState.MatchedIndex4 = null;
                        break;
                }
            }
            else
            {
                // mark this position as matched (green)
                var letterObject = LetterState.LetterBrain.Where(a => a.Letter == tb.Text.Trim().ToUpper()).FirstOrDefault();
                switch (tb.Name)
                {
                    case "tbg0":
                        LetterState.MatchedIndex0 = letterObject;
                        break;
                    case "tbg1":
                        LetterState.MatchedIndex1 = letterObject;
                        break;
                    case "tbg2":
                        LetterState.MatchedIndex2 = letterObject;
                        break;
                    case "tbg3":
                        LetterState.MatchedIndex3 = letterObject;
                        break;
                    case "tbg4":
                        LetterState.MatchedIndex4 = letterObject;
                        break;
                }
            }

            RecalculateWords();
        }

        /// <summary>
        /// Fired when text has finally changed in the ORANGE textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tby_TextChanged(object sender, TextChangedEventArgs e)
        {
            // is this a letter or null?
            var tb = (TextBox)sender as TextBox;
            if (tb.Text.Trim() == "")
            {
                // check and remove any green entries from this position
                switch (tb.Name)
                {
                    case "tby0":
                        LetterState.UnMatchedIndex0 = null;
                        break;
                    case "tby1":
                        LetterState.UnMatchedIndex1 = null;
                        break;
                    case "tby2":
                        LetterState.UnMatchedIndex2 = null;
                        break;
                    case "tby3":
                        LetterState.UnMatchedIndex3 = null;
                        break;
                    case "tby4":
                        LetterState.UnMatchedIndex4 = null;
                        break;
                }
            }
            else
            {
                // mark this position as matched (green)
                var letterObject = LetterState.LetterBrain.Where(a => a.Letter == tb.Text.Trim().ToUpper()).FirstOrDefault();
                switch (tb.Name)
                {
                    case "tby0":
                        // check for GREEN entry first
                        if (LetterState.MatchedIndex0 == null)
                        {
                            LetterState.UnMatchedIndex0 = letterObject;
                        }
                        else
                        {
                            tb.Text = "";
                        }                        
                        break;
                    case "tby1":
                        // check for GREEN entry first
                        if (LetterState.MatchedIndex1 == null)
                        {
                            LetterState.UnMatchedIndex1 = letterObject;
                        }
                        else
                        {
                            tb.Text = "";
                        }
                        break;
                    case "tby2":
                        // check for GREEN entry first
                        if (LetterState.MatchedIndex2 == null)
                        {
                            LetterState.UnMatchedIndex2 = letterObject;
                        }
                        else
                        {
                            tb.Text = "";
                        }
                        break;
                    case "tby3":
                        // check for GREEN entry first
                        if (LetterState.MatchedIndex3 == null)
                        {
                            LetterState.UnMatchedIndex3 = letterObject;
                        }
                        else
                        {
                            tb.Text = "";
                        }
                        break;
                    case "tby4":
                        // check for GREEN entry first
                        if (LetterState.MatchedIndex4 == null)
                        {
                            LetterState.UnMatchedIndex4 = letterObject;
                        }
                        else
                        {
                            tb.Text = "";
                        }
                        break;
                }
            }

            RecalculateWords();
        }

        /// <summary>
        /// Does a full recalculation based on current state and updates the datagrid
        /// </summary>
        public void RecalculateWords()
        {
            var data = LetterState.RecalcMatches(this);
            dgResults.ItemsSource = data;
        }
    }
}
