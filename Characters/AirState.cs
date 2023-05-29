using Godot;
using System;

public partial class AirState : State
{
    [Export] float doubleJumpVelocity = 300;
    bool usedDoubleJump = false;
    [Export] State groundState, attackState;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        groundState = MyStateMachine.GetNode<State>("Ground");
        attackState = MyStateMachine.GetNode<State>("Attack");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void StateProcess(double delta)
    {
        // GD.Print($"Double Jump is used: {usedDoubleJump}");

        Landing();
    }
    private void Landing()
    {
        if (MyCharacter.IsOnFloor())
        {
            usedDoubleJump = false;
            nextState = groundState;
        }
    }
    public override void StateInput(InputEvent @event)
    {
        if (@event.IsActionPressed("attack"))
        {
            nextState = attackState;
        }
        if (@event.IsActionPressed("jump") && !usedDoubleJump)
        {
            usedDoubleJump = true;
            Jump();
        }
    }
    public void Jump()
    {
        if (MyCharacter.Velocity.Y > 0)
            MyCharacter.Velocity = Vector2.Up * doubleJumpVelocity;

        else
            MyCharacter.Velocity += new Vector2(0, -doubleJumpVelocity);

        MyAudio.Play();
    }
}
