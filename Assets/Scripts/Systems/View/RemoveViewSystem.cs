using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RemoveViewSystem : ReactiveSystem<GameEntity> {
  private Contexts _contexts;

  public RemoveViewSystem(Contexts contexts) : base(contexts.game){
		_contexts = contexts;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (var entity in entities) {
      //TODO: destroy view
			entity.RemoveView();
		}
  }

  protected override bool Filter(GameEntity entity)   {
    return entity.hasView;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)   {
    return context.CreateCollector(GameMatcher.Asset.Removed(), GameMatcher.Destroyed.Added());
  }
}
