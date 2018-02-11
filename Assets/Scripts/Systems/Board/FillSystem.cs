using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class FillSystem : ReactiveSystem<GameEntity>
{
 	private Contexts _contexts;

  public FillSystem(Contexts contexts) : base(contexts.game) {
		_contexts = contexts;
  }

  protected override void Execute(List<GameEntity> entities) {
    var board = _contexts.game.gameBoard;
		for (int column = 0; column < board.columns; column++) {
			var position = new Vector2(column, board.rows);
			var nextRowPosition = BoardLogic.GetNextEmptyRow(_contexts.game, position);
			while (nextRowPosition != board.rows) {
				ContextExtensions.CreateRandomPiece(_contexts.game, column, nextRowPosition);
				nextRowPosition = BoardLogic.GetNextEmptyRow(_contexts.game, position);
			}
		}
  }

  protected override bool Filter(GameEntity entity) {
    return true;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.GameBoardElement.Removed());
  }
}
