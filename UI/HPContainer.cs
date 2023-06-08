using Godot;
using System;

public partial class HPContainer : TextureRect
{
    [Export] public Texture2D MyemptyHPTexture;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        MyemptyHPTexture = (Texture2D)ResourceLoader.Load("res://Art/UI/hp_empty.png");

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
