using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player : Sprite {


    private float _jumpTimer;
    public float CameraOffset;
    
    public Player(Texture2D texture, bool isCollideable) : base (texture, isCollideable) {
        
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture, Position, Colour);   
    }

    public override void Update (GameTime gameTime, List<Component> components) {
        
        _jumpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        Velocity = Physics.ApplyDragAndGravity(Velocity, _isAirborne);

       
              
        
        if (Keyboard.GetState().IsKeyDown(Keys.A) && _isAirborne == false) {
            if (Velocity.X > -3.2f) { 
                Velocity.X -= 1f;}
            if (CameraOffset > -200f) {
                CameraOffset -= 3f;
            }
        } else if (Keyboard.GetState().IsKeyDown(Keys.D) && _isAirborne == false) {
            if (Velocity.X < 3.2f) { 
                Velocity.X += 1f; }
            if (CameraOffset < 200f) {
                CameraOffset += 3f;
            }
        }

       


        if (Keyboard.GetState().IsKeyUp(Keys.A) && CameraOffset < 0) {
            CameraOffset += 2f;
        }
        if (Keyboard.GetState().IsKeyUp(Keys.D) && CameraOffset > 0) {
            CameraOffset -= 2f;
        }
        
        

        //X Collision Check + Iterator
        CollisionCheckX(components);

        //Y Collision Check + Iterator
        CollisionCheckY(components);

        if (Keyboard.GetState().IsKeyDown(Keys.Space) && _isAirborne == false && _jumpTimer > .1f) {
            _jumpTimer = 0;
            _isAirborne = true;
            Velocity.Y -= 6;
        }

        Position += Velocity;
    }

    private void CollisionCheckX(List<Component> components) {
        if (Velocity.X > 0 || Velocity.X < 0) {
            foreach (var component in components) {
                if (component is Player || !((Sprite)component).IsCollideable) {
                    continue;
                }
                if ((Velocity.X > 0 && IsTouchingLeft((Sprite)component)) ||//or nextline
                (Velocity.X < 0 && IsTouchingRight((Sprite)component))) {
                if (Velocity.X > 0) {
                    for (int i = (int)Velocity.X; i >= 0; i--) {
                        Velocity.X = i;
                        if (IsTouchingLeft((Sprite)component)) {
                            continue;
                        } else if (!IsTouchingLeft((Sprite)component)) {
                            break;
                        }
                    } 
                } else if (Velocity.X < 0) {
                    for (int i = (int)Velocity.X; i <= 0; i++) {
                        Velocity.X = i;
                        if (IsTouchingRight((Sprite)component)) {
                            continue;
                        }else if (!IsTouchingRight((Sprite)component)) {
                            break;
                            }
                        }
                    }
                }
            }
        }
    }
    
    private void CollisionCheckY(List<Component> components) {
        if (Velocity.Y > 0 || Velocity.Y < 0) {
            foreach (var component in components) {
                if (component is Player || !((Sprite)component).IsCollideable) {
                    continue;
                }
                if ((Velocity.Y > 0 && IsTouchingTop((Sprite)component)) ||//or nextline
                (Velocity.Y < 0 && IsTouchingBottom((Sprite)component))) {
                if (Velocity.Y > 0) {
                    for (int i = (int)Velocity.Y; i >= 0; i--) {
                        Velocity.Y = i;
                        if (IsTouchingTop((Sprite)component)) {
                            _isAirborne = false;
                            _jumpTimer = 0;
                            continue;
                        } else if (!IsTouchingTop((Sprite)component)) {
                            break;
                        }
                    }
                } else if (Velocity.Y < 0) {
                    for (int i = (int)Velocity.Y; i <= 0; i++) {
                        this.Velocity.Y = i;
                        if (IsTouchingBottom((Sprite)component)) {
                            continue;
                        } else if (!IsTouchingBottom((Sprite)component)) {
                            break;
                            }
                        }
                    }
                }
            }
        }
    }
}