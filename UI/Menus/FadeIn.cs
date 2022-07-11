using System;
using Godot;

public class FadeIn : ColorRect
{
    [Signal]
    public delegate void FadeFinished();

    [Export]
    public NodePath AnimPlayerNodePath { get; private set; }

    private AnimationPlayer animPlayer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animPlayer = GetNode<AnimationPlayer>(AnimPlayerNodePath);
        animPlayer.Connect("animation_finished", this, nameof(OnFadeFinished));
    }

    private void OnFadeFinished(string animationName)
    {
        EmitSignal(nameof(FadeFinished));
    }

    public void PlayFadeIn()
    {
        Show();
        animPlayer.Play("fade_in");
    }
}
