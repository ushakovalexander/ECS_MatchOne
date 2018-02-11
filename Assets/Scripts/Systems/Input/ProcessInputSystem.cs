using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class ProcessInputSystem : ReactiveSystem<InputEntity> {
  private Contexts _contexts;

  public ProcessInputSystem(Contexts contexts) : base(contexts.input) {
    _contexts = contexts;
  }

  protected override void Execute(List<InputEntity> entities) {
    var inputEntity = entities.SingleEntity();
    var input = inputEntity.input;

    var position = new Vector2(input.x, input.y);
		var entitiesWithPosition = BoardLogic.GetEntitiesWithPosition(_contexts.game, position);
    foreach (var entity in entitiesWithPosition) {
      if(entity != null && entity.isInteractive) {
        entity.isDestroyed = true;
      }
    }
    inputEntity.Destroy();
  }

  protected override bool Filter(InputEntity entity) {
    return entity.hasInput;
  }

  protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
    return context.CreateCollector(InputMatcher.Input);
  }

}
