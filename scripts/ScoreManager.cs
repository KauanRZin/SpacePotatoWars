using Godot;
using System;

public partial class ScoreManager : Node
{
    [Export] public Label ScoreTable { get; set; }
    [Export] public Label LifeTable { get; set; }
    [Export] public AudioStreamPlayer2D ScoreSound { get;set;}
    [Export] public AudioStreamPlayer2D HighScoreSound { get;set;}
    [Export] public Player player;
    

    private int count = 0;
    private int life;

    private static int score = 0;
    private static int highScore = 0;
    private static ScoreManager instance;

    private const string SavePath = "user://highscore.save";

    public override void _Ready()
    {
        instance = this;

        if (player != null)
        {
            life = player.health; 
        }
        LoadHighScore();
        UpdateScoreUI();
        UpdateLifeUI();
    }

    public static void AddPoints(int pointsToAdd)
    {
        GD.Print($"mais {pointsToAdd}");
        score += pointsToAdd;
        instance.ScoreSound.Play();
        if(score > highScore){
            if(instance.count == 0)
                instance.HighScoreSound.Play();
            instance.count++;            
            highScore = score;
            instance?.SaveHighScore();
        }

        instance?.UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (ScoreTable != null)
        {
            ScoreTable.Text = $"Score: {score} | HighScore: {highScore}";
        }
    }
    
    public static void UpdateLifeUI()
    {
        if (instance != null && instance.LifeTable != null)
        {
            if (instance.player != null)
            {
                instance.life = instance.player.health; // Puxa a vida atual do Player
            }
            instance.LifeTable.Text = $"Life: {instance.life}";
        }
    }


    // Chame isso quando o jogador morrer e o jogo reiniciar
    public static void ResetPoints()
    {
        score = 0;
        instance?.UpdateScoreUI();

    }

    private void SaveHighScore()
	{
		ConfigFile config = new ConfigFile();
		config.SetValue("Dados", "HighScore", highScore);
		
		Error error = config.Save(SavePath);
		if (error != Error.Ok)
		{
			GD.PrintErr($"Erro ao salvar o recorde: {error}");
		}
	}

    private void LoadHighScore()
	{
		ConfigFile config = new ConfigFile();
		Error error = config.Load(SavePath);

		// Se o arquivo existir e abrir com sucesso, lê o valor
		if (error == Error.Ok)
		{
			// O número '0' no final é o valor padrão caso a chave não exista
			highScore = (int)config.GetValue("Dados", "HighScore", 0);
			GD.Print($"Recorde carregado com sucesso: {highScore}");
		}
		else
		{
			highScore = 0;
			GD.Print("Nenhum recorde anterior encontrado. Começando do zero.");
		}
	}

}
