using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace TwitterFeedApp
{
    public partial class MainWindow : Window
    {
        private readonly string bearerToken = "AAAAAAAAAAAAAAAAAAAAAKitxAEAAAAAkJGgrVb4nBuKEoyTH9EazYpcFLk%3DByDukyZycv7Xfq2CL8AUuPrJWmkuZpggiVAqsqZf9NLr2aRRGj"; // Remplacez par votre Bearer Token

        public MainWindow()
        {
            InitializeComponent();
            LoadTweets(1);
        }

        private async void LoadTweets(int tweetLimit = 5) // Limite par défaut à 5 tweets
        {
            var username = "elonmusk"; // Garder un seul utilisateur

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                try
                {
                    // Étape 1 : Obtenez l'ID de l'utilisateur
                    var userResponse = await client.GetAsync($"https://api.twitter.com/2/users/by/username/{username}");
                    userResponse.EnsureSuccessStatusCode();

                    var userContent = await userResponse.Content.ReadAsStringAsync();
                    var userId = ExtractUserId(userContent);

                    // Étape 2 : Récupérez les tweets avec une limite
                    var tweetsResponse = await client.GetAsync($"https://api.twitter.com/2/users/{userId}/tweets?max_results={tweetLimit}");
                    tweetsResponse.EnsureSuccessStatusCode();

                    var tweetsContent = await tweetsResponse.Content.ReadAsStringAsync();
                    var tweetResponse = JsonConvert.DeserializeObject<TweetResponse>(tweetsContent);

                    if (tweetResponse?.data != null)
                    {
                        foreach (var tweet in tweetResponse.data)
                        {
                            TweetsListBox.Items.Add($"@{username}: {tweet.text}");
                        }
                    }
                    else
                    {
                        TweetsListBox.Items.Add($"Aucun tweet trouvé pour @{username}.");
                    }
                }
                catch (Exception ex)
                {
                    TweetsListBox.Items.Add($"Erreur lors de la récupération des tweets de @{username}: {ex.Message}");
                }
            }
        }

        private string ExtractUserId(string json)
        {
            var userResponse = JsonConvert.DeserializeObject<UserResponse>(json);
            return userResponse?.data?.id;
        }
    }

    // Créez une classe pour désérialiser la réponse de l'utilisateur
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

    public class TweetData
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}