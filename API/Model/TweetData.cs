﻿using System;
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
}