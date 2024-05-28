using System.Collections.Generic;
using SplashKitSDK;
public class TargetFace
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public double Width { get; private set; }
    public double Height { get; private set; }
    public double VelocityY { get; set; }
    public bool movingDown;

    public TargetFace(double x, double y, double width, double height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        movingDown = false;
        VelocityY = 1;
    }

    public void Draw(Color color)
    {
        SplashKit.FillRectangle(color, X, Y, Width, Height);
    }

    public bool IsPointInside(double pointX, double pointY)
    {
        return pointX >= X && pointX <= X + Width && pointY >= Y && pointY <= Y + Height;
    }

    public void UpdatePosition(double deltaY)
    {
        Y += deltaY;
    }
}