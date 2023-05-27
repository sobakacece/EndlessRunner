using Godot;
using System;
using System.Linq;
public partial class Bee : Area2D, IHurtBox
{
    private AnimationNodeStateMachinePlayback playback;
    private AnimationTree animationTree;
    private SignalBus signalBus;
    private Damageable damageableComponent;
    [Export] public int MyDamage { get; set; }
    public Vector2 MyGlobalPosition { get; set; }
    [Export] private float speed, distanceThreshold;
	private Vector2 spawnPoint, direction = Vector2.Down;
    public override void _Ready()
    {
		MyDamage = 1;
		ConnectToArea();
		spawnPoint = this.GlobalPosition;
        signalBus = GetNode<SignalBus>("/root/SignalBus");

        animationTree = GetNode<AnimationTree>("AnimationTree");
        playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");

        damageableComponent = GetNode<Damageable>("Damageable");
        damageableComponent.OnHit += GetShot;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
		this.GlobalPosition += direction * speed;
		if (spawnPoint.DistanceTo(this.GlobalPosition) >= distanceThreshold)
		{
			direction = -direction;
			spawnPoint = this.GlobalPosition;
		}
    }
    public void GetShot(Node2D body, int damage, Vector2 knockbackDirection)
    {
        playback.Travel("dead");
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
