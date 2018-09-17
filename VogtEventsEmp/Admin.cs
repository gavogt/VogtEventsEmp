using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace VogtEventsEmp
{
    class Admin : Employee<DateTime>
    {
        private DisplayAdminInformation displayAdminInformation;

        // Propfull
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        // Ctor
        public Admin()
        {
            displayAdminInformation = new DisplayAdminInformation();
            displayAdminInformation.DisplayEventAddedMessage += DisplayAdminInformation_DisplayEventAddedMessage;

        }

        private void DisplayAdminInformation_DisplayEventAddedMessage()
        {
            Console.WriteLine("Your admin name has been added!");

        }

        // Method for events
        public void DisplayAdminInfo(string name, int number)
        {
            displayAdminInformation.DisplayAdminInfo(name, number);

        }

    }

    class DisplayAdminInformation
    {
        public delegate void DisplayAddedMessage();
        public event DisplayAddedMessage DisplayEventAddedMessage;

        public void DisplayAdminInfo(string name, int number)
        {
            // Added speech
            SpeechSynthesizer speaker = new SpeechSynthesizer();
            DisplayEventAddedMessage();

            Console.Clear();
            Console.WriteLine($"Your name is {name}, and your admin number is {number}. Your login time is at {DateTime.Now.ToLocalTime()}. If this is not correct, please contact the appropriate team.");

            speaker.Speak($"Your name is {name}, and your admin number is {number}. Your login time is at {DateTime.Now.ToLocalTime()}. If this is not correct, please contact the appropriate team.");

            Console.Clear();

            Console.WriteLine("");

        }
    }
}
