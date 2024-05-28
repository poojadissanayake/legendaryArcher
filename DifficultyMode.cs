using SplashKitSDK;
#nullable disable

public class DifficultyMode
{
    private Rectangle easyBtn;
    private Rectangle mediumBtn;
    private Rectangle hardBtn;
    private Color easyBtnColor;
    private Color mediumBtnColor;
    private Color hardBtnColor;
    private Color gameModeColor;
    private bool btnClicked;
    private int targetScore;

    public DifficultyMode()
    {
        easyBtn = new Rectangle { X = 100, Y = 37.5, Width = 100, Height = 50 };
        mediumBtn = new Rectangle { X = 100, Y = 125, Width = 100, Height = 50 };
        hardBtn = new Rectangle { X = 100, Y = 212.5, Width = 100, Height = 50 };

        easyBtnColor = SplashKit.RGBColor(6, 214, 160);
        mediumBtnColor = SplashKit.RGBColor(255, 209, 102);
        hardBtnColor = SplashKit.RGBColor(239, 71, 111);
        btnClicked = false;

        gameModeColor = SplashKit.RGBColor(7, 59, 76);

    }

    public void Draw(Window gameMode, Font font, Color windowColor)
    {
        gameMode.Clear(windowColor);

        gameMode.FillRectangle(easyBtnColor, easyBtn);
        gameMode.DrawText("Easy", Color.Black, font, 14, easyBtn.X + 10, easyBtn.Y + 15);

        gameMode.FillRectangle(mediumBtnColor, mediumBtn);
        gameMode.DrawText("Medium", Color.Black, font, 14, mediumBtn.X + 10, mediumBtn.Y + 15);

        gameMode.FillRectangle(hardBtnColor, hardBtn);
        gameMode.DrawText("Hard", Color.Black, font, 14, hardBtn.X + 10, hardBtn.Y + 15);

        gameMode.Refresh();

        CheckButtonClick();

        if (IsClicked())
        {
            gameMode.Close();
        }
    }

    public void CheckButtonClick()
    {
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            Point2D mousePosition = SplashKit.MousePosition();

            if (SplashKit.PointInRectangle(mousePosition, easyBtn))
            {
                btnClicked = true;
                targetScore = 30;
            }
            else if (SplashKit.PointInRectangle(mousePosition, mediumBtn))
            {
                btnClicked = true;
                targetScore = 50;
            }
            else if (SplashKit.PointInRectangle(mousePosition, hardBtn))
            {
                btnClicked = true;
                targetScore = 120;
            }
        }
    }

    public bool IsClicked()
    {
        return btnClicked;
    }
    public int GetTargetScore()
    {
        return targetScore;
    }
}
