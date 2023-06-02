using Godot;
using System;

public partial class DeadState : State
{
    PackedScene gameOverScreen;

    [Export] string animaDeadNode = "dead";
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        gameOverScreen = (PackedScene)ResourceLoader.Load("res://UI/GameOverScreen.tscn");
        MyAnimationTree.Connect("animation_finished", new Callable(this, "AnimationFinished"));
    }
    public void AnimationFinished(string anim_name)
    {
        if (MyCharacter is Player && anim_name == animaDeadNode)
        {
            GetTree().Paused = true;
            GameOverScreen gameOverScreenNode = (GameOverScreen)gameOverScreen.Instantiate();
            MyCharacter.Owner.AddChild(gameOverScreenNode);
            if (gameOverScreenNode.ScoreBeaten)
            {
                gameOverScreenNode.ShowRecordScreen();
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    public override void OnEnter()
    {
        MyAudio.Play();
        MyCharacter.Velocity = Vector2.Zero;
    }
}
