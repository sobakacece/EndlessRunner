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

        bulletScene = (PackedScene)ResourceLoader.Load("res://Props/bullet.tscn");
        MyAnimationTree.AnimationFinished += Animation_Finished;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    public override void OnEnter()
    {
        MyAudio.Play();
        playback.Travel("attack");
        Attack();
    }
    public void Attack()
    {
        Bullet bullet = (Bullet)bulletScene.Instantiate();
        this.AddChild(bullet);
        bullet.MyTarget = MyCharacter.GetGlobalMousePosition() - MyCharacter.GlobalPosition;
        bullet.GlobalPosition = MyCharacter.GlobalPosition + GetNode<Marker2D>("BulletSpawner").GlobalPosition;
    }
    public void Animation_Finished(StringName anim_name)
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
