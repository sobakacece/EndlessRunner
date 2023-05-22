using Godot;
using System;
using System.Collections.Generic;

public partial class HealthBar : BoxContainer
{
    #region HealthUIVariables
    PackedScene packedHP;
    [Export] Texture2D emptyHP;
    List<TextureRect> hpList = new List<TextureRect>();
    BoxContainer healthBar;
    int counter = 1;

    #endregion
    #region  ScoreVariables
    float startingPoint, currentScore = 0;
    Label scoreLabel;
    public const int startMod = 5;
    #endregion
    Player player;
    SignalBus signalBus;

    public override void _Ready()
    {
        startingPoint = player.GlobalPosition.X;
    }
    public void ParsingPlayer(Player parameter, int health)
    {
        // GD.Print("Player Parsed");
        player = parameter;
        for (int i = 0; i < health; i++) //TODO Remove this shit and do it in a normal way
        {
            TextureRect healthPoint = (TextureRect)packedHP.Instantiate();
            healthBar.AddChild(healthPoint);
            hpList.Add(healthPoint);
        }
    }
    public override void _EnterTree()
    {
        packedHP = (PackedScene)ResourceLoader.Load("res://UI/hp.tscn");
        signalBus = GetNode<SignalBus>("/root/SignalBus");

        signalBus.HealthChanged += ChangeTexture;
        signalBus.PlayerEnteredScene += ParsingPlayer;
        // signalBus.PlayerDead += () => signalBus.EmitSignal(SignalBus.SignalName.SaveScore, currentScore);

        healthBar = this.GetNode<BoxContainer>("HealthBar");
        scoreLabel = this.GetNode<Label>("ScoreContainer/Score");

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

        if (currentScore < (player.GlobalPosition.X - startingPoint) / startMod) //keep the score from decreasing
            currentScore = (player.GlobalPosition.X - startingPoint) / startMod;

        // player.MyScore = (int)currentScore;
        scoreLabel.Text = currentScore.ToString("0000000");
        signalBus.FinalScore = currentScore;
    }
    public void ChangeTexture(Node node, int damage)
    {
        hpList[hpList.Count - counter].Texture = emptyHP;
        counter++;

    }
}
