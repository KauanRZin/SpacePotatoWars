using Godot;
using System;
using System.Collections.Generic;

//classe que vai controlar os estados do player, ela tem um estado atual e métodos para mudar de estado, atualizar o estado atual e lidar com input

public partial class StateMachine : Node
{
	
	[Export] public State InitialState; //estado inicial do player, será o estado IdleState
	[Export] public Label StateLabel;
	public State CurrentState;
	public Dictionary<string, State> States = new(); //dicionário para armazenamento dos estados do player

	//método de inicialização da state machine; percorre e armazena todos os estados disponiveis do jogador
	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			if (child is State st)
			{
				States[st.Name.ToString().ToLower()] = st;
				st.StateMachine = this;
				GD.Print($"Estado armazenado: {child.Name}, com sucesso!");
			}
			else
			{
				GD.Print($"Nó ignorado: {child.Name} (Não herdou de State ou não buildou)");
			}
		}
	
		//aqui, se um estado é válido, ele pode ser alterado caso haja input (ativar a func ChangeState())
		if (InitialState != null)
		{
			ChangeState(InitialState.Name.ToString().ToLower());
		}

		//if (StateLabel != null) StateLabel.Text = "Estado: " + CurrentState?.Name;

		//verificação de hit no player (escutador de sinal)
			var player = GetParent<Player>();
			player.Hit += (sourcePosition) => OnPlayerHit(sourcePosition);
	}

	

	//para logica de timer e animações
	public override void _Process(double delta)
	{
		if (CurrentState != null)
		{
			CurrentState.Update(delta);
		}
	
	}

	//para logica aplicada em fisica
    public override void _PhysicsProcess(double delta)
	{
		if (CurrentState != null)
		{
			CurrentState.PhysicsUpdate(delta);
		}

		if (StateLabel != null) StateLabel.Text = CurrentState?.Name;
	}

	//para logica relacionada à inputs no teclado/mouse/controle/...
	public override void _Input(InputEvent @event)
	{
		if (CurrentState != null)
		{
			CurrentState.HandleInput(@event);
		}
	}

	public void ChangeState (string NewStateName)
	{
		if (CurrentState != null)
		{
			CurrentState.Exit();
		}
		
		//variavel de acesso para um novo estado
		var key = NewStateName.ToLower();

		//verificação de erro 
		if (States.TryGetValue(key, out var newState))
		{
			CurrentState = newState;
		}
		else
		{
			GD.PushWarning($"State '{key}' não registrado."); //(debug)
		}

		//se o estado for válido, ele pode ser definido como novo estado e chamar a função de entrada
		if (CurrentState != null)
		{
			CurrentState.Enter();
			//coloquei isso, pq o debug, quando entrava em attack, sumia mt rapido
			GD.Print($"Trocando de Estado :  {NewStateName}");
		}
	}


}