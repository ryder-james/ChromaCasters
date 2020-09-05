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
	[SerializeField] private GolemGrid grid = null;

	public GolemGrid Grid { get => grid; set => grid = value; }

	protected GridEntity entity;

	protected virtual void Start() {
		GetComponentInChildren<Renderer>().material = material;
		entity = GetComponent<GridEntity>();
	}

	public abstract void DoTurn(GridCell[,] neighbors);
}
