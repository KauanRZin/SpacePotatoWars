using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export] public int health = 4;
	[Export] private float speed = 350f;
	[Export] public CollisionShape2D collider;
	[Export] public Node2D marker;
	[Export] public PackedScene ProjectileScene { get; set; }
	[Export] public AudioStreamPlayer2D ShootSound { get;set;}
	[Export] public AudioStreamPlayer2D DeadSound { get;set;}



	
	//public points points;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("shoot") && ProjectileScene != null)
        {
			GD.Print("Botão shoot pressionado!");
			if (ProjectileScene != null)
			{
				Shoot();
			}
			else
			{
				GD.PrintErr("Erro: ProjectileScene não foi arrastada no Inspector!");
			}
        }

		move();
	}


	public void move()
	{
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down"); //godot ja sabe
		
		if (direction != Vector2.Zero)
		{
			Velocity = direction * speed;
		}
		else
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, speed);
		}

		MoveAndSlide();
	}

	public void Shoot()
	{
		Projectile projectile = ProjectileScene.Instantiate<Projectile>(); 
		projectile.GlobalPosition = marker.GlobalPosition;
    	GetParent().AddChild(projectile);
		if (ShootSound != null)
		{
			ShootSound.Play(); // Dá o play no efeito sonoro
		}
	}


	public void TakeDamage()
	{
		health--;
		DeadSound.Play();
		ScoreManager.UpdateLifeUI();
		if (health <= 0)
		{
			ScoreManager.ResetPoints();
			GetTree().Quit(); 
		}
	}


}
