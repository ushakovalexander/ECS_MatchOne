using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class FallSystem : ReactiveSystem<GameEntity> {
	private Contexts _contexts;

  public FallSystem(Contexts contexts) : base(contexts.game) {
		_contexts = contexts;
  }

  protected override void Execute(List<GameEntity> entities) {
		var board = _contexts.game.gameBoard;

		for (int column = 0; column < board.columns; column++) {
			for (int row = 1; row < board.rows; row++) {
				var position = new Vector2(column, row);
				var entitiesWithPosition = BoardLogic.GetEntitiesWithPosition(_contexts.game, position);
				foreach (var entity in entitiesWithPosition) {
					if(entity != null && entity.isMovable) {
						MoveDown(entity, position);
					}
				}
			}
		}
  }

	private void MoveDown(GameEntity entity, Vector2 position) {
  	var nextRowPos = BoardLogic.GetNextEmptyRow(_contexts.game, position);
    if (nextRowPos != position.y) {
    	entity.ReplacePosition(new Vector2(position.x, nextRowPos));
    }
  }

  protected override bool Filter(GameEntity entity)   {
    return true;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)   {
    return context.CreateCollector(GameMatcher.GameBoardElement.Removed());
  }
}
