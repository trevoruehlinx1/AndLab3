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

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //Get the current score and put it in the var!
            if (savedInstanceState != null)
                score = savedInstanceState.GetInt("score");
            else score = 0;

            //Get the current quote index and put it in the variable
            if (savedInstanceState != null)
                quoteIndex = savedInstanceState.GetInt("quoteIndex");
            else quoteIndex = 0;

            // Create the quote collection and display the current quote
            //Im Trying to keep the current quote when activity is destroyed
            var quoteCollection = new QuoteBank();
            quoteCollection.LoadQuotes();
            if (quoteIndex > -1)
                quoteCollection.GetSpecificQuote(quoteIndex);
            else 
                quoteCollection.GetNextQuote();


            //Create variables for all of the TextViews
            var quotationTextView = FindViewById<TextView>(Resource.Id.quoteTextView);
            quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
            var enteredNameLabel = FindViewById<TextView>(Resource.Id.answerInputLabel);
            var resultLabel = FindViewById<TextView>(Resource.Id.wrongRightLabel);
            var scoreBoard = FindViewById<TextView>(Resource.Id.scoreBoardLabel);
            scoreBoard.Text = score.ToString();


            // Display another quote when the "Next Quote" button is tapped
            var nextButton = FindViewById<Button>(Resource.Id.nextButton);
            nextButton.Click += delegate 
            {
                quoteCollection.GetNextQuote();
                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
                resultLabel.Text = "";
                enteredNameLabel.Text = "";
                quoteIndex++;
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

