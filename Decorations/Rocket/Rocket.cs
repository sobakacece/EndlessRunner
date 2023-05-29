using Godot;
using System;
using System.Linq;

public partial class Rocket : HurtBox, IDespawnable
{
    [Export] public float speed = 5;
    public VisibleOnScreenNotifier2D notifier { get; set; }
    [Export] public override int MyDamage { get; set; }
    public Vector2 MyGlobalPosition { get; set; }
    private AnimationNodeStateMachinePlayback playback;
    private AnimationTree animationTree;
    private SignalBus signalBus;
    private Damageable damageableComponent;
    private bool dying = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        MyGlobalPosition = this.GlobalPosition;
        MyDamage = 1;
        ConnectToArea();
        ConnectToNotifier();
        signalBus = GetNode<SignalBus>("/root/SignalBus");

        animationTree = GetNode<AnimationTree>("AnimationTree");
        playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");

        damageableComponent = GetNode<Damageable>("Damageable");
        damageableComponent.OnHit += GetShot;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (!dying)
            this.GlobalPosition += speed * Vector2.Left;
    }

    public void GetShot(Node2D body, int damage, Vector2 knockback)
    {
        Despawn();
    }



    public override void Collided(Godot.Node2D body)
    {
        foreach (Damageable child in body.GetChildren().OfType<Damageable>())
        {
           child.Hit(MyDamage, Vector2.Zero);

        }
        Despawn();
    }
    public void Despawn()
    {
        // GD.Print("Tree despawmed");
        dying = true;
        playback.Travel("dead");
        SetCollisionMaskValue(2, false);


    }
    public void ConnectToNotifier()
    {
        notifier = GetNode<VisibleOnScreenNotifier2D>("Notifier");
        notifier.Connect("screen_exited", new Callable(this, "Despawn"));
    }
}
