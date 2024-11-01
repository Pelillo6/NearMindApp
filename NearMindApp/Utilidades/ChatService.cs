using Firebase.Database;
using Firebase.Database.Query;
using NearMindApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NearMindApp.Utilidades
{
    public class ChatService
    {
        private readonly FirebaseClient _firebaseClient;
        private const string FirebaseUrl = "https://nearmind-pin2024-default-rtdb.europe-west1.firebasedatabase.app/"; // URL de la Firebase Realtime Database

        public ObservableCollection<Message> Messages { get; set; }

        public ChatService()
        {
            _firebaseClient = new FirebaseClient(FirebaseUrl);
            Messages = new ObservableCollection<Message>();
        }

        public async Task SendMessageAsync(string user, string text)
        {
            var message = new Message
            {
                User = user,
                Text = text,
                Timestamp = DateTime.UtcNow
            };

            await _firebaseClient
                .Child("messages")
                .PostAsync(message);
        }

        public async Task SubscribeToMessagesAsync()
        {
            _firebaseClient
                .Child("messages")
                .AsObservable<Message>()
                .Subscribe(d =>
                {
                    if (d.Object != null)
                        Messages.Add(d.Object);
                });
        }
    }
}
