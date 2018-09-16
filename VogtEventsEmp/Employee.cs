using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace VogtEventsEmp
{
    #region Employee
    /// <summary>
    /// Employee Class
    /// </summary>
    /// <typeparam name="T">For datetime or int</typeparam>
    class Employee<T>
    {
        private DisplayInformation displayInformation;

        // Ctor
        public Employee()
        {
            displayInformation = new DisplayInformation();
            displayInformation.DisplayEventAddedMessage += DisplayInformation_DisplayEventAddedMessage;

        }

        private void DisplayInformation_DisplayEventAddedMessage()
        {
            Console.WriteLine("\nEmployee added: ");

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
        public void DisplayEmployeeInfo(string name, int number, DateTime hiredate)
        {
            displayInformation.DisplayEmpInformation(name, number, hiredate);

        }
    }
    #endregion

    #region DisplayEmployeeInformation
    /// <summary>
    /// Methods, Delegates and Events for displaying employee information
    /// </summary>
    class DisplayInformation
    {
        public delegate void DisplayAddedMessage();
        public event DisplayAddedMessage DisplayEventAddedMessage;

        public void DisplayEmpInformation(string name, int number, DateTime hiredate)
        {
            // Added speech
            SpeechSynthesizer speaker = new SpeechSynthesizer();
            DisplayEventAddedMessage();

            Console.Clear();
            Console.WriteLine($"The employee's name is {name}, number is {number} and hired {hiredate.ToShortDateString()}");

            speaker.Speak($"The employee's name is {name}, number is {number} and hired {hiredate.ToShortDateString()}");

            Console.WriteLine("");

        }
    }
    #endregion

}
