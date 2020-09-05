using UnityEngine;

public class RedGolem : GolemBase {
	public override void DoTurn(GridCell[,] neighbors) {
		for (int step = 0; step < cellsPerMove; step++) {
			int[] directionOrder = new int[] { 0, 1, 2, 3 };

			for (int i = 0; i < directionOrder.Length; i++) {
				int rand = Random.Range(0, directionOrder.Length);
				int temp = directionOrder[i];
				directionOrder[i] = directionOrder[rand];
				directionOrder[rand] = temp;
			}

			for (int i = 0; i < 4; i++) {
				Vector3 dir = Direction.All[directionOrder[i]];
				bool canMove = Grid.CheckMove(entity, dir);
				if (canMove) {
					Grid.Move(entity, dir);
					break;
				}
			}
		}
	}
}
