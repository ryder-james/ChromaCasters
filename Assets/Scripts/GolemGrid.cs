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
						GridCell[,] neighbors = GetNeighbors(new Vector2(col, row));
						golem.DoTurn(ref neighbors);
						stepped.Add(golem);
					}
				}
			}
		}

		foreach (GridCell from in fromTo.Keys) {
			GridCell to = fromTo[from];
			from.PassOccupant(to);
		}

		fromTo.Clear();
	}

	public bool AddEntity(GridEntity entity, Vector2 position) {
		int row = (int) position.y;
		int col = (int) position.x;

		if (!grid[row, col].IsOccupied) {
			grid[row, col].Occupant = entity;
			entity.transform.parent = transform;
			entity.transform.position = new Vector3(grid[row, col].transform.position.x, 0, grid[row, col].transform.position.z);
			if (grid[row, col].HasGolem) {
				grid[row, col].Golem.Grid = this;
			}
			return true;
		} else {
			return false;
		}
	}

	public void Move(GridEntity entity, Vector3 direction) {
		Vector2 pos = GetPosition(entity);
		Vector2 target = pos + new Vector2(direction.x, -direction.z);
		fromTo.Add(grid[(int) pos.y, (int) pos.x], grid[(int) target.y, (int) target.x]);

		entity.Move(new Vector3(direction.x, 0, direction.z));
	}

	public GridCell[,] GetNeighbors(GridEntity entity) {
		return GetNeighbors(GetPosition(entity));
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
				grid[row, col].name = $"{col}, {row}";
			}
		}
	}

	private GridCell[,] GetNeighbors(Vector2 point) {
		GridCell[,] neighbors = new GridCell[3, 3];

		for (int row = (int)point.y - 1, relRow = 0; relRow < 3; row++, relRow++) {
			for (int col = (int)point.x - 1, relCol = 0; relCol < 3; col++, relCol++) {
				if (IsInBounds(new Vector2(col, row))) {
					neighbors[relRow, relCol] = grid[row, col];
				} else {
					neighbors[relRow, relCol] = null;
				}
			}
		}

		return neighbors;
	}

	private bool IsInBounds(Vector2 point) {
		return point.x >= 0 && point.x < columns && point.y >= 0 && point.y < rows;
	}

	private Vector2 GetPosition(GridEntity entity) {
		Vector2 pos = -Vector2.one;

		for (int row = 0; row < rows; row++) {
			for (int col = 0; col < columns; col++) {
				if (grid[row, col].Occupant == entity) {
					pos = new Vector2(col, row);
					break;
				}
			}

			if (pos != -Vector2.one) {
				break;
			}
		}

		return pos;
	}
}
