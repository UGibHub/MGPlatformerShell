using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Sprite : Component {
    protected Texture2D _texture;
    public Vector2 Position;
    public Vector2 Velocity;
    public Color Colour;
    public bool IsCollideable;
    public Physics2D Physics;
    protected bool _isAirborne;
    public Rectangle Rectangle { get {
        return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
    } }

    public Sprite(Texture2D texture, bool isCollideable) {
        _texture = texture;
        Colour = Color.White;
        IsCollideable = isCollideable;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture, Position, Colour);
    }

    public override void Update(GameTime gameTime, List<Component> components) {

    }

    #region Collision
		protected bool IsTouchingLeft(Sprite sprite) {
                  return Rectangle.Right + Velocity.X > sprite.Rectangle.Left &&
                  Rectangle.Left < sprite.Rectangle.Left &&
                  Rectangle.Bottom > sprite.Rectangle.Top &&
                  Rectangle.Top < sprite.Rectangle.Bottom;
            }
		
		protected bool IsTouchingRight(Sprite sprite) {
                  return Rectangle.Left + Velocity.X < sprite.Rectangle.Right &&
                  Rectangle.Right > sprite.Rectangle.Right &&
                  Rectangle.Bottom > sprite.Rectangle.Top &&
                  Rectangle.Top < sprite.Rectangle.Bottom;
            }
		
		protected bool IsTouchingTop(Sprite sprite) {
                  return Rectangle.Bottom + Velocity.Y > sprite.Rectangle.Top &&
                  Rectangle.Top < sprite.Rectangle.Top &&
                  Rectangle.Right > sprite.Rectangle.Left &&
                  Rectangle.Left < sprite.Rectangle.Right;
            }
		
		protected bool IsTouchingBottom(Sprite sprite) {
                  return Rectangle.Top + Velocity.Y < sprite.Rectangle.Bottom &&
                  Rectangle.Bottom > sprite.Rectangle.Bottom &&
                  Rectangle.Right > sprite.Rectangle.Left &&
                  Rectangle.Left < sprite.Rectangle.Right;
            }
	#endregion
}