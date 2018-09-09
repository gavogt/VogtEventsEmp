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

        public void DisplayEmployeeInfo(string name, int number, DateTime hiredate)
        {
            DisplayInformation.DisplayEmpInformation(name, number, hiredate);
        }
    }

    class DisplayEmployeeInformation
    {
        public void DisplayEmpInformation(string name, int number, DateTime hiredate)
        {
            Console.WriteLine($"The employee's name is {name}, number is {number} and hired {hiredate}");
        }

    }
}
