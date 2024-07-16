using Microsoft.Xna.Framework;

public class Physics2D {// Intended for side on physics, in a world with gravity.
        
    public Physics2D() {
    }

    public Vector2 ApplyGravity(Vector2 velocityVec2) {
        var outgoing = velocityVec2;
        var Gravity = new Vector2 (0, .2f);//Y will be positive to simulate gravity, due to Y plane is inverted.
        outgoing  += Gravity;
        return outgoing;
    }

    public Vector2 ApplyDrag(Vector2 velocityVec2, bool isAirborne) {
        var outgoing = velocityVec2;
        if (!isAirborne){
            if (outgoing.X > 0 && outgoing.X <= .1f) {
                outgoing.X = 0;
            }else if (outgoing.X > 0 && outgoing.X <= .2f){
                outgoing.X -= .1f;
            } else if (outgoing.X > 0 && outgoing.X <= .4f) {
                outgoing.X -= .3f;
            } else if (outgoing.X > 0) {
                outgoing.X -= .5f;
            }

        }

        if (!isAirborne) {
            if (outgoing.X < 0 && outgoing.X >= -.1f) {
                outgoing.X = 0;
            } else if (outgoing.X < 0 && outgoing.X >= -.2f){
                outgoing.X += .1f;
            } else if (outgoing.X < 0 && outgoing.X >= -.4f) {
                outgoing.X += .3f;
            } else if (outgoing.X < 0 ) {
                outgoing.X += .5f;
            } 
            
        }
        return outgoing;
    }

    public Vector2 ApplyDragAndGravity(Vector2 velocityVec2, bool isAirborne) { //Two in one.
        var dragvec = ApplyDrag(velocityVec2, isAirborne);
        var outgoingVec = ApplyGravity(dragvec);
        return outgoingVec;
    }




}