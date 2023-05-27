using Godot;
using System;

public partial class GroundState : State
{
    [Export] public float jumpHolding = -25, jumpThreshold = -600;
    private float jumpVelocity;
    [Export] State airState, attackState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        jumpVelocity = MyCharacter.MyJumpVelocity;
        airState = MyStateMachine.GetNode<State>("Air");
        attackState = MyStateMachine.GetNode<State>("Attack");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // if (Input.IsActionPressed("jump") && jumpVelocity <= jumpThreshold)
        // {
        //     jumpVelocity+= jumpHolding;
        // }
    }
    public override void StateInput(InputEvent @event)
    {
        if (@event.IsActionReleased("jump"))
        {
            Jump();
        }
        if (@event.IsActionPressed("attack"))
        {
            nextState = attackState;
        }
    }
    public override void StateProcess(double delta)
    {
        if (Input.IsActionPressed("jump") && jumpVelocity > jumpThreshold)
        {
            // GD.Print(jumpVelocity);
            jumpVelocity -= jumpHolding;
        }


    }
    public override void OnEnter()
    {
        playback.Travel("walk");
    }
    public void Jump()
    {
        MyCharacter.Velocity = new Vector2(0, jumpVelocity);
        jumpVelocity = MyCharacter.MyJumpVelocity; //returning jumpVelocity to default
        playback.Travel("jump");
        nextState = airState;
    }
}
