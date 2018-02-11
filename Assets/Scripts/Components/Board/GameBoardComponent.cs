using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class GameBoardComponent : IComponent {
	public int rows;
	public int columns;
}
