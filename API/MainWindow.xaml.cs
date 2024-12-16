using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using TwitterFeedApp.Model;

namespace TwitterFeedApp
{
    public partial class MainWindow : Window
    {
        private readonly string bearerToken = "AAAAAAAAAAAAAAAAAAAAAKitxAEAAAAAkJGgrVb4nBuKEoyTH9EazYpcFLk%3DByDukyZycv7Xfq2CL8AUuPrJWmkuZpggiVAqsqZf9NLr2aRRGj"; // Remplacez par votre Bearer Token

        private readonly List<TweetData> ListTwitdata = new List<TweetData>();

        private readonly List<string> usernames = new List<string>
        {
            "elonmusk",
            "jack",
            "barackobama",
            "nasa"
        };

        public MainWindow()
        {
            InitializeComponent();
            _: LoadTweets(5); // Charger 5 tweets par utilisateur

          //  ListTwitdata = FakeTweetData.GetFakeTweets();
        }

        private async void LoadTweets(int tweetLimit)
        {
            TweetsListBox.Items.Clear(); // Vider la ListBox avant de charger de nouveaux tweets

            foreach (var username in usernames)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                    try
                    {
                        var userResponse = await client.GetAsync($"https://api.twitter.com/2/users/by/username/{username}");
                        if (!userResponse.IsSuccessStatusCode)
                        {
                            var errorContent = await userResponse.Content.ReadAsStringAsync();
                            TweetsListBox.Items.Add($"Erreur lors de la récupération de l'utilisateur {username}: {userResponse.StatusCode} - {errorContent}");
                            continue; // Passer à l'utilisateur suivant
                        }

                        var userContent = await userResponse.Content.ReadAsStringAsync();
                        var userId = ExtractUserId(userContent);

                        if (string.IsNullOrEmpty(userId))
                        {
                            TweetsListBox.Items.Add($"Erreur : ID d'utilisateur non trouvé pour @{username}.");
                            continue; // Passer à l'utilisateur suivant
                        }

                        var tweetsResponse = await client.GetAsync($"https://api.twitter.com/2/users/{userId}/tweets?max_results={tweetLimit}");
                        if (!tweetsResponse.IsSuccessStatusCode)
                        {
                            var errorContent = await tweetsResponse.Content.ReadAsStringAsync();
                            TweetsListBox.Items.Add($"Erreur lors de la récupération des tweets pour @{username}: {tweetsResponse.StatusCode} - {errorContent}");
                            continue; // Passer à l'utilisateur suivant
                        }

                        var tweetsContent = await tweetsResponse.Content.ReadAsStringAsync();
                        var tweetResponse = JsonConvert.DeserializeObject<TweetResponse>(tweetsContent);

                        if (tweetResponse?.data != null)
                        {
                            foreach (var tweet in tweetResponse.data)
                            {
                                TweetsListBox.Items.Add($"{tweet.text} \nPosté par @{username} le {DateTime.Now:g}");
                            }
                        }
                        else
                        {
                            TweetsListBox.Items.Add($"Aucun tweet trouvé pour @{username}.");
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        TweetsListBox.Items.Add($"Erreur de requête pour @{username}: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        TweetsListBox.Items.Add($"Erreur générale pour @{username}: {ex.Message}");
                    }
                }
            }
        }

        private string ExtractUserId(string json)
        {
            var userResponse = JsonConvert.DeserializeObject<UserResponse>(json);
            return userResponse?.data?.id;
        }
    }
}