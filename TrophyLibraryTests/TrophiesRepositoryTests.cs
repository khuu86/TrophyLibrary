using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrophyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace TrophyLibrary.Tests
{
    [TestClass()]
    public class TrophiesRepositoryTests
    {
        [TestMethod()]
        public void GetTest()
        {
            // Opretter repository
            TrophiesRepository repository = new TrophiesRepository();

            // Act: Henter trofæer fra TrophiesRepository
            repository.Get();

            // Assert: Tjekker hvor mange trofæer der er i listen (5 fra TrophiesRepository)
            Assert.AreEqual(5, repository.Get().Count());
        }

        [TestMethod()]
        public void GetFilterTest()
        {
            // Opretter repository
            TrophiesRepository repository = new TrophiesRepository();

            // Act: Filtrerer trofæer efter competition og år
            IEnumerable<Trophy> competitionResult = repository.Get(name: "Tour de France");
            IEnumerable<Trophy> yearResult = repository.Get(year: 2021);

            // Assert: Kontrollerer korrekt filtrering af trofæer baseret på competition og år
            Assert.AreEqual(1, competitionResult.Count());
            Assert.AreEqual(3, yearResult.Count());
        }

        [TestMethod()]
        public void GetSortByCompetitionTest()
        {
            // Opretter repository
            TrophiesRepository repository = new TrophiesRepository();

            // Act: Sorterer trofæer efter Competition
            var sortByCompetition = repository.Get(sortBy: "competition").ToList();

            // Assert: Kontrollerer at listen er sorteret alfabetisk efter Competition
            Assert.AreEqual("Danmark Rundt", sortByCompetition[0].Competition);
            Assert.AreEqual("Vuelta a Espana", sortByCompetition[4].Competition);

            // Act: Sorterer trofæer efter Competition descending
            var sortByCompetitionDesc = repository.Get(sortBy: "competitiondesc").ToList();

            // Assert: Kontrollerer at listen er sorteret alfabetisk efter Competition descending
            Assert.AreEqual("Vuelta a Espana", sortByCompetitionDesc[0].Competition);
            Assert.AreEqual("Danmark Rundt", sortByCompetitionDesc[4].Competition);
        }

        [TestMethod()]
        public void GetSortByYearTest()
        {
            // Opretter repository
            TrophiesRepository repository = new TrophiesRepository();

            // Act: Sorterer trofæer efter Year
            var sortByYear = repository.Get(sortBy: "year").ToList();

            // Assert: Kontrollerer at listen er sorteret korrekt
            Assert.AreEqual(2020, sortByYear[0].Year);
            Assert.AreEqual(2021, sortByYear[4].Year);

            // Act: Sorterer trofæer efter Year descending
            var sortByYearDesc = repository.Get(sortBy: "yeardesc").ToList();

            // Assert: Kontrollerer at listen er sorteret korrekt
            Assert.AreEqual(2021, sortByYearDesc[0].Year);
            Assert.AreEqual(2020, sortByYearDesc[4].Year);
        }
        [TestMethod()]
        public void GetSortByTest()
        {
            // Opretter repository
            TrophiesRepository repository = new TrophiesRepository();

            // Act: Sorterer trofæer efter ukendt parameter
            // Assert: Forventer ArgumentException
            Assert.ThrowsException<ArgumentException>(() => repository.Get(sortBy: "invalidParameter"));
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            // Opretter repositories
            TrophiesRepository repository = new TrophiesRepository();

            // Act: Henter trofæer fra TrophiesRepository
            Trophy trophy5 = repository.GetById(5);

            // Assert: Tjekker at trofæet med ID 5 hentes korrekt
            Assert.AreEqual(5, trophy5.Id);
            // Assert: Tjekker at null returneres, da ID 6 ikke eksisterer
            Assert.IsNull(repository.GetById(6));
        }

        [TestMethod()]
        public void AddTest()
        {
            // Arrange: Opretter repository og trofæer
            TrophiesRepository repository = new TrophiesRepository();
            Trophy trophy1 = new Trophy()
            {
                Competition = "Danmark Rundt",
                Year = 2024
            };
            Trophy trophy2 = new Trophy()
            {
                Competition = "Tour de France",
                Year = 2023
            };

            // Act: Tilføjer trofæer til repository
            repository.Add(trophy1);
            repository.Add(trophy2);

            // Assert: Kontrollerer at ID'erne tildeles korrekt
            Assert.AreEqual(1, trophy1.Id);
            Assert.AreEqual(2, trophy2.Id);

            // Assert: Kontrollerer at der er 7 trofæer i listen (2 tilføjet + 5 objekter i listen)
            Assert.AreEqual(7, repository.Get().Count());

            // Arrange: Opretter trofæ med for kort navn
            Trophy shortTrophyName = new Trophy()
            {
                Competition = "AB", // Invalid name
                Year = 2024
            };

            // Act & Assert: Forventer ArgumentOutOfRangeException pga. 2 tegn
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => repository.Add(shortTrophyName));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            // Arrange: Opretter repository
            TrophiesRepository repository = new TrophiesRepository();

            // Act: Fjerner trofæ med ID 5
            Trophy removedTrophy = repository.Remove(5);

            // Assert: Kontrollerer at trofæet er fjernet
            Assert.AreEqual(5, removedTrophy.Id);
            Assert.AreEqual(4, repository.Get().Count()); // Vi har kun 4 trofæer nu

            // Act & Assert: Forsøger at fjerne et trofæ med et ugyldigt ID (666 findes ikke) - forventer null
            Assert.IsNull(repository.Remove(666));

        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Arrange: Opretter repository og et opdateret trofæ
            TrophiesRepository repository = new TrophiesRepository();
            Trophy newTrophyData = new Trophy
            {
                Competition = "Paris-Roubaix",
                Year = 2000
            };

            // Act: Opdaterer trofæ med Id 4
            Trophy updatedTrophy = repository.Update(4, newTrophyData);

            // Assert: Kontrollerer at trofæet blev opdateret korrekt
            Assert.AreEqual("Paris-Roubaix", updatedTrophy.Competition);
            Assert.AreEqual(2000, updatedTrophy.Year);

            // Assert: Forsøger at opdatere et trofæ med et ugyldigt ID, forventer null
            Assert.IsNull(repository.Update(99, newTrophyData));
        }
    }
}