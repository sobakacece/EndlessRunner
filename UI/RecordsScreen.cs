using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public partial class RecordsScreen : GameScreen
{
    // Called when the node enters the scene tree for the first time.
    [Export] public Label scoreLabel, playerLabel;
    [Export] Panel inputPanel, agreementPanel;
    [Export] Button playerButton, clearButton;
    [Export] TextEdit playerEdit;
    Dictionary<string, int> records;
    public GameScreen ReturnScreen { get; set; }

    public override void _Ready()
    {
        records = MyGlobalSettings.LoadScores();
        UpdateText(records);

        ConnectToNodes();
    }
    public override void ConnectToNodes()
    {
        quitButton.Pressed += Quit;
        playerButton.Pressed += AddNewHighScore;
        playerEdit.TextChanged += EnableButton;
        clearButton.Pressed += () => agreementPanel.Show();
    }
    public void EnableButton()
    {
        if (playerEdit.Text != null && playerEdit.Text != string.Empty && playerEdit.Text.Length < 10)
        {
            playerButton.Disabled = false;
        }
        else
        {
            playerButton.Disabled = true;
        }
    }
    public override void Quit()
    {
        ChangeScreen(ReturnScreen);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    private void AddNewHighScore()
    {
        string newName = playerEdit.Text;
        int HI = (int)MyGlobalSettings.FinalScore;
        if (records.ContainsKey(newName))
        {
            records[newName] = HI;
        }
        else
        {
            records.Remove(records.Last().Key);
            records.Add(newName, HI);
        }
        records = records.OrderByDescending(i => i.Value).ToDictionary(x => x.Key, y => y.Value);

        UpdateText(records);
        UpdateConfig(records);
        inputPanel.Visible = false;
    }
    public bool CheckHighScore()
    {
        foreach (string player in records.Keys)
        {
            if (MyGlobalSettings.FinalScore > records[player])
            {
                inputPanel.Visible = true;
                return true;
            }
        }
        return false;
    }

    public void UpdateText(Dictionary<string, int> updateRecords)
    {
        string playerText = string.Empty;
        string scoreText = string.Empty;

        for (int i = 0; i < updateRecords.Count; i++)
        {
            playerText += string.Join("", updateRecords.ElementAt(i).Key, "\n");
            scoreText += string.Join("", updateRecords.ElementAt(i).Value.ToString("0000000"), "\n");
        }

        scoreLabel.Text = scoreText;
        playerLabel.Text = playerText;
    }

    public void UpdateConfig(Dictionary<string, int> updateRecords)
    {
        for (int i = 0; i < updateRecords.Count; i++)
        {
            MyGlobalSettings.config.SetValue($"Player{i + 1}", "player_name", updateRecords.ElementAt(i).Key);
            MyGlobalSettings.config.SetValue($"Player{i + 1}", "best_score", updateRecords.ElementAt(i).Value);

        }
        MyGlobalSettings.config.Save("user://scores.cfg");
    }

}
