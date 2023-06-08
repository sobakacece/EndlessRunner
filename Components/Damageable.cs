using Godot;
using System;

public partial class Damageable : Node
{
    [Export] public bool Knockable { get; set; }
    [Export] private string deadAnimationNode = "dead";
    SignalBus signalBus;
    [Signal] public delegate void OnHitEventHandler(Node2D node, int damage, Vector2 knockbackDirection);
    public override void _Ready()
    {
        signalBus = GetNode<SignalBus>("/root/SignalBus");

        var animTree = GetParent().GetNode<AnimationTree>("AnimationTree");
        animTree.AnimationFinished += Dead_Animation_Finished;
    }
    [Export] private int health = 20;
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
        // GD.Print("Damage: " + damage);
        EmitSignal(SignalName.OnHit, GetParent(), damage, knockbackDirection);
    }
    public void Dead_Animation_Finished(StringName anim_name)
    {
        if (anim_name == deadAnimationNode)
        {
            GetParent().QueueFree();
        }
    }
}