using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Lab3;

namespace SavingActivityState
{
    [Activity(Label = "Lab3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int score;
        int quoteIndex;
        string quoteText;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var quoteCollection = new QuoteBank();
            quoteCollection.LoadQuotes();

            //Create variables for all of the TextViews
            var quotationTextView = FindViewById<TextView>(Resource.Id.quoteTextView);
            var enteredNameLabel = FindViewById<TextView>(Resource.Id.answerInputLabel);
            var resultLabel = FindViewById<TextView>(Resource.Id.wrongRightLabel);
            var scoreBoard = FindViewById<TextView>(Resource.Id.scoreBoardLabel);

            //Get the current quote index and put it in the variable
            if (savedInstanceState != null)
            {
                quoteIndex = savedInstanceState.GetInt("quoteIndex");
                quoteText = quoteCollection.Quotes[quoteIndex].Quotation;
                score = savedInstanceState.GetInt("score");
            }
            else
            {
                quoteText = quoteCollection.GetNextQuote().Quotation;
                score = 0;
            }

            // Create the quote collection and display the current quote Im Trying to 
            //prevent the app from going the next quote when the activity is destroyed

            quotationTextView.Text = quoteText;
            scoreBoard.Text = score.ToString();


            // Display another quote when the "Next Quote" button is tapped
            var nextButton = FindViewById<Button>(Resource.Id.nextButton);
            nextButton.Click += delegate 
            {
                quoteCollection.GetNextQuote();
                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
                resultLabel.Text = "";
                enteredNameLabel.Text = "";
                quoteIndex = quoteCollection.Quotes.IndexOf(quoteCollection.CurrentQuote);
            };

            var enterButton = FindViewById<Button>(Resource.Id.enterButton);
            enterButton.Click += (sender, e) =>
            {
                var quoteName = quoteCollection.CurrentQuote.Person;
                if (quoteName == enteredNameLabel.Text)
                {
                    score++;
                    resultLabel.Text = "Correct Answer!";
                    scoreBoard.Text = score.ToString();
                }
                else
                {
                    resultLabel.Text = "Wrong! It was " + quoteName;

                }
            };
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("score", score);
            outState.PutInt("quoteIndex", quoteIndex);
            base.OnSaveInstanceState(outState);
        }
    }
}