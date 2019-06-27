using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsConnected { get; set; }
        public string SessionToken { get; set; }
        public List<Player> Friend { get; set; }
    }
}
