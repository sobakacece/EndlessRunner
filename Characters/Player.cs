using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] public float speed = 300.0f;
    // [Export] public float jumpVelocity = -400.0f;
    // [Export] public int MyHealth { get => 3; set => MyHealth = value; }

    public int MyScore {get; set;}

    private Vector2 direction;
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    AnimationTree animationTree;
	AnimationNodeStateMachinePlayback playback;
    CharacterStateMachine stateMachine;

    public override void _Ready()
    {
        animationTree = this.GetNode<AnimationTree>("AnimationTree");
        animationTree.Active = true;

		playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
        stateMachine = this.GetNode<CharacterStateMachine>("CharacterStateMachine");

    }
    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y += gravity * (float)delta;       

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        direction = Input.GetVector("left", "right", "up", "down");
        if (direction != Vector2.Zero && stateMachine.IsMoveable() && stateMachine.CurrentState.ToString() != "Hit")
        {
            velocity.X = direction.X * speed;
        }
        else if (stateMachine.CurrentState.ToString() != "Hit")
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
        }

        Velocity = velocity;
        MoveAndSlide();
		AnimationUpdateParameters();
    }
    public void AnimationUpdateParameters()
    {
        animationTree.Set("parameters/move/blend_position", direction.X);

    }
}
