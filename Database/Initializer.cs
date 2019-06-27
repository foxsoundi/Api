using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace Database
{
    public class Initializer
    {
        public static void Initialize(FoxsoundiContext context)
        {
            context.Database.EnsureCreated();

            if (context.players.Any())
            {
                return;   // DB has been seeded
            }

            var users = new Player[]
            {
                new Player{FirstName = "toto", ID = 1, IsAdmin = false, IsConnected = false, LastName = "The"},
                new Player{FirstName = "toto2", ID = 2, IsAdmin = false, IsConnected = false, LastName = "The"},
                new Player{FirstName = "toto3", ID = 3, IsAdmin = false, IsConnected = false, LastName = "The"}
            };

            context.SaveChanges();
        }
    }
}
