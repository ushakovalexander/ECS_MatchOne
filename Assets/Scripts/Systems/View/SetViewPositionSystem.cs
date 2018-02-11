using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SetViewPositionSystem : ReactiveSystem<GameEntity> {
  private Contexts _contexts;

  public SetViewPositionSystem(Contexts contexts) : base(contexts.game){
    _contexts = contexts;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (var entity in entities) {
      var asset = Resources.Load<GameObject>(entity.asset.name);
      var position = entity.position;
      entity.view.gameObject.transform.position = new Vector3(position.value.x, position.value.y, 0f);
    }
  }

  protected override bool Filter(GameEntity entity)   {
    return entity.hasView && entity.hasPosition;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)   {
    return context.CreateCollector(GameMatcher.View);
  }
}
