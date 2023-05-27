using Godot;
using System;
using System.Linq;

public partial interface IHurtBox
{
    // Called when the node enters the scene tree for the first time.
    public int MyDamage { get; set; }
    public abstract void Body_Collided(Node2D body);
    protected abstract void ConnectToArea();

}
