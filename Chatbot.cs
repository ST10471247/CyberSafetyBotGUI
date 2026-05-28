using System;

namespace CyberSafetyBotGUI
{
    public class Chatbot
    {
        private AudioService audio;
        private ResponseHandler responder;
        private string userName = "";

        public event Action<string>? BotMessage;

        public Chatbot()
        {
            audio = new AudioService();
            responder = new ResponseHandler();
        }

        public void Start()
        {
            audio.PlayGreeting();
            SendMessage("🛡️ Welcome to the Cybersecurity Awareness Bot!");
            SendMessage("I'm here to help you stay safe online.");
            SendMessage("What is your name?");
        }

        public void SetUserName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                userName = name;
                SendMessage($"Hello {userName}! Nice to meet you.");
                SendMessage("You can ask me about passwords, phishing, safe browsing, or links.");
                SendMessage("Type 'help' to see all topics.");
            }
        }

        public void ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                SendMessage("Please type something!");
                return;
            }

            if (input.ToLower() == "exit" || input.ToLower() == "bye")
            {
                SendMessage($"Goodbye {userName}! Stay safe online! 🔒");
                return;
            }

            string response = responder.GetResponse(input, userName);
            SendMessage(response);
        }

        private void SendMessage(string message)
        {
            BotMessage?.Invoke($"Bot: {message}");
        }
    }
}