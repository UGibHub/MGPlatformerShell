using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Tile : Sprite {
    
    public Tile(Texture2D texture, bool isCollideable, int xPos, int yPos) : base (texture, isCollideable) {
        _texture = texture;
        Position = new Vector2(xPos, yPos);  
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture, Position, Colour);
    }

    public override void Update(GameTime gameTime, List<Component> components) {

    }
}