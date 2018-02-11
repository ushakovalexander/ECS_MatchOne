using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DestroySystem : ReactiveSystem<GameEntity> {
  private Contexts _contexts;

  public DestroySystem(Contexts contexts) : base(contexts.game){
		_contexts = contexts;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (var entity in entities) {
			entity.Destroy();
		}
  }

  protected override bool Filter(GameEntity entity)   {
    return entity.isEnabled;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)   {
    return context.CreateCollector(GameMatcher.Destroyed);
  }
}
