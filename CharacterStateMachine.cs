using Godot;
using System;
using System.Collections.Generic;

public partial class CharacterStateMachine : Node
{
    public Dictionary<string, State> stateList;
    [Export]
    private AnimationTree animationTree;
    private Player character;
    [Export]
    public Player MyBody
    {
        get => character;
        set => character = value;
    }
    private State currState;
    [Export]
    public State CurrentState
    {
        get => currState;
        set => currState = value;
    }
    public override void _Ready()
    {

    }
    public override void _EnterTree() //Moved Ready() actions to EnterTree() because it is called after Child Nodes fully initialized
    {
        stateList = new Dictionary<string, State>();
        foreach (State child in this.GetChildren())
        {
            //parsing needed variables to States
            child.playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
            child.MyCharacter = character;
            child.MyStateMachine = this;
            child.MyAnimationTree = animationTree;
            stateList.Add(child.ToString(), child);
            child.InterruptState += OnInterruption;
        }
        base._EnterTree();
    }
    public override void _Process(double delta)
    {
        currState.StateProcess(delta);
        if (currState.nextState != null)
            SwitchStates(currState.nextState);


    }

    public bool IsMoveable()
    {
        // GD.Print(currState.moveable.ToString() + " " + currState.ToString());
        return currState.moveable;
    }
    private void SwitchStates(State newState)
    {

        if (currState != null)
        {
            currState.OnExit();
            currState.nextState = null;
        }
        currState = newState;
        currState.OnEnter();
    }
    public override void _Input(InputEvent @event)
    {
        currState.StateInput(@event);
    }
    public void OnInterruption(State newState)
    {
        GD.Print(newState.ToString() + " interrupted " + currState.ToString());

        SwitchStates(newState);
    }

}
