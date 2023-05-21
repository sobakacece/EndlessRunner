using System;
using Godot;

interface IDespawnable
{
    bool WasOnScreen();
    void ConnectToNotifierNode();
    void Despawn();
}