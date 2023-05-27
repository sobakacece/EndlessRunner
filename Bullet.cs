using Godot;
using System;
using System.Linq;

public partial class Bullet : Despawnable, IHurtBox
{
    public Vector2 MyTarget;
    public int MyDamage { get; set; }
    [Export] public float speed = 25;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // GD.Print($"Bullet Position is {this.Position}");
        this.GlobalPosition += MyTarget.Normalized() * speed;
    }
    public void ConnectToArea()
    {
        this.Connect("body_entered", new Callable(this, "Body_Collided"));

    }
    public void Body_Collided(Node2D body)
    {
        Despawn();
        foreach (Damageable child in body.GetChildren().OfType<Damageable>())
        {
            child.Hit(MyDamage, Vector2.Zero);
        }
    }

}
