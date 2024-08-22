namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
        private Button lastClickedButton;
        private int matchesfound = 0;
        private bool findingMatch = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;
            List<string> animalEmoji = [
                "😊", "😊", "😂","😂","🤣","🤣","❤️","❤️","😍","😍","😒","😒", "😘","😘","💕", "💕", 
            ];

            foreach(Button button in AnimalButtons.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            };

            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);

        }

        int tenthsOfSecondsElapsed = 0;
        private bool TimerTick()
        {
            if (!this.IsLoaded) return false;

            tenthsOfSecondsElapsed++;

            TimeElapsed.Text = "Time elapsed: " + 
                (tenthsOfSecondsElapsed/10F).ToString("0.0s");

            if (PlayAgainButton.IsVisible) {
                tenthsOfSecondsElapsed = 0;
                return false;
            };
            return true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                if (!findingMatch && !string.IsNullOrWhiteSpace(clickedButton.Text))
                {
                    clickedButton.BackgroundColor = Colors.ForestGreen;
                    lastClickedButton = clickedButton;
                    findingMatch = true;
                }
                else {
                    if (clickedButton != lastClickedButton && clickedButton.Text == lastClickedButton.Text)
                    {
                        if (!string.IsNullOrWhiteSpace(clickedButton.Text))
                        {
                            matchesfound++;
                            clickedButton.Text = " ";
                            lastClickedButton.Text = " ";
                        };
                    };
                    clickedButton.BackgroundColor = Colors.LightBlue;
                    lastClickedButton.BackgroundColor = Colors.LightBlue;
                    findingMatch = false;



                };
                if (matchesfound == 8)
                {
                    PlayAgainButton.IsVisible = true;
                    AnimalButtons.IsVisible = false;
                    matchesfound = 0;

                }
            };
        }
    }

}

