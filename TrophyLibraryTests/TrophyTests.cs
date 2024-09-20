using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrophyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrophyLibrary.Tests
{
    [TestClass()]
    public class TrophyTests
    {
        /// <summary>
        /// Tester om ToString() metoden returnerer den korrekte streng af objektet.
        /// </summary>
        [TestMethod()]
        public void ToStringTest()
        {
            // Arrange: Opretter et trofæ objekt med gyldige værdier (Id, Competition og Year).
            Trophy trophy = new Trophy
            {
                Id = 1,
                Competition = "Danmark Rundt",
                Year = 2024
            };

            // Act: Kalder ToString() metoden på trofæet.
            string result = trophy.ToString();

            // Assert: Kontrollerer om string er lig med result.
            Assert.AreEqual("Id: 1, Competition: Danmark Rundt, Year: 2024", result);
        }

        /// <summary>
        /// Tester om ValidateCompetition, inkl. null, tomme og gyldige værdier.
        /// </summary>
        [TestMethod]

        public void ValidateCompetitionTest()
        {
            // Arrange: Opretter en competition med null værdi (ugyldigt)
            Trophy trophy = new Trophy
            {
                Id = 2,
                Competition = null, // Invalid name
                Year = 2022
            };

            // Act & Assert: Forventer ArgumentNullException, da navnet er null
            Assert.ThrowsException<ArgumentNullException>(() => trophy.ValidateCompetition());


            // Arrange: Opretter en competition med for kort navn (under 3 tegn)
            Trophy trophy2 = new Trophy
            {
                Id = 3,
                Competition = "US", // Invalid name (too short)
                Year = 2022
            };

            // Act & Assert: Forventer ArgumentOutOfRangeException, da navnet er tomt
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophy2.ValidateCompetition());

            // Arrange: Opretter en competition med en gyldig værdi (præcis 3 tegn)
            Trophy trophy3 = new Trophy
            {
                Id = 4,
                Competition = "USA", // Valid name
                Year = 2022
            };
        }

        /// <summary>
        /// Validerer året, hvor trofæet blev tildelt.
        /// </summary>
        [TestMethod]
        public void ValidateYearTest()
        {
            // Arrange: Opretter et trofæ objekt med et ugyldigt år
            Trophy trophy = new Trophy
            {
                Id = 5,
                Competition = "Tour de France",
                Year = 1969 // Invalid year
            };
            // Act & Assert: Forventer ArgumentOutOfRangeException, da året er ugyldigt
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophy.ValidateYear());

            // Arrange: Opretter et trofæ objekt med et gyldigt år (limit tester 1970)
            Trophy trophy2 = new Trophy
            {
                Id = 6,
                Competition = "Giro d'Italia",
                Year = 1970 // Valid year
            };
            // Act & Assert: Forventer ingen exceptions, da året er gyldigt
            trophy2.ValidateYear();

            // Arrange: Opretter et trofæ objekt med et gyldigt år (limit tester 2024)
            Trophy trophy3 = new Trophy
            {
                Id = 7,
                Competition = "Vuelta a Espana",
                Year = 2024 // Valid year
            };
            // Act & Assert: Forventer ingen exceptions, da året er gyldigt
            trophy3.ValidateYear();

            // Arrange: Opretter et trofæ objekt med et ugyldigt år (2025)
            Trophy trophy4 = new Trophy
            {
                Id = 8,
                Competition = "Tour de France",
                Year = 2025 // Invalid year
            };

            // Act & Assert: Forventer ArgumentOutOfRangeException, da året er ugyldigt
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophy4.ValidateYear());

        }

        /// <summary>
        /// Tester om Validate() metoden validerer både konkurrencenavn og år korrekt.
        /// </summary>
        [TestMethod]
        public void ValidateTest()
        {
            // Arrange: Opretter et trofæ objekt med ugyldigt konkurrencenavn (null)
            Trophy trophy = new Trophy
            {
                Id = 9,
                Competition = null, // Invalid competition
                Year = 2020
            };
            // Act & Assert: Forventer ArgumentNullException, da navnet er null
            Assert.ThrowsException<ArgumentNullException>(() => trophy.Validate());

            // Arrange: Opretter et trofæ objekt med ugyldigt konkurrencenavn (for kort)
            Trophy trophy2 = new Trophy
            {
                Id = 10,
                Competition = "US", // Invalid name (too short)
                Year = 2020
            };
            // Act & Assert: Forventer ArgumentOutOfRangeException pga. for kort navn
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophy2.Validate());


            // Arrange: Opretter et trofæ objekt med ugyldigt år (under minimum)
            Trophy trophy3 = new Trophy
            {
                Id = 11,
                Competition = "Tour de France",
                Year = 1790 // Invalid year
            };
            // Act & Assert: Forventer ArgumentOutOfRangeException pga. ugyldigt år
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophy3.Validate());


            // Arrange: Opretter et trofæ objekt med gyldige værdier
            Trophy trophy4 = new Trophy
            {
                Id = 12,
                Competition = "USA",
                Year = 2023 // Valid year
            };
            // Act & Assert: Forventer ingen exceptions for gyldige værdier
            trophy4.Validate();
        }

        // Tester constructors (Ved ikke om det er nødvendigt?)

        /// <summary>
        /// Tester om konstruktøren korrekt initialiserer værdierne.
        /// </summary>
        [TestMethod()]
        public void TrophyConstructorTest()
        {
            // Arrange & Act: Opretter et trofæ objekt med gyldige værdier
            Trophy trophy = new Trophy(13, "Champions League", 2024);

            // Assert: Kontrollerer om værdierne blev initialiseret korrekt
            Assert.AreEqual(13, trophy.Id);
            Assert.AreEqual("Champions League", trophy.Competition);
            Assert.AreEqual(2024, trophy.Year);
        }

    }
}