
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WordleSolver
{
    public class LetterState
    {
        /// <summary>
        /// Letter state info
        /// </summary>
        public static List<LetterState> LetterBrain = new List<LetterState>();

        /// <summary>
        /// Letter object is matched (green) at letter index 0
        /// </summary>
        public static LetterState MatchedIndex0 { get; set; }
        /// <summary>
        /// Letter object is matched (green) at letter index 1
        /// </summary>
        public static LetterState MatchedIndex1 { get; set; }
        /// <summary>
        /// Letter object is matched (green) at letter index 2
        /// </summary>
        public static LetterState MatchedIndex2 { get; set; }
        /// <summary>
        /// Letter object is matched (green) at letter index 3
        /// </summary>
        public static LetterState MatchedIndex3 { get; set; }
        /// <summary>
        /// Letter object is matched (green) at letter index 4
        /// </summary>
        public static LetterState MatchedIndex4 { get; set; }

        /// <summary>
        /// Letter object is not indexed (orange) at letter index 0
        /// </summary>
        public static LetterState UnMatchedIndex0 { get; set; }
        /// <summary>
        /// Letter object is not indexed (orange) at letter index 1
        /// </summary>
        public static LetterState UnMatchedIndex1 { get; set; }
        /// <summary>
        /// Letter object is not indexed (orange) at letter index 2
        /// </summary>
        public static LetterState UnMatchedIndex2 { get; set; }
        /// <summary>
        /// Letter object is not indexed (orange) at letter index 3
        /// </summary>
        public static LetterState UnMatchedIndex3 { get; set; }
        /// <summary>
        /// Letter object is not indexed (orange) at letter index 4
        /// </summary>
        public static LetterState UnMatchedIndex4 { get; set; }



        /// <summary>
        /// The (capitalised) letter
        /// </summary>
        public string Letter { get; set; }
        /// <summary>
        /// The name of the exclude togglebutton associated with this letter
        /// </summary>
        public string ExcludeName => "tog" + Letter;
        /// <summary>
        /// The row of the keyboard this letter lives on
        /// </summary>
        public int KeyboardRow { get; set; }
        /// <summary>
        /// Whether or not the letter is currently marked as excluded
        /// </summary>
        public bool ExcludeIsToggled { get; set; }
        /// <summary>
        /// Signals whether the letter is in the wordle, but not in an indexed position (yet)
        /// </summary>
        public bool IsPresentButInDifferentPosition { get; set; }


        public static List<WordMatch> RecalcMatches(MainWindow mw)
        {
            List<WordMatch> results = new List<WordMatch>();

            // initial working set
            var working =
                (from a in mw.USDict
                 select new WordMatch()
                 {
                     Word = a,
                     Score = 0
                 }).ToList();

            string workingMatched = "";
            string workingUnmatched = "";

            // filter based on GREEN positions first
            if (MatchedIndex0 != null)
            {
                results =
                    (from a in working
                     where a.Word.Substring(0, 1).ToUpper() == MatchedIndex0.Letter
                     select new WordMatch()
                     {
                         Word = a.Word,
                         Score = a.Score += 20
                     }).ToList();

                working = results;
            }

            if (MatchedIndex1 != null)
            {
                results =
                    (from a in working
                     where a.Word.Substring(1, 1).ToUpper() == MatchedIndex1.Letter
                     select new WordMatch()
                     {
                         Word = a.Word,
                         Score = a.Score += 20
                     }).ToList();

                working = results;
            }

            if (MatchedIndex2 != null)
            {
                results =
                    (from a in working
                     where a.Word.Substring(2, 1).ToUpper() == MatchedIndex2.Letter
                     select new WordMatch()
                     {
                         Word = a.Word,
                         Score = a.Score += 20
                     }).ToList();

                working = results;
            }

            if (MatchedIndex3 != null)
            {
                results =
                    (from a in working
                     where a.Word.Substring(3, 1).ToUpper() == MatchedIndex3.Letter
                     select new WordMatch()
                     {
                         Word = a.Word,
                         Score = a.Score += 20
                     }).ToList();

                working = results;
            }

            if (MatchedIndex4 != null)
            {
                results =
                    (from a in working
                     where a.Word.Substring(4, 1).ToUpper() == MatchedIndex4.Letter
                     select new WordMatch()
                     {
                         Word = a.Word,
                         Score = a.Score += 20
                     }).ToList();

                working = results;
            }

            

            // now filter on unmatched positions (ignoring the index they are in)
            if (UnMatchedIndex0 != null)
            {
                workingMatched = MatchedIndex0 != null ? MatchedIndex0.Letter : "";
                workingUnmatched = UnMatchedIndex0.Letter;
                results =
                    (from a in working
                     where a.Word.ToUpper().Contains(workingUnmatched) &&
                     a.Word.Substring(0, 1).ToUpper() != workingMatched
                     select new WordMatch()
                     {
                         Word = a.Word,
                         Score = a.Score += (7 * a.Word.ToUpper().Count(x => x == char.Parse(workingUnmatched)))
                     }).ToList();

                working = results;
            }

            if (UnMatchedIndex1 != null)
            {
                workingMatched = MatchedIndex1 != null ? MatchedIndex1.Letter : "";
                workingUnmatched = UnMatchedIndex1.Letter;
                results =
                    (from a in working
                     where a.Word.ToUpper().Contains(workingUnmatched) &&
                     a.Word.Substring(1, 1).ToUpper() != workingMatched
                     select new WordMatch()
                     {
                         Word = a.Word,
                         Score = a.Score += (7 * a.Word.ToUpper().Count(x => x == char.Parse(workingUnmatched)))
                     }).ToList();

                working = results;
            }

            if (UnMatchedIndex2 != null)
            {
                workingMatched = MatchedIndex2 != null ? MatchedIndex2.Letter : "";
                workingUnmatched = UnMatchedIndex2.Letter;
                results =
                    (from a in working
                     where a.Word.ToUpper().Contains(workingUnmatched) &&
                     a.Word.Substring(2, 1).ToUpper() != workingMatched
                     select new WordMatch()
                     {
                         Word = a.Word,
                         Score = a.Score += (7 * a.Word.ToUpper().Count(x => x == char.Parse(workingUnmatched)))
                     }).ToList();

                working = results;
            }

            if (UnMatchedIndex3 != null)
            {
                workingMatched = MatchedIndex3 != null ? MatchedIndex3.Letter : "";
                workingUnmatched = UnMatchedIndex3.Letter;
                results =
                    (from a in working
                     where a.Word.ToUpper().Contains(workingUnmatched) &&
                     a.Word.Substring(3, 1).ToUpper() != workingMatched
                     select new WordMatch()
                     {
                         Word = a.Word,
                         Score = a.Score += (7 * a.Word.ToUpper().Count(x => x == char.Parse(workingUnmatched)))
                     }).ToList();

                working = results;
            }

            if (UnMatchedIndex4 != null)
            {
                workingMatched = MatchedIndex4 != null ? MatchedIndex4.Letter : "";
                workingUnmatched = UnMatchedIndex4.Letter;
                results =
                    (from a in working
                     where a.Word.ToUpper().Contains(workingUnmatched) &&
                     a.Word.Substring(4, 1).ToUpper() != workingMatched
                     select new WordMatch()
                     {
                         Word = a.Word,
                         Score = a.Score += (7 * a.Word.ToUpper().Count(x => x == char.Parse(workingUnmatched)))
                     }).ToList();

                working = results;
            }

            // now filter out excluded letters
            foreach (var x in GetAllExcludedLetters())
            {
                results =
                    (from a in working
                     where !a.Word.ToUpper().Contains(x)
                     select a).ToList();

                working = results;
            }

            return results
                .OrderByDescending(a => a.Score)
                .ThenBy(b => b.Word)
                .ToList();
        }

        /// <summary>
        /// Returns a list of excluded letters
        /// </summary>
        private static List<string> GetAllExcludedLetters()
        {
            var lookup =
                (from a in LetterBrain
                 where a.ExcludeIsToggled == true
                 select a.Letter).ToList();

            return lookup;
        }

        private static List<string> GetAllNonExcludedLetters()
        {
            var lookup =
                (from a in LetterBrain
                 where a.ExcludeIsToggled == false
                 select a.Letter).ToList();

            return lookup;
        }


        /// <summary>
        /// Signals whether the supplied letter is currently in the excluded list
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public static bool IsLetterExcluded(string letter)
        {
            var lookup = LetterBrain.Where(a => a.Letter == letter).FirstOrDefault();
            if (lookup != null)
            {
                return lookup.ExcludeIsToggled;
            }

            return false;
        }

        /// <summary>
        /// Changes toggle value for an exclude letter in the letter brain
        /// </summary>
        /// <param name="togName"></param>
        /// <param name="state"></param>
        public static void SetExcludeToggle(string togName, bool state)
        {
            var lookup = LetterBrain.Where(a => a.ExcludeName == togName).FirstOrDefault();

            if (lookup != null)
            {
                lookup.ExcludeIsToggled = state;
            }
        }

        /// <summary>
        /// Initialisation
        /// </summary>
        /// <param name="mw"></param>
        public static void Init(MainWindow mw)
        {
            LetterBrain = new List<LetterState>
            {
                new LetterState { Letter = "Q", KeyboardRow = 0 },
                new LetterState { Letter = "W", KeyboardRow = 0 },
                new LetterState { Letter = "E", KeyboardRow = 0 },
                new LetterState { Letter = "R", KeyboardRow = 0 },
                new LetterState { Letter = "T", KeyboardRow = 0 },
                new LetterState { Letter = "Y", KeyboardRow = 0 },
                new LetterState { Letter = "U", KeyboardRow = 0 },
                new LetterState { Letter = "I", KeyboardRow = 0 },
                new LetterState { Letter = "O", KeyboardRow = 0 },
                new LetterState { Letter = "P", KeyboardRow = 0 },

                new LetterState { Letter = "A", KeyboardRow = 1 },
                new LetterState { Letter = "S", KeyboardRow = 1 },
                new LetterState { Letter = "D", KeyboardRow = 1 },
                new LetterState { Letter = "F", KeyboardRow = 1 },
                new LetterState { Letter = "G", KeyboardRow = 1 },
                new LetterState { Letter = "H", KeyboardRow = 1 },
                new LetterState { Letter = "J", KeyboardRow = 1 },
                new LetterState { Letter = "K", KeyboardRow = 1 },
                new LetterState { Letter = "L", KeyboardRow = 1 },

                new LetterState { Letter = "Z", KeyboardRow = 2 },
                new LetterState { Letter = "X", KeyboardRow = 2 },
                new LetterState { Letter = "C", KeyboardRow = 2 },
                new LetterState { Letter = "V", KeyboardRow = 2 },
                new LetterState { Letter = "B", KeyboardRow = 2 },
                new LetterState { Letter = "N", KeyboardRow = 2 },
                new LetterState { Letter = "M", KeyboardRow = 2 },
            };

            // add the exclude keys to the UI
            foreach (var k in LetterBrain)
            {
                // create togglebutton and formatting
                var tg = new ToggleButton();
                tg.FontSize = 20;
                tg.FontWeight = FontWeights.Bold;
                tg.Background = Brushes.Transparent;
                tg.Margin = new Thickness(5);
                tg.Width = 30;
                tg.Name = k.ExcludeName;
                tg.Content = k.Letter;

                // add event triggers
                tg.Click += new RoutedEventHandler(mw.ExcludeToggle_Click);
                var eventTriggerChecked = new EventTrigger();
                

                // add to correct row on screen
                switch (k.KeyboardRow)
                {
                    case 0:
                        mw.spRow01.Children.Add(tg);
                        break;

                    case 1:
                        mw.spRow02.Children.Add(tg);
                        break;

                    case 2:
                        mw.spRow03.Children.Add(tg);
                        break;
                }
            }

        }
    }
}
