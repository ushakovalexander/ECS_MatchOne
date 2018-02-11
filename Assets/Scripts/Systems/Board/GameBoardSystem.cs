using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class GameBoardSystem : ReactiveSystem<GameEntity>, IInitializeSystem {

	private Contexts _contexts;
	private IGroup<GameEntity> _boardElements;

	public GameBoardSystem(Contexts contexts) : base(contexts.game) {
    _contexts = contexts;
		_boardElements = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.GameBoardElement, GameMatcher.Position));
  }

	public void Initialize() {
		var board = _contexts.game.CreateGameBoard().gameBoard;
		for (int row = 0; row < board.rows; row++) {
      for (int column = 0; column < board.columns; column++) {
        if (Random.value > 0.9f) {
    			_contexts.game.CreateBlocker(column, row);
        } else {
        	_contexts.game.CreateRandomPiece(column, row);
        }
			}
		}
	}

  protected override void Execute(List<GameEntity> entities) {
    var board = entities.SingleEntity().gameBoard;
		foreach (var entity in entities) {
			if(entity.position.value.x >= board.columns || entity.position.value.y >= board.rows) {
				entity.isDestroyed = true;
			}
		}
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasGameBoard && entity.hasPosition;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.GameBoard);
  }
}
