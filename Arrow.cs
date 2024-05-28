using SplashKitSDK;
#nullable disable

public class Arrow
{
    public double arrowX { get; private set; }
    public double arrowY { get; private set; }
    public double VelocityX { get; set; }
    public double VelocityY { get; set; }
    public Bitmap arrowBitmap { get; private set; }

    public Arrow()
    {
        arrowX = 20;
        arrowY = 290;
        arrowBitmap = new Bitmap("arrow", "arrow.png");
    }

    public void Draw()
    {
        arrowBitmap.Draw(arrowX, arrowY);
    }

    public void UpdatePosition()
    {
        arrowX += VelocityX;
        arrowY += VelocityY;
    }

    public bool CheckCollision(TargetFace targetFace)
    {
        return SplashKit.BitmapRectangleCollision(arrowBitmap, arrowX, arrowY, SplashKit.RectangleFrom(targetFace.X, targetFace.Y, targetFace.Width, targetFace.Height));
    }

    public void Stop()
    {
        VelocityX = 0;
        VelocityY = 0;
    }
}