using System;
using Godot;

public class TitleScreen : Control
{
    [Export]
    public PackedScene GameScene { get; private set; }
    [Export]
    public NodePath PlayButtonNodePath { get; private set; }
    [Export]
    public NodePath QuitButtonNodePath { get; private set; }
    [Export]
    public NodePath FadeInNodePath { get; private set; }

    private Button playButton;
    private Button quitButton;
    private FadeIn fadeIn;

    private PackedScene sceneToLoad;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playButton = GetNode<Button>(PlayButtonNodePath);
        quitButton = GetNode<Button>(QuitButtonNodePath);
        fadeIn = GetNode<FadeIn>(FadeInNodePath);

        playButton.GrabFocus();

        playButton.Connect("pressed", this, nameof(OnPlayButtonPressed));
        quitButton.Connect("pressed", this, nameof(OnQuitButtonPressed));
        fadeIn.Connect(nameof(FadeIn.FadeFinished), this, nameof(OnFadeFinished));
    }

    private void OnPlayButtonPressed()
    {
        sceneToLoad = GameScene;
        fadeIn.PlayFadeIn();
    }

    private void OnQuitButtonPressed()
    {
        QuitApplication();
    }

    private void QuitApplication()
    {
        GetTree().Quit();
    }

    private void OnFadeFinished()
    {
        GetTree().ChangeSceneTo(sceneToLoad);
    }
}
