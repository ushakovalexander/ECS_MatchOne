using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class RemoveViewSystem : ReactiveSystem<GameEntity> {
  private Contexts _contexts;

  public RemoveViewSystem(Contexts contexts) : base(contexts.game){
    _contexts = contexts;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (var entity in entities) {
      DestroyView(entity.view);
      entity.RemoveView();
    }
  }

  protected override bool Filter(GameEntity entity)   {
    return entity.hasView;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)   {
    return context.CreateCollector(GameMatcher.Asset.Removed(), GameMatcher.Destroyed.Added());
  }

  private void DestroyView(ViewComponent viewComponent) {
    var gameObject = viewComponent.gameObject;
    var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    var color = spriteRenderer.color;
    color.a = 0f;
    spriteRenderer.material.DOColor(color, 0.2f);
    gameObject.transform
              .DOScale(Vector3.one * 1.5f, 0.2f)
              .OnComplete(() => {
                  Object.Destroy(gameObject);
              });
  }
}
