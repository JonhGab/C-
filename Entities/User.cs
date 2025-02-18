using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOActivity.Entities
{
    class User
    {
        public string Name { get; set; }
        public int CPF { get; set; }

        public User(string name, int id)
        {
            Name = name;
            CPF = id;
        }
        public User()
        {
        }   
    }
}
