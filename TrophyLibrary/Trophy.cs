namespace TrophyLibrary
{
    /// <summary>
    /// Repræsenterer et trofæ med et ID, konkurrencenavn og år.
    /// </summary>
    public class Trophy
    {
        public int Id { get; set; }
        public string Competition { get; set; }
        public int Year { get; set; }

        public Trophy(int id, string competition, int year)
        {
            Id = id;
            Competition = competition;
            Year = year;
        }

        public Trophy()
        {

        }
        /// <summary>
        /// Returnerer en streng, der repræsenterer det aktuelle objekt.
        /// </summary>
        /// <returns>En streng, der repræsenterer det aktuelle objekt.</returns>
        public override string ToString()
        {
            return $"Id: {Id}, Competition: {Competition}, Year: {Year}";
        }

        /// <summary>
        /// Validerer konkurrencenavnet.
        /// </summary>
        /// <exception cref="ArgumentNullException">Kastes, når konkurrencenavnet er null.</exception>
        /// <exception cref="ArgumentException">Kastes, når konkurrencenavnet er mindre end 3 tegn langt.</exception>
        public void ValidateCompetition()
        {
            if (Competition == null)
            {
                throw new ArgumentNullException($"Competition cannot be null {Competition}");
            }
            if (Competition.Length < 3)
            {
                throw new ArgumentOutOfRangeException($"Competition must be at least 3 characters long {Competition}");
            }
        }

        /// <summary>
        /// Validerer året, hvor trofæet blev tildelt.
        /// </summary>
        /// <exception cref=ArgumentOutOfRangeException">Kastes, når året ikke er mellem 1970 og 2024.</exception>
        public void ValidateYear()
        {
            if (Year < 1970 || Year > 2024)
            {
                throw new ArgumentOutOfRangeException($"Year must be between 1970 and 2024 {Year}");
            }
        }

        /// <summary>
        /// Validerer trofæets konkurrencenavn og år.
        /// </summary>
        public void Validate()
        {
            ValidateCompetition();
            ValidateYear();
        }
    }
}
