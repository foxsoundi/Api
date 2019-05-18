using System.Collections.Generic;

namespace Api
{
    public class InMemoryDatabase : IDatabase
    {
        public List<Genre> GetGenres()
        {
            return new List<Genre>{
                new Genre { Name = "Rock" },
                new Genre { Name = "Indie"},
                new Genre { Name = "Electronic"}
            };
        }
    }
}