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
    public const int startMod = 5; //modifier for score
    private float acceleration, scoreDelta; //acceleration is amount of additional speed and scoreDelta is how often it gives to Player
    int accelerationMod = 1; //score counter
    #endregion
    Player player;
    SignalBus signalBus;
    GlobalSettings global;

    public override void _Ready()
    {
        player = (Player)GetNode("/root/TestLevel/Player");


        healthBar = this.GetNode<BoxContainer>("HealthBar");
        scoreLabel = this.GetNode<Label>("ScoreContainer/Score");
        signalBus = GetNode<SignalBus>("/root/SignalBus");
        global = GetNode<GlobalSettings>("/root/GlobalSettings");

        packedHP = (PackedScene)ResourceLoader.Load("res://UI/hp.tscn");

        startingPoint = player.GlobalPosition.X;
        acceleration = player.MyAccelaration;
        scoreDelta = player.MyAccelerationDelta;

        signalBus.Restart += OnRestart;
        signalBus.HealthChanged += DecreasePlayerHP;
        CreatingHealtbar();
    }
    public void OnRestart()
    {
        foreach (HPContainer child in healthBar.GetChildren())
        {
            healthBar.RemoveChild(child);
        }
        hpList = new List<HPContainer>();
        counter = 1;
        CreatingHealtbar();
    }
    public void CreatingHealtbar()
    {
        for (int i = 0; i < player.PlayerHealth; i++)
        {
            HPContainer healthPoint = (HPContainer)packedHP.Instantiate();
            healthBar.AddChild(healthPoint);
            hpList.Add(healthPoint);
        }
    }
    public override void _Process(double delta)
    {

        if (currentScore < (player.GlobalPosition.X - startingPoint) / startMod) //keep the score from decreasing
            currentScore = (player.GlobalPosition.X - startingPoint) / startMod;

        // player.MyScore = (int)currentScore;
        scoreLabel.Text = currentScore.ToString("0000000");
        Accelerate();
        global.FinalScore = currentScore;
    }
    public void DecreasePlayerHP(Node node, int damage)
    {
        if (node is Player)
        {
            hpList[hpList.Count - counter].Texture = hpList[hpList.Count - counter].MyemptyHPTexture;
            counter++;
        }
    }
    private void Accelerate()
    {
        if (currentScore / scoreDelta > accelerationMod)
        {
            // GD.Print("Accelerated");
            player.speed += acceleration * accelerationMod;
            accelerationMod++;
        }
    }
}
