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
using System.Net.Http;
using Newtonsoft.Json;
using Tweetinvi;
using System.Windows.Media.Animation;
using Tweetinvi.Models;
using Tweetinvi.Core.Models;
using TweetSharp;

namespace TwitterFeedApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadTweets();
        }

        private void LoadTweets()
        {
            var consumerKey = "qsc5SXJkZeEK8fbL7Iqjk6ACk";
            var consumerSecret = "fDSMeRCANfL7Gio3k3NiOqEnksgl4pBvPKJpwqcTrsOS1m81Sw";
            var accessToken = "1860986408858435584-D0lapfTYWU5ecw7eyiSzIXUZ7PN3gR";
            var accessTokenSecret = "nmyPIMS278ciFAbdiiuBTPS9yYv8xM7Hg7fCWjahNJYFu";

            var service = new TwitterService(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            var usernames = new List<string>
    {
        "elonmusk", "jack", "neiltyson", "BarackObama",
        "realDonaldTrump", "ladygaga", "BillGates",
        "justinbieber", "rihanna", "taylorswift13"
    };

            foreach (var username in usernames)
            {
                try
                {
                    var tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions
                    {
                        ScreenName = username,
                        Count = 5
                    });

                    // Vérifiez si tweets n'est pas null et a des éléments
                    if (tweets == null || !tweets.Any())
                    {
                        TweetsListBox.Items.Add($"Aucun tweet trouvé pour @{username}.");
                        continue;
                    }

                    foreach (var tweet in tweets)
                    {
                        if (tweet != null)
                        {
                            TweetsListBox.Items.Add($"@{username}: {tweet.Text} (Publié le: {tweet.CreatedDate})");
                        }
                    }
                }
                catch (Exception ex)
                {
                    TweetsListBox.Items.Add($"Erreur lors de la récupération des tweets de @{username}: {ex.Message}");
                }
            }
        }
    }
}