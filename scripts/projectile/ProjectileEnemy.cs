using Godot;
using System;

public partial class ProjectileEnemy : Projectile
{
	[Export] private int speed = 300;

	// Called when the node enters the scene tree for the first time.
	public override void _PhysicsProcess(double delta){
	
		Position += Vector2.Down * speed * (float)delta;
	}

}
