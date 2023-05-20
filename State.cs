using Godot;
using System;

public partial class State : Node
{
    [Signal]
    public delegate void InterruptStateEventHandler(State newState);
    // private bool moveable = true;
    [Export]
    public bool moveable;
    public CharacterBody2D MyCharacter { get; set; }
    public CharacterStateMachine MyStateMachine {get; set; }
    public AnimationTree MyAnimationTree {get; set; }
    public Vector2 velocity;
    public State nextState;
    public AnimationNodeStateMachinePlayback playback; //for traveling between states in anim tree
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }
    public virtual void StateProcess(double delta)
    {
        return;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // velocity = Character.Velocity;
        // Character.Velocity = velocity;
    }
    public override string ToString()
    {
        return this.Name;
    }
    public virtual void StateInput(InputEvent @event)
    {
        return;
    }
    public virtual void OnExit()
    {
        return;
    }
    public virtual void OnEnter()
    {
        return;
    }

    public virtual void OnInterruption()
    {
        return;
    }

}
