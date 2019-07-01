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

            if (context.Players.Any())
            {
                return;   // DB has been seeded
            }

            var users = new Player[]
            {
                new Player{Email = "yoyo@gmail.com", Password = "******", FirstName = "Yohan", LastName = "Fairfort", Preferences = new Preference{Theme = Theme.Dark}},
                new Player{Email = "rahma@gmail.com", Password = "******", FirstName = "Rahma", LastName = "Bourahoui", Preferences = new Preference{Theme = Theme.Dark}},
                new Player{Email = "loghan@gmail.com", Password = "******", FirstName = "Loghan", LastName = "Ramassami", Preferences = new Preference{Theme = Theme.Dark}}
            };
            context.Players.AddRange(users);
            context.SaveChanges();
        }
    }
}
