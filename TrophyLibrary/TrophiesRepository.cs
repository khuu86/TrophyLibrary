using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TrophyLibrary
{
    public class TrophiesRepository
    {
        private int nextId = 1;
        private List<Trophy> _trophies = new List<Trophy>();

        public TrophiesRepository()
        {
            _trophies.Add(new Trophy(1, "Tour de France", 2021));
            _trophies.Add(new Trophy(2, "Giro d'Italia", 2021));
            _trophies.Add(new Trophy(3, "Vuelta a Espana", 2021));
            _trophies.Add(new Trophy(4, "Danmark Rundt", 2020));
            _trophies.Add(new Trophy(5, "Schweiz Rundt", 2020));
        }

        public IEnumerable<Trophy> Get(int? year = null, string name = null, string sortBy = null)
        {

            // Copy Constructor
            List<Trophy> result = new List<Trophy>(_trophies);

            // Filtering på år
            if (name != null)
            {
                return _trophies.FindAll(t => t.Competition == name);
            }
            if (year != null)
            {
                return _trophies.FindAll(t => t.Year == year);
            }
            if (sortBy != null)
            {
                switch (sortBy.ToLower())
                {
                    case "competition":
                        result.Sort((t1, t2) => t1.Competition.CompareTo(t2.Competition));
                        break;
                    case "competitiondesc":
                        result.Sort((t1, t2) => t2.Competition.CompareTo(t1.Competition));
                        break;
                    case "year":
                        result.Sort((t1, t2) => t1.Year.CompareTo(t2.Year));
                        break;
                    case "yeardesc":
                        result.Sort((t1, t2) => t2.Year.CompareTo(t1.Year));
                        break;
                    default:
                        throw new ArgumentException($"Unknown sort by value: {sortBy}");
                }
            }
            return result;
        }

        public Trophy? GetById(int id)
        {
            return _trophies.FirstOrDefault(t => t.Id == id);
        }
        public Trophy Add(Trophy trophy)
        {
            trophy.Validate();
            trophy.Id = nextId++;
            _trophies.Add(trophy);
            return trophy;
        }

        public Trophy? Remove(int id)
        {
            Trophy? trophy = GetById(id);
            if (trophy == null)
            {
                return null;
            }
            _trophies.Remove(trophy);
            return trophy;
        }

        public Trophy? Update(int id, Trophy trophy)
        {
            trophy.Validate();
            Trophy? existingTrophy = GetById(id);
            if (existingTrophy == null)
            {
                return null;
            }
            existingTrophy.Competition = trophy.Competition;
            existingTrophy.Year = trophy.Year;
            return existingTrophy;
        }

    }
}
