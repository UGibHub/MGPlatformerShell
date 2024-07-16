using Microsoft.Xna.Framework;
using PlatformerShell;

public class Camera {

    public Matrix Transformation;

    public Camera() {}

    public void CameraFollow(Sprite sprite) {

        var Position = Matrix.CreateTranslation(
            -sprite.Position.X - sprite.Rectangle.Width / 2,
            -sprite.Position.Y - sprite.Rectangle.Height / 2,
            0
        );

        var Offset = Matrix.CreateTranslation(
            Game1.ScreenWidth / 2 + ((Player)sprite).CameraOffset,
            Game1.ScreenHeight / 2,
            0
        );

        Transformation = Position * Offset;
    }
}