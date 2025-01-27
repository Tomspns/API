using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeedApp.Model
{
    public class TweetData 
    {
        public string id { get; set; } // ID du tweet
        public string text { get; set; } // Texte du tweet
        public string Username { get; set; } // Nom d'utilisateur
        public string TweetText { get; set; } // Texte du tweet
        public string Timestamp { get; set; } // Timestamp
        public string created_at { get; set; }
    }

    public class UserResponse
    {
        public UserData data { get; set; }
    }

    public class UserData
    {
        public string id { get; set; }
        public string username { get; set; }
    }

    public class TweetResponse
    {
        public List<TweetData> data { get; set; }
    }

    public class FakeData //fausse données
    {
        public static List<TweetData> GetFakeTweets()
        {
            return new List<TweetData>
            {
                new TweetData
                {
                    id = "1",
                    text = "Ceci est un tweet de test.",
                    Username = "Utilisateur1",
                    TweetText = "Ceci est un tweet de test.",
                    Timestamp = DateTime.Now.ToString("o"),
                    created_at = DateTime.Now.ToString("o")
                },
                new TweetData
                {
                    id = "2",
                    text = "Un autre tweet pour simuler des données.",
                    Username = "Utilisateur2",
                    TweetText = "Un autre tweet pour simuler des données.",
                    Timestamp = DateTime.Now.AddMinutes(-1).ToString("o"),
                    created_at = DateTime.Now.AddMinutes(-1).ToString("o")
                },
                new TweetData
                {
                    id = "3",
                    text = "Les tests sont importants pour le développement.",
                    Username = "Utilisateur3",
                    TweetText = "Les tests sont importants pour le développement.",
                    Timestamp = DateTime.Now.AddMinutes(-2).ToString("o"),
                    created_at = DateTime.Now.AddMinutes(-2).ToString("o")
                },
                new TweetData
                {
                    id = "4",
                    text = "Simulons encore un tweet.",
                    Username = "Utilisateur4",
                    TweetText = "Simulons encore un tweet.",
                    Timestamp = DateTime.Now.AddMinutes(-3).ToString("o"),
                    created_at = DateTime.Now.AddMinutes(-3).ToString("o")
                },
                new TweetData
                {
                    id = "5",
                    text = "Dernier tweet de test.",
                    Username = "Utilisateur5",
                    TweetText = "Dernier tweet de test.",
                    Timestamp = DateTime.Now.AddMinutes(-4).ToString("o"),
                    created_at = DateTime.Now.AddMinutes(-4).ToString("o")
                }
            };
        }
    }
}
