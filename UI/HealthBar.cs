using Godot;
using System;
using System.Collections.Generic;

public partial class HealthBar : BoxContainer
{
    #region HealthUIVariables
    PackedScene packedHP;
    [Export] Texture2D emptyHP;
    List<HPContainer> hpList = new List<HPContainer>();
    BoxContainer healthBar;
    int counter = 1;

    #endregion
    #region  ScoreVariables
    float startingPoint, currentScore;
    Label scoreLabel;
    public const int startMod = 5;
    #endregion
    Player player;
    SignalBus signalBus;

    public override void _Ready()
    {
        player = (Player)GetNode("/root/TestLevel/Player");
        startingPoint = player.GlobalPosition.X;

        healthBar = this.GetNode<BoxContainer>("HealthBar");
        scoreLabel = this.GetNode<Label>("ScoreContainer/Score");
        // GD.Print("Order: UI");
        packedHP = (PackedScene)ResourceLoader.Load("res://UI/hp.tscn");
        signalBus = GetNode<SignalBus>("/root/SignalBus");

        signalBus.Restart += OnRestart;
        signalBus.HealthChanged += ChangeTexture;
        ParsingPlayer();

        // signalBus.PlayerEnteredScene += ParsingPlayer; //TODO Remove this shit and do it in a normal way
        // signalBus.PlayerDead += () => signalBus.EmitSignal(SignalBus.SignalName.SaveScore, currentScore);
    }
    public void OnRestart()
    {
        foreach (HPContainer child in healthBar.GetChildren())
        {
            healthBar.RemoveChild(child);
        }
        hpList = new List<HPContainer>();
        counter = 1;
        ParsingPlayer();
    }
    public void ParsingPlayer()
    {
        // GD.Print("Player Parsed");
        for (int i = 0; i < player.PlayerHealth; i++)
        {
            HPContainer healthPoint = (HPContainer)packedHP.Instantiate();
            healthBar.AddChild(healthPoint);
            hpList.Add(healthPoint);
        }
    }
    public override void _EnterTree()
    {


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
        hpList[hpList.Count - counter].Texture = hpList[hpList.Count - counter].MyemptyHPTexture;
        counter++;
    }
}
