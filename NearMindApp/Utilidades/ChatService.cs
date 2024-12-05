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

        private string GenerateConversationKey(Guid emisorId, Guid receptorId)
        {
            // Ordena los IDs alfabéticamente para que la clave sea consistente
            var ids = new List<Guid> { emisorId, receptorId };
            ids.Sort();
            return string.Join("_", ids);
        }

        public async Task SendMessageAsync(Guid emisorId, Guid receptorId, string text)
        {
            var message = new Message
            {
                emisor_id = emisorId,
                receptor_id = receptorId,
                texto = text,
                fecha_hora = DateTime.UtcNow
            };

            string conversationKey = GenerateConversationKey(emisorId, receptorId);

            await _firebaseClient
                .Child("messages")
                .Child(conversationKey)
                .PostAsync(message);
        }

        public async Task SubscribeToMessagesAsync(Guid emisorId, Guid receptorId, ObservableCollection<Message> messages)
        {
            string conversationKey = GenerateConversationKey(emisorId, receptorId);

            _firebaseClient
            .Child("messages")
            .Child(conversationKey)
            .AsObservable<Message>()
            .Subscribe(d =>
            {
                if (d.Object != null)
                    messages.Add(d.Object);
            });
        }

        public async Task<List<Message>> ObtenerMensajesAsync(Guid emisorId, Guid receptorId)
        {
            string conversationKey = GenerateConversationKey(emisorId, receptorId);

            var messages = await _firebaseClient
            .Child("messages")
            .Child(conversationKey)
            .OrderByKey()
            .OnceAsync<Message>();

            return messages.Select(m => m.Object).OrderBy(m => m.fecha_hora).ToList();
        }
    }
}
