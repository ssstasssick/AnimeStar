using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface Intermediate<T, A, S> where T: class where A : class where S : class
    {
        IEnumerable<A> FindAnime(int Id);
        IEnumerable<S> Find(int animeId);
        void Create(int animeId, int Id);
        void Delete(int animeId, int Id);
    }
}
