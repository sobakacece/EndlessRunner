using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] public float speed = 300.0f;

    public int PlayerHealth { get; set; }

    private Vector2 direction;
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    AnimationTree animationTree;
    AnimationNodeStateMachinePlayback playback;
    CharacterStateMachine stateMachine;
    Damageable damageableComponent;
    PackedScene gamePausedScreen;
    private float acceleration = 10, accelerationDelta = 1000;
    [Export] public float MyAccelaration { get => acceleration; set => acceleration = value; } //How much accelerates
    [Export] public float MyAccelerationDelta { get => accelerationDelta; set => accelerationDelta = value; } // How often accelerates
    [Export] public float MyJumpVelocity { get; set; }
    public override void _Ready()
    {
        gamePausedScreen = (PackedScene)ResourceLoader.Load("res://UI/GamePauseScreen.tscn");


        animationTree = this.GetNode<AnimationTree>("AnimationTree");
        animationTree.Active = true;

        playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
        stateMachine = this.GetNode<CharacterStateMachine>("CharacterStateMachine");

        damageableComponent = this.GetNode<Damageable>("Damageable");
        PlayerHealth = damageableComponent.MyHealth;
    }
    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        if (!IsOnFloor() && stateMachine.CurrentState.ToString() != "Dead")
            velocity.Y += gravity * (float)delta;



        direction = Vector2.Right;
        if (stateMachine.IsMoveable() && stateMachine.CurrentState.ToString() != "Hit")
        {
            velocity.X = Mathf.MoveToward(Velocity.X, direction.X * speed, speed);
        }

        Velocity = velocity;
        MoveAndSlide();
        AnimationUpdateParameters();
    }
    public void AnimationUpdateParameters()
    {
        animationTree.Set("parameters/move/blend_position", direction.X);

    }
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("pause") && GetTree().Paused == false)
        {
            GameScreen gamePaused = (GameScreen)gamePausedScreen.Instantiate();
            this.Owner.AddChild(gamePaused);
            GetTree().Paused = true;
            gamePaused.Visible = true;
        }
    }
}
