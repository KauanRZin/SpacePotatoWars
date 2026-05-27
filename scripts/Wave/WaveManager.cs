using Godot;
using System;

public partial class WaveManager : Node
{
	 [Export] public PackedScene EnemyScene { get; set; }
    
    // Defina os limites horizontais baseados na largura da sua imagem do planeta
    [Export] public float MinX { get; set; }  = 356.0f;
    [Export] public float MaxX { get; set; } = 680.0f;
    [Export] private float spawnY {get;set;} = -62f;

    private int currentWave = 0;
    private float waveTimer = 0f;
    private float timeBetweenWaves = 6f; // Nova horda a cada 6 segundos

    public override void _Ready()
    {
        StartNextWave();
    }

    public override void _Process(double delta)
    {
        waveTimer += (float)delta;
        if (waveTimer >= timeBetweenWaves)
        {
            waveTimer = 0f;
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        currentWave++;
        GD.Print($"--- Iniciando Horda {currentWave}! ---");

        // Define quantos inimigos vão surgir (aumenta a cada horda)
        int enemiesToSpawn = 4 + currentWave; 

        for (int i = 0; i < enemiesToSpawn; i++)
        {
			GD.Print($"PUF");
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (EnemyScene == null)
        {
            GD.PrintErr("Erro: EnemyScene não associada no WaveManager!");
            return;
        }

        Enemy enemyInstance = EnemyScene.Instantiate<Enemy>();

        // Sorteia uma posição X dentro dos limites da imagem do planeta
        float randomX = (float)GD.RandRange(MinX, MaxX);
        
        // Surge um pouco acima da tela (Y negativo) para aparecerem descendo
        
        enemyInstance.GlobalPosition = new Vector2(randomX, spawnY);
        
        // Passa a horda atual para o inimigo configurar seu comportamento
        enemyInstance.SetupEnemy(currentWave);

        GetParent().CallDeferred(Node.MethodName.AddChild, enemyInstance);
    }
}

