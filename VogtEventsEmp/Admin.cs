using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace VogtEventsEmp
{

    class Admin
    {
        private DisplayAdminInformation displayAdminInformation;

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

        // Propfulls
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }

        }

        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }

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
            DateTime dateTime = new DateTime();

            // Added speech
            SpeechSynthesizer speaker = new SpeechSynthesizer();
            DisplayEventAddedMessage();

            Console.Clear();
            Console.WriteLine($"Your name is {name}, and your admin number is {number}. Your login time is at {DateTime.Now.ToLocalTime()}. If this is not correct, please try again.");

            speaker.Speak($"Your name is {name}, and your admin number is {number}. Your login time is at {DateTime.Now.ToLocalTime()}. If this is not correct, please try again.");

            Console.WriteLine("");

        }
    }
}
