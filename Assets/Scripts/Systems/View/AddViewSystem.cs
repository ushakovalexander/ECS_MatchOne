using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddViewSystem : ReactiveSystem<GameEntity> {
	private Contexts _contexts;
	private Transform _viewContainer;

  public AddViewSystem(Contexts contexts) : base(contexts.game) {
		_contexts = contexts;
		_viewContainer = new GameObject("Views").transform;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (var entity in entities) {
			var asset = Resources.Load<GameObject>(entity.asset.name);
			GameObject gameObject = UnityEngine.Object.Instantiate(asset);
			if(gameObject != null) {
				gameObject.transform.SetParent(_viewContainer, false);
				entity.AddView(gameObject);
			}
		}
  }

  protected override bool Filter(GameEntity entity)   {
    return entity.hasAsset && !entity.hasView;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)   {
    return context.CreateCollector(GameMatcher.Asset);
  }
}
