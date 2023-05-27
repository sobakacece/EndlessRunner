using Godot;
using System;
using System.Linq;

public partial class Tree : Despawnable, IHurtBox
{
    [Export] public int MyDamage { get; set; }
    public Vector2 MyGlobalPosition { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        MyGlobalPosition = this.GlobalPosition;
        MyDamage = 1;
        ConnectToArea();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void ConnectToArea()
    {
        this.Connect("body_entered", new Callable(this, "Body_Collided"));
    }
    public void Body_Collided(Godot.Node2D body)
    {
        foreach (Damageable child in body.GetChildren().OfType<Damageable>())
        {
            Vector2 directionToTarget = body.GlobalPosition - this.GlobalPosition;
            if (directionToTarget.X > 0 && child.Knockable) // > than half of sword hitbox?
            {
                child.Hit(MyDamage, Vector2.Right);

            }
            else if (directionToTarget.X < 0 && child.Knockable)
            {
                child.Hit(MyDamage, Vector2.Left);

            }
            else
                child.Hit(MyDamage, Vector2.Zero);

        }
    }

}
