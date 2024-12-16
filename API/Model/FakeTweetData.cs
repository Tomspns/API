using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeedApp.Model
{
    public class FakeTweetData
    {

        public static List<TweetData> GetFakeTweets()
        {
            return new List<TweetData>
        {
            new TweetData
            {
                id = "1",
                text = "C'est une belle journée pour coder !",
                Username = "@dev123",
                TweetText = "C'est une belle journée pour coder !",
                Timestamp = "2024-12-15 14:30"
            },
            new TweetData
            {
                id = "2",
                text = "Excité pour le lancement de notre nouveau produit !",
                Username = "@startup_guy",
                TweetText = "Excité pour le lancement de notre nouveau produit !",
                Timestamp = "2024-12-15 15:00"
            },
            new TweetData
            {
                id = "3",
                text = "Le café est prêt !",
                Username = "@coffee_love",
                TweetText = "Le café est prêt !",
                Timestamp = "2024-12-15 15:15"
            },
            new TweetData
            {
                id = "4",
                text = "Quelqu'un a dit qu'il allait pleuvoir aujourd'hui ?",
                Username = "@weather_enthusiast",
                TweetText = "Quelqu'un a dit qu'il allait pleuvoir aujourd'hui ?",
                Timestamp = "2024-12-15 16:00"
            },
            new TweetData
            {
                id = "5",
                text = "Terminé un super livre aujourd'hui !",
                Username = "@bookworm",
                TweetText = "Terminé un super livre aujourd'hui !",
                Timestamp = "2024-12-15 16:30"
            },
            new TweetData
            {
                id = "6",
                text = "La technologie avance à grands pas.",
                Username = "@tech_trend",
                TweetText = "La technologie avance à grands pas.",
                Timestamp = "2024-12-15 17:00"
            },
            new TweetData
            {
                id = "7",
                text = "N'oubliez pas de prendre soin de vous !",
                Username = "@self_care",
                TweetText = "N'oubliez pas de prendre soin de vous !",
                Timestamp = "2024-12-15 17:30"
            },
            new TweetData
            {
                id = "8",
                text = "La pizza est la meilleure nourriture !",
                Username = "@foodie",
                TweetText = "La pizza est la meilleure nourriture !",
                Timestamp = "2024-12-15 18:00"
            },
            new TweetData
            {
                id = "9",
                text = "Prêt pour le week-end !",
                Username = "@weekend_vibes",
                TweetText = "Prêt pour le week-end !",
                Timestamp = "2024-12-15 18:30"
            },
            new TweetData
            {
                id = "10",
                text = "Rappel : restez positif !",
                Username = "@positivity",
                TweetText = "Rappel : restez positif !",
                Timestamp = "2024-12-15 19:00"
            }
        };
        }
    }
}
