using UnityEngine;

[RequireComponent(typeof(GridEntity))]
public abstract class GolemBase : MonoBehaviour {
	[SerializeField] protected int cellsPerMove = 1;
	[SerializeField] protected int attackRange = 1;
	[SerializeField] protected float damage = 1;
	[SerializeField] protected float health = 3;
	[SerializeField] protected bool canMoveDiagonally = false;
	[SerializeField] protected bool canAttackDiagonally = false;

	[Space]

	[SerializeField] private Material material = null;

	protected GridEntity entity;

	protected virtual void Awake() {
		GetComponentInChildren<Renderer>().material = material;
		entity = GetComponent<GridEntity>();
	}

	protected bool CheckEmpty(ref GridCell[,] neighbors, Vector3 direction) {
		Vector2 dir = new Vector2(direction.x, -direction.z) + Vector2.one;
		int row = (int) dir.y;
		int col = (int) dir.x;
		return neighbors[row, col] != null && !neighbors[row, col].IsOccupied;
	}

	public abstract void DoTurn(ref GridCell[,] neighbors);
}
