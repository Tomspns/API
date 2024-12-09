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
            LoadTweets();
        }

        private async void LoadTweets()
        {
            var usernames = new List<string>
            {
                "elonmusk", "jack", "neiltyson", "BarackObama",
                "realDonaldTrump", "ladygaga", "BillGates",
                "justinbieber", "rihanna", "taylorswift13",
                "NASA"
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                foreach (var username in usernames)
                {
                    try
                    {
                        // Étape 1 : Obtenez l'ID de l'utilisateur
                        var userResponse = await client.GetAsync($"https://api.twitter.com/2/users/by/username/{username}");
                        userResponse.EnsureSuccessStatusCode();

                        var userContent = await userResponse.Content.ReadAsStringAsync();
                        // Extraire l'ID de l'utilisateur du JSON (à l'aide de Newtonsoft.Json ou d'un autre moyen)

                        // Exemple de désérialisation pour obtenir l'ID (ajoutez Newtonsoft.Json à votre projet)
                        var userId = ExtractUserId(userContent); // Implémentez cette méthode pour extraire l'ID

                        // Étape 2 : Récupérez les tweets
                        var tweetsResponse = await client.GetAsync($"https://api.twitter.com/2/users/{userId}/tweets");
                        tweetsResponse.EnsureSuccessStatusCode();

                        var tweetsContent = await tweetsResponse.Content.ReadAsStringAsync();
                        // Traitez les tweets ici (désérialisez et affichez-les dans la ListBox)

                        TweetsListBox.Items.Add($"Tweets de @{username}: {tweetsContent}"); // Remplacez ceci par un traitement approprié
                    }
                    catch (Exception ex)
                    {
                        TweetsListBox.Items.Add($"Erreur lors de la récupération des tweets de @{username}: {ex.Message}");
                    }
                }
            }
        }

        private string ExtractUserId(string json)
        {
            // Implémentez la logique pour désérialiser le JSON et extraire l'ID
            // Par exemple, en utilisant Newtonsoft.Json
            // var user = JsonConvert.DeserializeObject<UserResponse>(json);
            // return user.data.id;

            throw new NotImplementedException("Implémentez la méthode pour extraire l'ID de l'utilisateur.");
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
}