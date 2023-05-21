using Godot;
using System;

public partial class Decoration : HurtBox
{
    [Export] VisibleOnScreenNotifier2D notifier;
    public override void _Ready()
    {
        notifier = this.GetNode<VisibleOnScreenNotifier2D>("Notifier");
        notifier.Connect("screen_exited", new Callable(this, "Despawn"));

        ConnectToArea();
    }
    public void Despawn()
    {
        this.QueueFree();
    }
}
