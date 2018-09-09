using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VogtEventsEmp
{
    class Employee<T>
    {
        private DisplayEmployeeInformation DisplayInformation;

        // Ctor
        public Employee()
        {
            // Subscribe
            DisplayInformation.DisplayEventAddedMessage += DisplayInformation_DisplayEventAddedMessage;
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

        private T hireDate;

        public T HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }

        // Method for events
        public void DisplayEmployeeInfo(string name, int number, int hiredate)
        {
            DisplayInformation.DisplayEmpInformation(name, number, hiredate);
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
            DisplayEventAddedMessage();
            Console.WriteLine($"The employee's name is {name}, number is {number} and hired {hiredate}");

        }
    }
}
