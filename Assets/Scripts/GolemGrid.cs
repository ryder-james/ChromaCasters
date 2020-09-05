using System.Collections.Generic;
using UnityEngine;

public class GolemGrid : MonoBehaviour {
	[SerializeField] private int rows = 8;
	[SerializeField] private int columns = 8;
	[SerializeField] private Vector2 offset = Vector2.zero;
	[SerializeField] private Transform cellParent = null;

	[Space]

	[SerializeField] private GridCell cellPrefab = null;

	private GridCell[,] grid;
	Dictionary<GridCell, GridCell> fromTo;

	private void Start() {
		fromTo = new Dictionary<GridCell, GridCell>();
		UpdateGrid();
	}

	public void Step() {
		List<GolemBase> stepped = new List<GolemBase>();

		for (int row = 0; row < rows; row++) {
			for (int col = 0; col < columns; col++) {
				GridCell cell = grid[row, col];
				if (cell.HasGolem) {
					GolemBase golem = cell.Golem;
					if (!stepped.Contains(golem)) {
						golem.DoTurn(null);
						stepped.Add(golem);
					}
				}
			}
		}

		foreach (GridCell from in fromTo.Keys) {
			GridCell to = fromTo[from];
			to.PassOccupant(from);
		}

		fromTo.Clear();
	}

	public bool AddEntity(GridEntity entity, Vector2 position) {
		int x = (int) position.x;
		int y = (int) position.y;

		if (!grid[x, y].IsOccupied) {
			grid[x, y].Occupant = entity;
			entity.transform.parent = transform;
			entity.transform.position = new Vector3(grid[x, y].transform.position.x, 0, grid[x, y].transform.position.z);
			if (grid[x, y].HasGolem) {
				grid[x, y].Golem.Grid = this;
			}
			return true;
		} else {
			return false;
		}
	}

	public bool CheckMove(GridEntity entity, Vector3 direction) {
		Vector2 pos = GetPosition(entity);

		if (pos == -Vector2.one) {
			return false;
		}

		Vector2 target = pos + new Vector2(direction.x, direction.z);

		if (IsInBounds(target) && fromTo.ContainsValue(grid[(int) target.x, (int) target.y])) {
			return false;
		}

		if (IsInBounds(target) && !grid[(int) target.x, (int) target.y].IsOccupied) {
			return true;
		} else {
			return false;
		}
	}

	private Vector2 GetPosition(GridEntity entity) {
		Vector2 pos = -Vector2.one;

		for (int row = 0; row < rows; row++) {
			for (int col = 0; col < columns; col++) {
				if (grid[row, col].Occupant == entity) {
					pos = new Vector2(row, col);
					break;
				}
			}

			if (pos != -Vector2.one) {
				break;
			}
		}

		return pos;
	}

	public void Move(GridEntity entity, Vector3 direction) {
		Vector2 pos = GetPosition(entity);
		Vector2 target = pos + new Vector2(direction.x, direction.z);
		//grid[(int) target.x, (int) target.y].PassOccupant(grid[(int) pos.x, (int) pos.y]);
		fromTo.Add(grid[(int) pos.x, (int) pos.y], grid[(int) target.x, (int) target.y]);

		entity.Move(new Vector3(direction.x, 0, -direction.z));
	}

	public void UpdateGrid() {
		Transform parent = cellParent != null ? cellParent : transform;
		int childCount = parent.childCount;
		for (int i = childCount - 1; i >= 0; i--) {
			DestroyImmediate(parent.GetChild(i).gameObject);
		}
		grid = new GridCell[rows, columns];
		for (int row = 0; row < rows; row++) {
			for (int col = 0; col < columns; col++) {
				float x = col - columns * 0.5f;
				float y = rows * 0.5f - row;
				Vector3 pos = new Vector3(x, 0, y) + new Vector3(offset.x, 0, offset.y) + new Vector3(1, 0, -1);
				grid[row, col] = Instantiate(cellPrefab, pos, Quaternion.identity, parent);
			}
		}
	}

	private bool IsInBounds(Vector2 point) {
		return point.x >= 0 && point.x < columns && point.y >= 0 && point.y < rows;
	}
}
