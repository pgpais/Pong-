using System;
using Godot;
using Pong.Entities.Managers;

public class HeadsUpDisplay : CanvasLayer
{
    [Export]
    public NodePath ScoreNodePath { get; private set; }

    private ScoreHUD scoreHUD;
    public override void _Ready()
    {
        base._Ready();
        scoreHUD = GetNode<ScoreHUD>(ScoreNodePath);
    }

    public void ConnectScoreSignal(ScoreManager scoreManager)
    {
        scoreManager.Connect(nameof(ScoreManager.ScoreChanged), scoreHUD, nameof(ScoreHUD.OnScoreChanged));
    }
}
