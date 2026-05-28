using System;
using System.Collections.Generic;

namespace CyberSafetyBotGUI
{
    public class ResponseHandler
    {
        private Dictionary<string, string> responses;
        private Dictionary<string, string> topics;

        public ResponseHandler()
        {
            responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            topics = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            SetupResponses();
        }

        private void SetupResponses()
        {
            responses.Add("how are you", "I'm functioning well, thanks for asking! Ready to help you learn about cybersecurity.");
            responses.Add("what is your purpose", "My purpose is to educate people about online threats like phishing, malware, and social engineering scams.");
            responses.Add("what can i ask", "You can ask me about passwords, phishing, safe browsing, or suspicious links.");
            responses.Add("help", "I can teach you about:\n- Creating strong passwords\n- Spotting phishing emails\n- Safe internet browsing\n- Recognizing fake links\n\nJust type any of these topics!");
            responses.Add("hello", "Hi there! Want to learn about staying safe online?");
            responses.Add("hi", "Hello! I'm your cybersecurity guide. Ask me anything about online safety.");
            responses.Add("thanks", "You're welcome! Stay safe out there.");
            responses.Add("thank you", "Happy to help! Remember to always be careful online.");

            topics.Add("password", @"=== PASSWORD SAFETY TIPS ===

1. Use at least 12 characters in your passwords
2. Mix uppercase letters, lowercase letters, numbers, and symbols
3. Never reuse passwords across different websites
4. Use a password manager to keep track of everything
5. Turn on two-factor authentication whenever possible

Bad passwords to avoid: password123, qwerty, your name, or 12345678");

            topics.Add("phishing", @"=== SPOTTING PHISHING SCAMS ===

Warning signs to look for:
• Emails asking you to 'verify your account immediately'
• Bad spelling and grammar mistakes
• Sender email addresses that look slightly wrong
• Links that don't match where they say they go
• Threats or urgent deadlines trying to scare you

If something seems suspicious, don't click anything!");

            topics.Add("safe browsing", @"=== SAFE BROWSING PRACTICES ===

• Look for 'https://' and the padlock icon
• Don't download files from untrusted websites
• Keep your browser updated
• Clear your browsing history regularly");

            topics.Add("link", @"=== CHECKING SUSPICIOUS LINKS ===

Before clicking any link:
• Hover over it to see the real address
• Look for misspellings
• When in doubt, type the address yourself");
        }

        public string GetResponse(string input, string userName)
        {
            foreach (var response in responses)
            {
                if (input.ToLower().Contains(response.Key.ToLower()))
                {
                    return response.Value;
                }
            }

            foreach (var topic in topics)
            {
                if (input.ToLower().Contains(topic.Key.ToLower()))
                {
                    return topic.Value;
                }
            }

            return "I'm not sure I understand. Try asking about passwords, phishing, safe browsing, or links. Type 'help' for options.";
        }
    }
}