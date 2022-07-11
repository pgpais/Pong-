using System;
using Godot;

public class TitleScreen : Control
{
    [Export]
    public PackedScene GameScene { get; private set; }
    [Export]
    public NodePath PlayButtonNodePath { get; private set; }
    [Export]
    public NodePath FadeInNodePath { get; private set; }

    private Button playButton;
    private FadeIn fadeIn;

    private PackedScene sceneToLoad;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playButton = GetNode<Button>(PlayButtonNodePath);
        fadeIn = GetNode<FadeIn>(FadeInNodePath);

        playButton.Connect("pressed", this, nameof(OnPlayButtonPressed));
        fadeIn.Connect(nameof(FadeIn.FadeFinished), this, nameof(OnFadeFinished));
    }

    private void OnPlayButtonPressed()
    {
        sceneToLoad = GameScene;
        fadeIn.PlayFadeIn();
    }

    private void OnFadeFinished()
    {
        GetTree().ChangeSceneTo(sceneToLoad);
    }
}
