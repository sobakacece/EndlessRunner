using Godot;
using System;
using System.Linq;

public partial class Bullet : HurtBox, IDespawnable
{
    public VisibleOnScreenNotifier2D notifier { get; set; }
    public Vector2 MyTarget;
    public override int MyDamage { get; set; }
    public Vector2 MyGlobalPosition { get; set; }
    [Export] public float speed = 25;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        MyDamage = 1;
        ConnectToArea();
        ConnectToNotifier();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // GD.Print($"Bullet Position is {this.Position}");
        this.GlobalPosition += MyTarget.Normalized() * speed;
    }
    protected override void ConnectToArea()
    {
        this.Connect("area_entered", new Callable(this, "Body_Collided")); //Bee's are area2D, so we are checking for are2D objects

    }
    public override void Body_Collided(Node2D body)
    {
        GD.Print("Direct Hit");
        foreach (Damageable child in body.GetChildren().OfType<Damageable>())
        {
            child.Hit(MyDamage, Vector2.Zero);
        }
        Despawn();

    }
    public void ConnectToNotifier()
    {

        notifier = GetNode<VisibleOnScreenNotifier2D>("Notifier");
        notifier.Connect("screen_exited", new Callable(this, "Despawn"));
    }
    public void Despawn()
    {
        GD.Print("Bullet despawned");
        this.QueueFree();
    }

}
