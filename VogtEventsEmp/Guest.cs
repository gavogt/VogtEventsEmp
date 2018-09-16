using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VogtEventsEmp
{
    class Guest
    {
        /// <summary>
        ///  Propfulls
        /// </summary>
        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }

        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }

        }
    }
}
