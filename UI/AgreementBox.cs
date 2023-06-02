using Godot;
using System;

public partial class AgreementBox : Panel
{
	[Export] Button btnYes, btnNo;
	RecordsScreen recordsScreen {get => (RecordsScreen)GetParent();}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		btnYes.Pressed += ResetRecords;
		btnNo.Pressed += () => this.Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void ResetRecords()
	{
		recordsScreen.MyGlobalSettings.ResetConfig();
		recordsScreen.UpdateText(recordsScreen.MyGlobalSettings.LoadScores());
		this.Hide();
	}
}
