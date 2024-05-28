using SplashKitSDK;
#nullable disable

public class Archer
{
    private Bitmap _ArcherBitmap;

    public Archer()
    {
        _ArcherBitmap = new Bitmap("Archer", "archer.png");

    }
    public void Draw()
    {
        _ArcherBitmap.Draw(20, 290);
    }

}