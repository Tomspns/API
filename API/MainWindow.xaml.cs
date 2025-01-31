﻿using System;
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
        };

        public MainWindow()
        {
            InitializeComponent();
            LoadTweets(5); // Charger 5 tweets par utilisateur
        }

        private async void LoadTweets(int tweetLimit)
        {
            TweetsListBox.Items.Clear(); // Vider la ListBox avant de charger de nouveaux tweets

            foreach (var username in usernames)
            {
                await LoadTweetsForUser(username, tweetLimit);
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            TweetsListBox.Items.Clear(); // Vider la ListBox avant de charger de nouveaux tweets
            string username = UsernameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                TweetsListBox.Items.Add("Veuillez entrer un nom d'utilisateur.");
                return;
            }

            await LoadTweetsForUser(username, 5); // Charger 5 tweets pour l'utilisateur spécifié
        }

        private async Task LoadTweetsForUser(string username, int tweetLimit)
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
                        return; // Sortir si l'utilisateur n'est pas trouvé
                    }

                    var userContent = await userResponse.Content.ReadAsStringAsync();
                    var userId = ExtractUserId(userContent);

                    if (string.IsNullOrEmpty(userId))
                    {
                        TweetsListBox.Items.Add($"Erreur : ID d'utilisateur non trouvé pour @{username}.");
                        return; // Sortir si l'ID d'utilisateur n'est pas trouvé
                    }

                    var tweetsResponse = await client.GetAsync($"https://api.twitter.com/2/users/{userId}/tweets?max_results={tweetLimit}");
                    if (!tweetsResponse.IsSuccessStatusCode)
                    {
                        var errorContent = await tweetsResponse.Content.ReadAsStringAsync();
                        TweetsListBox.Items.Add($"Erreur lors de la récupération des tweets pour @{username}: {tweetsResponse.StatusCode} - {errorContent}");
                        return; // Sortir si les tweets ne peuvent pas être récupérés
                    }

                    var tweetsContent = await tweetsResponse.Content.ReadAsStringAsync();
                    var tweetResponse = JsonConvert.DeserializeObject<TweetResponse>(tweetsContent);

                    if (tweetResponse?.data != null)
                    {
                        foreach (var tweet in tweetResponse.data)
                        {
                            if (!string.IsNullOrEmpty(tweet.created_at))
                            {
                                // Convertir la date de publication
                                var tweetDate = DateTime.Parse(tweet.created_at);
                                TweetsListBox.Items.Add($"{tweet.text} \nPosté par @{username} le {tweetDate:g}");
                            }
                            else
                            {
                                TweetsListBox.Items.Add($"{tweet.text} \nPosté par @{username} (date inconnue)");
                            }
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

        private string ExtractUserId(string json)
        {
            var userResponse = JsonConvert.DeserializeObject<UserResponse>(json);
            return userResponse?.data?.id;
        }

        // Gestion des événements pour le placeholder
        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = Visibility.Collapsed; // Cacher le placeholder
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                PlaceholderTextBlock.Visibility = Visibility.Visible; // Afficher le placeholder si le TextBox est vide
            }
        }
    }
}