using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] public Button StartButton { get; set; }
    [Export] public Button ExitButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartButton.Pressed += OnStartButtonPressed;
        ExitButton.Pressed += OnExitButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	 private void OnStartButtonPressed()
    {
        // Reseta os pontos na memória antes de começar
        ScoreManager.ResetPoints();
        
        // Altere para o caminho exato da cena principal do seu jogo!
        GetTree().ChangeSceneToFile("res://scenes/World.tscn"); 
    }
	 private void OnExitButtonPressed()
    {
        GetTree().Quit(); 
    }
}
