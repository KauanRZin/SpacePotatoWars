using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export] public float Speed { get; set; } = 150f;
    [Export] public PackedScene EnemyProjectileScene { get; set; }
	[Export] private Node2D marker;
	[Export] public PackedScene ProjectileScene { get; set; }


	private float shootTimer = 0f;
    private float shootInterval = 2.0f; // Atira a cada 2 segundos
    private int currentWave = 1;
    private float timeActive = 0f;


	private float targetYPosition = 150f;

	public void SetupEnemy(int wave)
    {
        currentWave = wave;
        shootTimer = (float)GD.RandRange(0.0, 1.5);
        targetYPosition = (float)GD.RandRange(80.0, 220.0);
    }
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timeActive += (float)delta;
        Vector2 velocity = Vector2.Zero;

       
        if (GlobalPosition.Y < targetYPosition)
        {
            velocity.Y = Speed;
        }
        else
        {
            velocity.Y = 0;

            if (currentWave >= 3)
            {
                velocity.X = Mathf.Sin(timeActive * 3f) * Speed;
            }
        }
		Velocity = velocity;
        MoveAndSlide();

		shootTimer += (float)delta;
        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            EnemyShoot();
        }
	}

	private void EnemyShoot()
    {
        if (ProjectileScene == null) return;

        Projectile projectile = ProjectileScene.Instantiate<Projectile>();
        
        // Posiciona o projétil no inimigo
        projectile.GlobalTransform = marker.GlobalTransform;
        // Rotaciona o projétil para baixo (180 graus ou PI radianos) para ele ir na direção do Player
        projectile.GlobalRotation = Mathf.Pi; 
        GetParent().CallDeferred(Node.MethodName.AddChild, projectile);
    }

	 // Método para ser chamado quando o tiro do player atingir este inimigo
    public void TakeDamage()
    {
        QueueFree(); // Destrói o inimigo
    }

	 
}
