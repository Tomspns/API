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

namespace API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Auth.SetUserCredentials("qsc5SXJkZeEK8fbL7Iqjk6ACk", "fDSMeRCANfL7Gio3k3NiOqEnksgl4pBvPKJpwqcTrsOS1m81Sw", "1860986408858435584-D0lapfTYWU5ecw7eyiSzIXUZ7PN3gR", "nmyPIMS278ciFAbdiiuBTPS9yYv8xM7Hg7fCWjahNJYFu");

            // Récupère les 10 derniers tweets de l'utilisateur spécifié
            var userTweets = User.GetUserFromScreenName("nomUtilisateur").Timeline.GetHomeTimeline(10);

            // Affiche les textes des tweets dans la ListBox
            foreach (var tweet in userTweets)
            {
                tweetListBox.Items.Add(tweet.FullText);
            }
        }
    }
}