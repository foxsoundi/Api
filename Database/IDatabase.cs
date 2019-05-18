using System.Collections.Generic;

namespace Api
{
    internal interface IDatabase
    {
        List<Genre> GetGenres();
    }
}