using Godot;
using System;
using System.Collections.Generic;
public partial class GlobalSettings : Node
{
    int busEffect, busMusic, busMaster;
    public Slider VFXslider, MusicSlider, GlobalSlider;
    private OptionsScreen optionsInstance;
    private CreditsScreen creditsInstance;
    private RecordsScreen recordsIntstance;
    public OptionsScreen MyOptions { get => optionsInstance; }
    public CreditsScreen MyCredits { get => creditsInstance; }
    public RecordsScreen MyRecords { get => recordsIntstance; }
    public ConfigFile config;
    public AudioStreamPlayer GlobalMusic {get => GetNode<AudioStreamPlayer>("/root/GameMusic");}
    public float FinalScore { get; set; }
    public override void _Ready()
    {
        config = new ConfigFile();
        LoadScores();

        optionsInstance = (OptionsScreen)AddGlobalScreen("res://UI/OptionsScreen.tscn");
        creditsInstance = (CreditsScreen)AddGlobalScreen("res://UI/CreditsScreen.tscn");
        recordsIntstance = (RecordsScreen)AddGlobalScreen("res://UI/RecordsScreen.tscn");

        busMaster = AudioServer.GetBusIndex("Master");  

        GlobalSlider = MyOptions.GlobalSlider;
        GlobalSlider.Connect("drag_ended", new Callable(this, "Master"));

    }
    public void Master(bool value)
    {
        ChangeVolume(busMaster, GlobalSlider);

    }
    public void ChangeVolume(int busIndex, Slider slider)
    {
        if ((int)slider.Value == (int)slider.MinValue)
        {
            AudioServer.SetBusMute(busIndex, true);
        }
        else
        {
            AudioServer.SetBusMute(busIndex, false);
            AudioServer.SetBusVolumeDb(busIndex, (float)slider.Value);
        }
    }
    public Dictionary<string, int> LoadScores()
    {
        var scoreData = new Dictionary<string, int>();
        Error err = config.Load("user://scores.cfg");
        if (err != Error.Ok)
        {
            if (err == Error.FileNotFound)
            {
                ResetConfig();
            }
            GD.Print(err.ToString());
            return null;
        }
        foreach (String player in config.GetSections())
        {
            var player_name = (String)config.GetValue(player, "player_name");
            var player_score = (int)config.GetValue(player, "best_score");
            scoreData[player_name] = player_score;
            // GD.Print(scoreData[player_name], player_name);
        }

        return scoreData;


    }
    public void ResetConfig()
    {

        for (int i = 0; i < 10; i++)
        {
            config.SetValue($"Player{i + 1}", "player_name", $"Player {i + 1}");
            config.SetValue($"Player{i + 1}", "best_score", 10000 - i * 1000);

        }
        config.Save("user://scores.cfg");
        MyRecords.UpdateText(LoadScores());
        GD.Print("Config file reseted");
    }
    public GameScreen AddGlobalScreen(string path)
    {
        GameScreen MyScreen = new GameScreen();
        PackedScene Screen = (PackedScene)ResourceLoader.Load(path);
        MyScreen = (GameScreen)Screen.Instantiate();
        this.AddChild(MyScreen);
        MyScreen.Visible = false;
        return MyScreen;
    }

    public void OnRestart()
    {

    }
}
