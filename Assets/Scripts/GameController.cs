using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour {

	private Contexts _contexts;
	private Systems _systems;

	void Start () {
		_contexts = new Contexts();

		_systems = CreateSystems(_contexts);

		_systems.Initialize();
	}

	void Update () {
		_systems.Execute();
	}

	private Systems CreateSystems(Contexts contexts) {
		return new Feature("Game")

			.Add(new GameBoardSystem(contexts))
			.Add(new FallSystem(contexts))
			.Add(new FillSystem(contexts))

			.Add(new RemoveViewSystem(contexts))
			.Add(new AddViewSystem(contexts))
			.Add(new SetViewPositionSystem(contexts))
			;
	}
}
