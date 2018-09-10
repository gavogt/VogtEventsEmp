using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VogtEventsEmp
{
    class User
    {
        // Ctor
        public User()
        {

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
    }
}
