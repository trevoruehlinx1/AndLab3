﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Lab3;

namespace SavingActivityState
{
    [Activity(Label = "Lab3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        QuoteBank quoteCollection;
        TextView quotationTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Create the quote collection and display the current quote
            quoteCollection = new QuoteBank();
            quoteCollection.LoadQuotes();
            quoteCollection.GetNextQuote();

            quotationTextView = FindViewById<TextView>(Resource.Id.quoteTextView);
            quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;


            // Display another quote when the "Next Quote" button is tapped
            var nextButton = FindViewById<Button>(Resource.Id.nextButton);
            nextButton.Click += delegate {
                quoteCollection.GetNextQuote();
                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
            };
        }
    }
}

