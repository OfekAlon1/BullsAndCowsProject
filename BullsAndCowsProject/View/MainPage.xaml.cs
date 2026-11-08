
using BullsAndCowsProject.ViewModels;

namespace BullsAndCowsProject.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // חיבור המסך למשחק
        BindingContext = new GameViewModel();
    }
}
