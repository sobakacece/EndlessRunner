using Godot;
using System;
using System.Linq;

public partial class Tree : HurtBox, IDespawnable
{
    public VisibleOnScreenNotifier2D notifier {get; set;}
    [Export] public override int MyDamage { get; set; }
    public Vector2 MyGlobalPosition { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        MyGlobalPosition = this.GlobalPosition;
        MyDamage = 1;
        ConnectToArea();
        ConnectToNotifier();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }


    public override void Body_Collided(Godot.Node2D body)
    {
        base.Body_Collided(body);
    }
    public void Despawn()
    {
        // GD.Print("Tree despawmed");
        GetParent().QueueFree();

    }
    public void ConnectToNotifier()
    {
        notifier = GetNode<VisibleOnScreenNotifier2D>("Notifier");
        notifier.Connect("screen_exited", new Callable(this, "Despawn"));
    }

}
