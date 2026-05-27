using Godot;
using System;

public partial class Projectile : Area2D
{
	[Export] private int speed = 450;
	[Export] public CollisionShape2D Collider;
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta){
	
		Position += Vector2.Up * speed * (float)delta;
	}

	public void OnBodyEntered(Node2D body)
    {
		GD.Print($"BANG! Bateu em: {body.Name}");
        // Se o inimigo tiver a função TakeDamage, aplica o dano nele
		if (body.HasMethod("TakeDamage"))
		{
			body.Call("TakeDamage");
		}
		QueueFree(); 
    }

	public void OnAreaEntered(Area2D area)
	{
		GD.Print($"BANG! Bateu na área: {area.Name}");
		QueueFree();
	}



}
