using System.Collections.Generic;
using UnityEngine;

public static class BoardLogic {

	public static int GetNextEmptyRow(GameContext context, Vector2 position) {
		position.y = -1;
		while(position.y >= 0 && GetEntitiesWithPositionCount(context, position) == 0) {
			position.y -= 1;
		}
		return (int)position.y + 1;
	}

	public static List<GameEntity> GetEntitiesWithPosition(GameContext context, Vector2 position) {
		List<GameEntity> result = new List<GameEntity>();
		var entities = context.GetEntities();
		foreach (var entity in entities) {
			if(entity != null && entity.position.value == position) {
				result.Add(entity);
			}
		}
		return result;
	}

	public static int GetEntitiesWithPositionCount(GameContext context, Vector2 position) {
		return GetEntitiesWithPosition(context, position).Count;
	}
}
