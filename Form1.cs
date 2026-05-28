using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberSafetyBotGUI
{
    public partial class Form1 : Form
    {
        private RichTextBox chatDisplay;
        private TextBox inputTextBox;
        private Button sendButton;
        private Label titleLabel;
        private Chatbot chatbot;
        private bool waitingForName = true;

        public Form1()
        {
            // Make the window
            this.Text = "Cybersecurity Awareness Bot";
            this.Size = new Size(850, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 35);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Title at the top
            titleLabel = new Label();
            titleLabel.Text = "  SHIELD - CYBERSECURITY AWARENESS  ";
            titleLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(0, 183, 255);
            titleLabel.BackColor = Color.FromArgb(45, 45, 50);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Height = 55;
            titleLabel.Padding = new Padding(10, 0, 10, 0);

            // Chat area where messages appear
            chatDisplay = new RichTextBox();
            chatDisplay.Location = new Point(15, 75);
            chatDisplay.Size = new Size(805, 470);
            chatDisplay.ReadOnly = true;
            chatDisplay.BackColor = Color.FromArgb(40, 40, 45);
            chatDisplay.ForeColor = Color.White;
            chatDisplay.Font = new Font("Segoe UI", 11);
            chatDisplay.BorderStyle = BorderStyle.None;

            // Text box for user to type
            inputTextBox = new TextBox();
            inputTextBox.Location = new Point(15, 555);
            inputTextBox.Size = new Size(690, 35);
            inputTextBox.Font = new Font("Segoe UI", 12);
            inputTextBox.BackColor = Color.FromArgb(60, 60, 65);
            inputTextBox.ForeColor = Color.White;
            inputTextBox.BorderStyle = BorderStyle.FixedSingle;
            inputTextBox.KeyPress += InputTextBox_KeyPress;
            inputTextBox.PlaceholderText = "  Type here... ask about passwords, phishing, safe browsing";

            // Send button
            sendButton = new Button();
            sendButton.Location = new Point(715, 553);
            sendButton.Size = new Size(105, 40);
            sendButton.Text = "SEND";
            sendButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            sendButton.BackColor = Color.FromArgb(0, 120, 215);
            sendButton.ForeColor = Color.White;
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.FlatAppearance.BorderSize = 0;
            sendButton.Cursor = Cursors.Hand;
            sendButton.Click += SendButton_Click;

            // Add everything to the window
            this.Controls.Add(titleLabel);
            this.Controls.Add(chatDisplay);
            this.Controls.Add(inputTextBox);
            this.Controls.Add(sendButton);

            // Start the chatbot
            chatbot = new Chatbot();
            chatbot.BotMessage += AppendMessage;
            chatbot.Start();
        }

        private void InputTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendButton_Click(sender, e);
                e.Handled = true;
            }
        }

        private void SendButton_Click(object? sender, EventArgs e)
        {
            string userInput = inputTextBox.Text.Trim();

            if (string.IsNullOrEmpty(userInput))
                return;

            // Show what the user typed
            AppendMessage($"You: {userInput}");

            // Clear the text box
            inputTextBox.Clear();
            inputTextBox.Focus();

            // Check if we're still waiting for their name
            if (waitingForName)
            {
                waitingForName = false;
                chatbot.SetUserName(userInput);
            }
            else
            {
                chatbot.ProcessInput(userInput);
            }

            // Scroll to bottom of chat
            chatDisplay.ScrollToCaret();
        }

        private void AppendMessage(string message)
        {
            if (chatDisplay.InvokeRequired)
            {
                chatDisplay.Invoke(new Action(() => AppendMessage(message)));
                return;
            }

            // Make bot messages look different from user messages
            if (message.StartsWith("Bot:"))
            {
                chatDisplay.SelectionColor = Color.FromArgb(0, 183, 255);
                chatDisplay.SelectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
                chatDisplay.AppendText("BOT > ");
                chatDisplay.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
                chatDisplay.SelectionColor = Color.White;
                chatDisplay.AppendText(message.Substring(5) + "\n\n");
            }
            else if (message.StartsWith("You:"))
            {
                chatDisplay.SelectionColor = Color.FromArgb(100, 200, 100);
                chatDisplay.SelectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
                chatDisplay.AppendText("YOU > ");
                chatDisplay.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
                chatDisplay.SelectionColor = Color.White;
                chatDisplay.AppendText(message.Substring(5) + "\n\n");
            }
            else
            {
                chatDisplay.SelectionColor = Color.Gray;
                chatDisplay.AppendText(message + "\n");
            }

            chatDisplay.ScrollToCaret();
        }
    }
}