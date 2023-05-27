using Godot;
using System;

public partial class AttackState : State
{
    PackedScene bulletScene;
    State groundState, airState;
    public override void _Ready()
    {
        airState = MyStateMachine.GetNode<State>("Air");
        groundState = MyStateMachine.GetNode<State>("Ground");

        bulletScene = (PackedScene)ResourceLoader.Load("res://bullet.tscn");
        MyAnimationTree.Connect("animation_finished", new Callable(this, "Animation_Finished"));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    public override void OnEnter()
    {
        playback.Travel("attack");
        Attack();
    }
    public void Attack()
    {
        Bullet bullet = (Bullet)bulletScene.Instantiate();
        this.AddChild(bullet);
        bullet.MyTarget = MyCharacter.GetGlobalMousePosition() - MyCharacter.GlobalPosition;
        // GD.Print($"Target is {bullet.MyTarget}, player is {MyCharacter.GlobalPosition}");
        bullet.GlobalPosition = MyCharacter.GlobalPosition + GetNode<Marker2D>("BulletSpawner").GlobalPosition;
        // GD.Print($"Player position {MyCharacter.GlobalPosition} Bullet position {bullet.GlobalPosition}");
    }
    public void Animation_Finished(string anim_name)
    {
        if (anim_name == "attack")
        {
            if (!MyCharacter.IsOnFloor())
            {
                nextState = airState;
            }
            else
                nextState = groundState;
        }
    }

}
