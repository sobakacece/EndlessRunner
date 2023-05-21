using Godot;
using System;
using System.Linq;

public partial class HurtBox : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public int MyDamage{get; set;}
	public override void _Ready()
	{
		MyDamage = 5;
		// ConnectToArea();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public virtual void Body_Collided(Node2D body)
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
	protected virtual void ConnectToArea()
	{
		this.Connect("body_entered", new Callable(this, "Body_Collided")); 
	}

}
