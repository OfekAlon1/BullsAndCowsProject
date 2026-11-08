using System;
using System.Collections.Generic;
using System.Text;

namespace BullsAndCowsProject.Models
{
    public class GuessResult
    {
        public int Bulls { get; set; } // כמה ספרות היו במקום הנכון
        public int Cows { get; set; } // כמה ספרות נכונות אך לא במקום הנכון
        public bool IsWinningGuess => Bulls == 4; // בודק אם לשחקן יש ארבע מספרים במקום הנכון

    }
}
