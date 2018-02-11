using Entitas;
using UnityEngine;

public class EmitInputSystem : IExecuteSystem, ICleanupSystem {
  private Contexts _contexts;
  private IGroup<InputEntity> _inputs;

  public EmitInputSystem(Contexts contexts) {
    _contexts = contexts;
    _inputs = _contexts.input.GetGroup(InputMatcher.Input);
  }

  public void Execute() {
    var input = Input.GetMouseButtonDown(0);
    if (input) {
      var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100);
      if (hit.collider != null) {
        var hitPosition = hit.collider.transform.position;
        _contexts.input.CreateEntity().AddInput((int)hitPosition.x, (int)hitPosition.y);
      }
    }
  }

  //not usable at this version for some reasons TODO: check
  public void Cleanup() {
    foreach (var entity in _inputs.GetEntities()) {
        entity.Destroy();
    }
  }
}
