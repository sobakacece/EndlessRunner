using Godot;
using System;

public partial interface IDespawnable
{
    [Export] VisibleOnScreenNotifier2D notifier { get; set; }
    public abstract void ConnectToNotifier();
    public abstract void Despawn();
        // notifier = this.GetNode<VisibleOnScreenNotifier2D>("Notifier");
        // notifier.Connect("screen_exited", new Callable(this, "Despawn"));
}
