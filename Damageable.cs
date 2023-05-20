using Godot;
using System;

public partial class Damageable : Node
{
    [Export]
    public bool Knockable {get; set;}
    [Export]
    private string deadAnimationNode = "dead";
    SignalBus signalBus;
    [Signal]
    public delegate void OnHitEventHandler(Node2D node, int damage, Vector2 knockbackDirection);
    public override void _Ready()
    {
        signalBus = GetNode<SignalBus>("/root/SignalBus");
        var animTree = GetParent().GetNode<AnimationTree>("AnimationTree"); 
        animTree.Connect("animation_finished", new Callable(this, "Animation_Finished"));
    }
    // Called when the node enters the scene tree for the first time.
    [Export]
    private int health = 20;
    public int MyHealth
    {
        get => health;

        set
        {
            signalBus.EmitSignal(SignalBus.SignalName.HealthChanged, GetParent(), value - health);
            health = value;
        }
    }
    public void Hit(int damage, Vector2 knockbackDirection)
    {
        MyHealth -= damage;
        EmitSignal(SignalName.OnHit, GetParent(), damage, knockbackDirection);
        // if (MyHealth <= 0)
        // {
        //     GetParent().QueueFree();
        // }
    }
    public void Animation_Finished(string anim_name)
    {
        if (anim_name == deadAnimationNode && this.Owner is not Player)
        {
            GetParent().QueueFree();
        }
    }
}