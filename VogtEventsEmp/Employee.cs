using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace VogtEventsEmp
{
    class Employee<T>
    {
        private DisplayEmployeeInformation displayInformation;

        // Ctor
        public Employee()
        {
            displayInformation = new DisplayEmployeeInformation();
            displayInformation.DisplayEventAddedMessage += DisplayInformation_DisplayEventAddedMessage;

        }

        private void DisplayInformation_DisplayEventAddedMessage()
        {
            Console.WriteLine("Employee added: ");

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

        // For datetime and int hiredate
        private T hireDate;

        public T HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }

        }

        // Method for events
        public void DisplayEmployeeInfo(string name, int number, int hiredate)
        {
            displayInformation.DisplayEmpInformation(name, number, hiredate);

        }
    }

    /// <summary>
    /// Methods, Delegates and Events for displaying employee information
    /// </summary>
    class DisplayEmployeeInformation
    {
        public delegate void DisplayAddedMessage();
        public event DisplayAddedMessage DisplayEventAddedMessage;

        public void DisplayEmpInformation(string name, int number, int hiredate)
        {
            // Added speech
            SpeechSynthesizer speaker = new SpeechSynthesizer();
            DisplayEventAddedMessage();

            Console.WriteLine($"The employee's name is {name}, number is {number} and hired {hiredate}");

            speaker.Speak($"The employee's name is {name}, number is {number} and hired {hiredate}");

            Console.WriteLine("");

        }
    }
}
