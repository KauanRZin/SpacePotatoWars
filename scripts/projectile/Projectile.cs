using Godot;
using System;

public partial class Projectile : Area2D
{
	[Export] private int speed = 400;
	[Export] public CollisionShape2D Collider;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Position += Transform.Y * -1 *speed * (float)delta;
	}

	 private void _on_body_entered(Node2D body)
    {
        QueueFree(); // Remove o projétil da memória
    }



}
