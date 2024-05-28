using SplashKitSDK;
using System.Collections.Generic;
#nullable disable

public class GameWindow
{
    private Archer archer;
    private Arrow arrow;
    private List<TargetFace> targets;
    private List<int> scorePerTargetFace;
    private Color faceOuter;
    private Color faceMid;
    private Color faceInner;
    private Color resultsColor;
    private double tfVelocityY;
    private int arrowsShot;
    private const int MaxArrows = 5;
    private SplashKitSDK.Timer timer;
    private const double Timedelay = 1000;
    private int totalScore; // Total score accumulated
    private TargetFace shot; // Rectangle that the arrow hit
    string title;
    string message;
    int targetScore;
    public GameWindow(Window game, int targetScore)
    {
        archer = new Archer();
        arrow = new Arrow();
        timer = new SplashKitSDK.Timer("timer");

        CreateTargetFace(game);

        faceOuter = SplashKit.RGBColor(67, 188, 205);
        faceMid = SplashKit.RGBColor(255, 94, 91);
        faceInner = SplashKit.RGBColor(249, 200, 14);
        resultsColor = SplashKit.RGBColor(21, 97, 109);

        this.targetScore = targetScore;
        tfVelocityY = 1;
        arrowsShot = 0;
        totalScore = 0;
    }
    public void Draw(Window gameWindow, Font font, Color scoreColor, Color backgroundColor)
    {
        Rectangle scores = new Rectangle { X = 450, Y = 10, Width = 100, Height = 70 };

        gameWindow.Clear(backgroundColor);

        gameWindow.FillRectangle(scoreColor, scores);
        gameWindow.DrawText("Legendary Archer", Color.Black, font, 14, 250, 10);
        gameWindow.DrawText("Target: " + targetScore, Color.Black, font, 14, scores.X + 10, scores.Y + 15);
        gameWindow.DrawText("Score: " + totalScore, Color.Black, font, 14, scores.X + 10, scores.Y + 40);

        archer.Draw();

        Color[] colors = { faceInner, faceMid, faceOuter };
        int i = 0;
        foreach (TargetFace tf in targets)
        {
            tf.Draw(colors[i++ % colors.Length]);
        }
        if (arrowsShot < MaxArrows)
        {
            ChancesAvailable();
            arrow.Draw();
        }

        gameWindow.Refresh();

        if (arrowsShot == MaxArrows && totalScore > 1 && totalScore < targetScore)
        {
            title = "GAME OVER!";
            message = "Better Luck next time!";
            EndWindow(gameWindow, font, title, message);
        }
        else if (arrowsShot == MaxArrows)
        {
            title = "GAME OVER!";
            message = "Better Luck next time!";
            EndWindow(gameWindow, font, title, message);
        }
        else if (totalScore >= targetScore)
        {
            title = "CONGRATULATIONS!";
            message = "You Won!";
            EndWindow(gameWindow, font, title, message);
        }

    }

    void EndWindow(Window gameWindow, Font font, string title, string message)
    {
        gameWindow.Close();

        Window result = new Window(title, 300, 300);
        result.Clear(resultsColor);
        result.DrawText(message, Color.White, font, 20, 50, 130);
        result.Refresh(60);
        SplashKit.Delay(10000);
    }

    private void ChancesAvailable()
    {
        int positionX = 20;
        for (int i = 0; i < MaxArrows - arrowsShot; i++)
        {
            SplashKit.FillCircle(Color.Red, positionX, 20, 10);
            positionX += 25;
        }
    }

    public void CreateTargetFace(Window gameWindow)
    {
        double midY = gameWindow.Height / 2;
        double[] lengths = { 60, 80, 100 };
        double targetWidth = 15;
        double positionX = 500;
        targets = new List<TargetFace>();
        scorePerTargetFace = new List<int>();

        foreach (double length in lengths)
        {
            double rectangleY = midY - length / 2;
            targets.Add(new TargetFace(positionX, rectangleY, targetWidth, length));
            positionX += targetWidth;
        }
        // Assign scores to each targetface
        scorePerTargetFace.Add(30); // S 
        scorePerTargetFace.Add(20); // M 
        scorePerTargetFace.Add(10); // L 
    }

    public void Update(Window game)
    {
        ProcessInput();
        if (arrow.VelocityX != 0 || arrow.VelocityY != 0)
        {
            arrow.UpdatePosition();
            for (int i = 0; i < targets.Count; i++)
            {
                if (arrow.CheckCollision(targets[i]))
                {
                    arrow.Stop();
                    shot = targets[i];
                    // Update the total score
                    totalScore += scorePerTargetFace[i];
                    timer.Start();
                    break;
                }
            }
            // Check if arrow leaves the window
            if (arrow.arrowX < 0 || arrow.arrowX > game.Width || arrow.arrowY < 0 || arrow.arrowY > game.Height)
            {
                arrow.Stop();
                timer.Start();
            }
        }
        else if (timer.Ticks >= Timedelay)
        {
            arrow = new Arrow();
            arrowsShot++;
            timer.Reset();
            timer.Stop();
        }
        UpdateTargetFace();
    }

    public void ProcessInput()
    {
        if (SplashKit.MouseClicked(MouseButton.LeftButton) && arrow.VelocityX == 0 && arrowsShot < MaxArrows)
        {
            double targetX = SplashKit.MouseX();
            double targetY = SplashKit.MouseY();

            double deltaX = targetX - arrow.arrowX;
            double deltaY = targetY - arrow.arrowY;
            // normalize the velocity
            // calculating the Euclidean distance between two points in a 2D space -> Math.Sqrt(dx * dx + dy * dy)
            // to ensure -> arrow moves at a consistent speed 
            // regardless of the distance between current position and the target position
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            arrow.VelocityX = deltaX / distance * 5;
            arrow.VelocityY = deltaY / distance * 5;
        }
    }

    public void UpdateTargetFace()
    {
        // Check if targetface hits the top or bottom of the limit 120-500
        foreach (TargetFace tf in targets)
        {
            if (tf.Y <= 120 || tf.Y + tf.Height >= 500)
            {
                tfVelocityY *= -1;
                break;
            }
        }

        // all rectangles of targetface as one single unit
        foreach (TargetFace tf in targets)
        {
            tf.UpdatePosition(tfVelocityY);
        }
    }
}