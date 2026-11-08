
using System;
using System.ComponentModel;
using System.Windows.Input;
using BullsAndCowsProject.Models;

namespace BullsAndCowsProject.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private BullsAndCowsGame game;

        private string guess = "";
        public string Guess
        {
            get { return guess; }
            set
            {
                guess = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Guess)));
            }
        }

        private string statusMessage = "";
        public string StatusMessage
        {
            get { return statusMessage; }
            set
            {
                statusMessage = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(StatusMessage)));
            }
        }

        private string errorMessage = "";
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(ErrorMessage)));
            }
        }

        private string attemptsText = "Attempts: 0";
        public string AttemptsText
        {
            get { return attemptsText; }
            set
            {
                attemptsText = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(AttemptsText)));
            }
        }

        private bool hasStatusMessage;
        public bool HasStatusMessage
        {
            get { return hasStatusMessage; }
            set
            {
                hasStatusMessage = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(HasStatusMessage)));
            }
        }

        private bool hasErrorMessage;
        public bool HasErrorMessage
        {
            get { return hasErrorMessage; }
            set
            {
                hasErrorMessage = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(HasErrorMessage)));
            }
        }

        private bool canSubmitGuess = true;

        private string randomNumberText = "";

        public string RandomNumberText
        {
            get { return randomNumberText; }
            set
            {
                randomNumberText = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(RandomNumberText)));
            }
        }

        public bool CanSubmitGuess
        {
            get { return canSubmitGuess; }
            set
            {
                canSubmitGuess = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(CanSubmitGuess)));
            }
        }

        public ICommand SubmitGuessCommand { get; }
        public ICommand NewGameCommand { get; }

        public ICommand GenerateRandomNumberCommand { get; }

        // יצירת המשחק והפקודות
        public GameViewModel()
        {
            game = new BullsAndCowsGame();

            SubmitGuessCommand = new SubmitGuessButtonCommand(this);
            NewGameCommand = new NewGameButtonCommand(this);

            GenerateRandomNumberCommand = new GenerateRandomNumberButtonCommand(this);

        }

        // פעולה שמתבצעת בשליחת ניחוש
        public void SubmitGuess()
        {
            if (game.IsGameOver)
            {
                StatusMessage = "The game is over. start a new one";
                HasStatusMessage = true;
                CanSubmitGuess = false;
                return;
            }

            string error;

            if (!BullsAndCowsGame.IsValidGuess(Guess, out error))
            {
                ErrorMessage = error;
                HasErrorMessage = true;
                return;
            }

            ErrorMessage = "";
            HasErrorMessage = false;

            GuessResult result = game.SubmitGuess(Guess);

            StatusMessage =
                result.Bulls + " Bulls, " + result.Cows + " Cows";

            if (result.IsWinningGuess)
            {
                StatusMessage = "Correct! You guessed it in " +
                                game.AttempCount + " attempts.";
                CanSubmitGuess = false;
            }

            HasStatusMessage = true;
            AttemptsText = "Attempts: " + game.AttempCount;
        }

        // התחלת משחק חדש
        public void StartNewGame()
        {
            game.startNewGame();

            Guess = "";
            StatusMessage = "";
            ErrorMessage = "";
            AttemptsText = "Attempts: 0";
            HasStatusMessage = false;
            HasErrorMessage = false;
            CanSubmitGuess = true;
        }

        // הפעלת הגרלת המספר והצגתו במסך
        public void GenerateRandomNumber()
        {
            game.GenerateRandomNumber();
            RandomNumberText = game.RandomNumber.ToString();
        }
    }


        // פקודה לשליחת ניחוש
        public class SubmitGuessButtonCommand : ICommand
        {
            private GameViewModel viewModel;

            public SubmitGuessButtonCommand(GameViewModel vm)
            {
                viewModel = vm;
            }

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object? parameter)
            {
                return true;
            }

            public void Execute(object? parameter)
            {
                viewModel.SubmitGuess();
            }
        }

        // פקודה להתחלת משחק חדש
        public class NewGameButtonCommand : ICommand
        {
            private GameViewModel viewModel;

            public NewGameButtonCommand(GameViewModel vm)
            {
                viewModel = vm;
            }

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object? parameter)
            {
                return true;
            }

            public void Execute(object? parameter)
            {
                viewModel.StartNewGame();
            }
        }

    // פקודה להפעלת הגרלת המספר
    public class GenerateRandomNumberButtonCommand : ICommand
    {
        private GameViewModel viewModel;

        public GenerateRandomNumberButtonCommand(GameViewModel vm)
        {
            viewModel = vm;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            viewModel.GenerateRandomNumber();
        }
    }

}

