using Godot;
using System;
using System.Linq;
public partial class Bee : HurtBox, IDespawnable
{
    public VisibleOnScreenNotifier2D notifier {get; set;}
    private AnimationNodeStateMachinePlayback playback;
    private AnimationTree animationTree;
    private SignalBus signalBus;
    private Damageable damageableComponent;
    [Export] public override int MyDamage { get; set; }
    public Vector2 MyGlobalPosition { get; set; }
    // [Export] private float speed, distanceThreshold;
	// private Vector2 spawnPoint, direction = Vector2.Down;
    public override void _Ready()
    {
		MyDamage = 1;
		ConnectToArea();
		// spawnPoint = this.GlobalPosition;
        signalBus = GetNode<SignalBus>("/root/SignalBus");

        animationTree = GetNode<AnimationTree>("AnimationTree");
        playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");

        damageableComponent = GetNode<Damageable>("Damageable");
        damageableComponent.OnHit += GetShot;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
		// this.GlobalPosition += direction * speed;
		// if (spawnPoint.DistanceTo(this.GlobalPosition) >= distanceThreshold)
		// {
		// 	direction = -direction;
		// 	spawnPoint = this.GlobalPosition;
		// }
    }
    public void GetShot(Node2D body, int damage, Vector2 knockbackDirection)
    {
        playback.Travel("dead");
    }
    protected override void ConnectToArea()
    {
        base.ConnectToArea();
    }
    public override void Body_Collided(Godot.Node2D body)
    {
        base.Body_Collided(body);
    }
    public void ConnectToNotifier()
    {
        
        notifier = GetNode<VisibleOnScreenNotifier2D>("Notifier");
        notifier.Connect("screen_exited", new Callable(this, "Despawn"));
    }
    public void Despawn()
    {
        playback.Travel("dead");
    }


}
