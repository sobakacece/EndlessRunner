using Godot;
using System;

public abstract partial class Decoration : Node2D, IDespawnable
{
	public abstract float MyHeight {get; set;}
	public abstract int RangeOfRandom {get; set;}
	[Export] public VisibleOnScreenNotifier2D notifier {get; set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// ConnectToNotifier();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public virtual void Despawn()
	{
		// GD.Print("Despawned");
		GetParent().QueueFree();
	}
	public virtual void ConnectToNotifier()
	{
		notifier = GetNode<VisibleOnScreenNotifier2D>("Notifier");
        notifier.Connect("screen_exited", new Callable(this, "Despawn"));
	}
}
