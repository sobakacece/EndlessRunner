using Godot;
using System;

public partial class HitState : State

{
    [Export]
    Vector2 knockbackSpeed = new Vector2(100, 0);
    [Export]
    Damageable damageable;
    [Export]
    State deadState;
    [Export]
    State returnState;
    // [Export]
    // CharacterStateMachine characterStateMachine;
    Timer timer;

    string deadAnimationNode = "dead";
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        // characterStateMachine = (CharacterStateMachine)this.GetParent();
        damageable.OnHit += OnDamageableHit;

        deadState = MyStateMachine.stateList["Dead"];
        returnState = MyStateMachine.stateList["Ground"];

        timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimeout;
    }
    public override void StateProcess(double delta)
    {
        // GD.Print(Math.Round(Convert.ToDecimal(timer.TimeLeft), 2));
        base.StateProcess(delta);
    }

    public override void OnEnter()
    {
        MyAudio.Play();
        // MyCharacter.Velocity = knockbackSpeed;
        MyCharacter.SetCollisionLayerValue(2, false); //turning off Player collision layer;
        playback.Travel("hit");
        timer.Start();
    }

    public override void OnExit()
    {
        MyCharacter.SetCollisionLayerValue(2, true); //turning onn Player collision layer;
        timer.Stop();
    }
    private void OnDamageableHit(Node node, int damage, Vector2 knockbackDirection)
    {
        if (damageable.MyHealth > 0)
        {
            MyCharacter.Velocity = knockbackSpeed * knockbackDirection;
            EmitSignal(SignalName.InterruptState, this);
            // GD.Print(MyCharacter.Velocity.ToString());
            // GD.Print("Health: " + damageable.MyHealth);
        }
        else
        {
            GD.Print("dead");
            EmitSignal(SignalName.InterruptState, deadState);
            playback.Travel(deadAnimationNode);
        }
    }
    public void OnTimeout()
    {
        nextState = returnState;
    }





}
