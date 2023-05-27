using Godot;
using System;

public partial class Despawnable : Node2D
{
    [Export] VisibleOnScreenNotifier2D notifier;
    public override void _Ready()
    {
        notifier = this.GetNode<VisibleOnScreenNotifier2D>("Notifier");
        notifier.Connect("screen_exited", new Callable(this, "Despawn"));
    }
    public virtual void Despawn()
    {
        // GD.Print(this.Name + " is Despawned");
        this.QueueFree();
    }
}
