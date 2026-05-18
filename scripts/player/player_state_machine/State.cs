using Godot;
using System;

//essa é a classe abstrata que usaremos para construir todos os estados do player, ela tem métodos para entrar, sair, atualizar, atualizar física e lidar com input

public abstract partial class State : Node
{
	public StateMachine StateMachine;
	public virtual void Enter()
	{
	}

	public virtual void Exit()
	{
	}

	public virtual void Update(double delta)
	{
	}

	public virtual void PhysicsUpdate(double delta)
	{
	}

	public virtual void HandleInput(InputEvent inputEvent)
	{
	}


}