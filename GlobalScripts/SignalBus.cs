using Godot;
using System;

public partial class SignalBus : Node
{
    [Signal] public delegate void HealthChangedEventHandler(Node node, int health_changed);
    [Signal] public delegate void RestartEventHandler();




}

