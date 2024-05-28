using SplashKitSDK;
using System.Collections.Generic;
#nullable disable

public class LegendaryArcher
{
    private Font font;
    private Color gameModeColor;
    private Color backgroundColor;
    private Color scoreColor;
    private int targetScore;

    private DifficultyMode difficultyMode;

    public LegendaryArcher()
    {
        gameModeColor = SplashKit.RGBColor(7, 59, 76);
        backgroundColor = SplashKit.RGBColor(17, 138, 178);
        scoreColor = SplashKit.RGBColor(6, 214, 160);
        font = SplashKit.LoadFont("LoveDays", "LoveDays-2v7Oe.ttf");


        difficultyMode = new DifficultyMode();

    }

    public void StartGame()
    {
        // using statement is a special construct that is used to ensure that resources are properly disposed of after use
        // The IDisposable interface in the .NET framework is used to implement the disposal pattern for releasing unmanaged resources.
        // The Dispose() method is part of the 'IDisposable' interface, which Window must implement. 
        // This method is designed to release any resources the window holds, such as graphical resources
        // when using 'using' with an object, it must implement the 'IDisposable' interface, 
        // which provides a mechanism for releasing unmanaged resources
        using (Window gameModeWindow = new Window("Game Mode Selection", 300, 300))
        {
            SelectGameMode(gameModeWindow);
        }
        SplashKit.Delay(100);
        using (Window gameWindow = new Window("Legendary Archer", 600, 600))
        {
            PlayGame(gameWindow);
        }
    }

    private void SelectGameMode(Window window)
    {
        while (!window.CloseRequested)
        {
            SplashKit.ProcessEvents();
            difficultyMode.Draw(window, font, gameModeColor);
            targetScore = difficultyMode.GetTargetScore();

        }
    }

    private void PlayGame(Window window)
    {
        GameWindow game = new GameWindow(window, targetScore);
        while (!window.CloseRequested)
        {
            SplashKit.ProcessEvents();
            game.Update(window);

            SplashKit.ClearScreen(backgroundColor);
            game.Draw(window, font, scoreColor, backgroundColor);

            SplashKit.RefreshScreen(60);
        }
    }
}
