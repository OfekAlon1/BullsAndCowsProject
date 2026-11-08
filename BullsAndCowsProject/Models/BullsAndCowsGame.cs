using System;
using System.Collections.Generic;
using System.Text;

namespace BullsAndCowsProject.Models
{
    public class BullsAndCowsGame
    {
        // האורך של המספר הסודי תמיד 4
        private const int CodeLength = 4;
        private Random _random = new Random();
        // המספר אותו צריך לנחש
        private string _secretNumber;

        public int AttempCount { get; private set; }
        public bool IsGameOver { get; private set; }

        public int RandomNumber { get; private set; }

        //
        // מופעל כאשר יוצרים משחק חדש
        public BullsAndCowsGame()
        {
            startNewGame();
        }
        // מתחיל משחק חדש
        public void startNewGame()
        {
            _secretNumber = GenerateSecretNumber(); // מגריל מספר חדש
            AttempCount = 0;
            IsGameOver = false;
        }
        // פעולה שמייצרת מספר בן 4 ספרות, ללא מספרים חוזרים וללא 0 בהתחלה
        private string GenerateSecretNumber()
        {
            string secret = "";
            int RNum = _random.Next(1, 10);
            secret = RNum.ToString();
            while (secret.Length < CodeLength)
            {
                RNum = _random.Next(0, 10);
                bool exists = false;
                for (int i = 0; i < secret.Length; i++)
                {
                    if (secret[i] == RNum.ToString()[0])
                    {
                        exists = true;
                    }
                }

                if (!exists)
                {
                    secret += RNum.ToString();
                }
            }
            return secret;
        }
        // בודקת אם הניחוש של המשתמש חוקי
        public static bool IsValidGuess(string guess, out string errorMessage)
        {
            if (guess.Length != CodeLength)
            {
                errorMessage = "the guess must have 4 digits";
                return false;
            }
            if (guess[0] == '0')
            {
                errorMessage = "the first digit cant be 0";
                return false;
            }
            for (int i = 0; i < CodeLength; i++)
            {
                bool IsNum = false;
                for (int j = 0; j <= 9; j++)
                {
                    int digitValue = guess[i] - '0';
                    if (digitValue == j)
                        IsNum = true;
                }
                if (!IsNum)
                {
                    errorMessage = "the guess should contains only numbers";
                    return false;
                }
            }
            for (int i = 0; i < guess.Length; i++)
            {
                for (int j = i + 1; j < guess.Length; j++)
                {
                    if (guess[i] == guess[j])
                    {
                        errorMessage = "all digits must be diffrent from each other";
                        return false;
                    }
                }
            }
            errorMessage = string.Empty;
            return true;
        }
        public GuessResult SubmitGuess(string guess)
        {
            AttempCount++;
            int bulls = 0;
            int cows = 0;

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == _secretNumber[i])
                {
                    bulls++;
                }
                else
                {
                    for (int j = 0; j < _secretNumber.Length; j++)
                    {
                        if (guess[i] == _secretNumber[j])
                        {
                            cows++;
                        }
                    }
                }
            }
            GuessResult result = new GuessResult();
            result.Bulls = bulls;
            result.Cows = cows;
            if (result.IsWinningGuess)
            {
                IsGameOver = true;
            }
            return result;
        }


        // מגרילה מספר אקראי בין 1000 ל-9999
        public void GenerateRandomNumber()
        {
            RandomNumber = _random.Next(1000, 10000);
        }

    }

}
