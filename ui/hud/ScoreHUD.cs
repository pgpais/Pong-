using System;
using Godot;

internal class ScoreHUD : Label
{
    public override void _Ready()
    {
        base._Ready();
        SetText(new int[] { 0, 0 });
    }

    internal void OnScoreChanged(int[] score)
    {
        SetText(score);
    }

    private void SetText(int[] score)
    {
        Text = $"{score[0]} : {score[1]}";
    }
}