using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMusify
{
    public class Admin : User
    {
        public Admin(string name, string username, string password) : base(name, username, password)
        {

        }
    }
}
