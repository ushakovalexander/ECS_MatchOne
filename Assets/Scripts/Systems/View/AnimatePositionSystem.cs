using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimatePositionSystem : ReactiveSystem<GameEntity> {
  private Contexts _contexts;

  public AnimatePositionSystem(Contexts contexts) : base(contexts.game) {
    _contexts = contexts;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (var entity in entities) {
      var position = entity.position;
      var isTopRow = position.value.y == _contexts.game.gameBoard.rows - 1;
      if (isTopRow) {
        entity.view.gameObject.transform.localPosition = new Vector3(position.value.x, position.value.y + 1);
      }
      entity.view.gameObject.transform.DOMove(new Vector3(position.value.x, position.value.y, 0f), 0.3f);
    }
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasView && entity.hasPosition;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.Position);
  }
}
