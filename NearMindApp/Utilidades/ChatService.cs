using Firebase.Database;
using Firebase.Database.Query;
using NearMindApp.Models;
using NearMindApp.Services;
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
        private bool isSubscribed = false;

        public ObservableCollection<Message> Messages { get; set; }

        public ChatService()
        {
            _firebaseClient = new FirebaseClient(FirebaseUrl);
            Messages = new ObservableCollection<Message>();
        }

        private string GenerateConversationKey(Guid emisorId, Guid receptorId)
        {
            string id1String = emisorId.ToString();
            string id2String = receptorId.ToString();

            return string.Compare(id1String, id2String, StringComparison.Ordinal) < 0
                ? $"{id1String}_{id2String}"
                : $"{id2String}_{id1String}";
        }

        public async Task SendMessageAsync(Guid emisorId, Guid receptorId, string text)
        {
            var usuario = await UsuarioService.Instance.ObtenerUsuarioPorId(emisorId);
            var message = new Message
            {
                id = Guid.NewGuid(),
                emisorId = emisorId,
                receptorId = receptorId,
                texto = text,
                fechaHora = DateTime.UtcNow,
                NombreEmisor = usuario.nombre
            };

            string conversationKey = GenerateConversationKey(emisorId, receptorId);

            await _firebaseClient
                .Child("messages")
                .Child(conversationKey)
                .PostAsync(message);
        }

        public async Task SubscribeToMessagesAsync(Guid emisorId, Guid receptorId, ObservableCollection<Message> messages)
        {

            if (isSubscribed) return; // Evita suscripciones duplicadas
            isSubscribed = true;

            string conversationKey = GenerateConversationKey(emisorId, receptorId);

            _firebaseClient
            .Child("messages")
            .Child(conversationKey)
            .AsObservable<Message>()
            .Subscribe(d =>
            {
                if (d.Object != null && !messages.Any(m => m.id == d.Object.id))
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

            return messages.Select(m => m.Object).OrderBy(m => m.fechaHora).ToList();
        }
    }
}
