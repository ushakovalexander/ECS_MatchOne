using UnityEngine;

public static class ContextExtensions {

    static readonly string[] _pieces = {
       "Piece0",
      "Piece1",
      "Piece2",
      "Piece3",
      "Piece4",
      "Piece5"
    };

    public static GameEntity CreateGameBoard(this GameContext context) {
        var entity = context.CreateEntity();
        entity.AddGameBoard(10, 10);
        return entity;
    }

    public static GameEntity CreateRandomPiece(this GameContext context, int x, int y) {
        var entity = context.CreateEntity();
        entity.isGameBoardElement = true;
        entity.isMovable = true;
        entity.isInteractive = true;
        entity.AddPosition(new Vector2(x, y));
        entity.AddAsset(_pieces[Random.Range(0, _pieces.Length)]);
        return entity;
    }

    public static GameEntity CreateBlocker(this GameContext context, int x, int y) {
        var entity = context.CreateEntity();
        entity.isGameBoardElement = true;
        entity.AddPosition(new Vector2(x, y));
        entity.AddAsset("Blocker");
        return entity;
    }
}
