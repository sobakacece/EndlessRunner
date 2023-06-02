using Godot;
using System;

public partial class GameOverScreen : GameScreen
{
    [Export] protected Label scoreLabel;
    public bool ScoreBeaten;
    public override void _Ready()
    {
        base._Ready();
        ConnectToNodes();
        scoreLabel.Text = $"Your Score: {MyGlobalSettings.FinalScore.ToString("0000000")}";
        ScoreBeaten = MyGlobalSettings.MyRecords.CheckHighScore();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void ShowRecordScreen()
    {
        ChangeScreen(MyGlobalSettings.MyRecords);
        MyGlobalSettings.MyRecords.ReturnScreen = this;
    }




}
