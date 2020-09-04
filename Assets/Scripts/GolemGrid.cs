using UnityEngine;

public class GolemGrid : MonoBehaviour {
	[SerializeField] private int rows = 8;
	[SerializeField] private int columns = 8;
	[SerializeField] private Vector2 offset = Vector2.zero;
	[SerializeField] private GridCell cellPrefab = null;

	private GridCell[,] grid;

	private void Start() {
		UpdateGrid();
	}

	public void UpdateGrid() {
		int childCount = transform.childCount;
		for (int i = childCount - 1; i >= 0; i--) {
			DestroyImmediate(transform.GetChild(i).gameObject);
		}
		grid = new GridCell[rows, columns];
		for (int row = 0; row < rows; row++) {
			for (int col = 0; col < columns; col++) {
				float x = col - columns * 0.5f;
				float y = rows * 0.5f - row;
				Vector3 pos = new Vector3(x, 0, y) + new Vector3(offset.x, 0, offset.y) + new Vector3(1, 0, -1);
				grid[row, col] = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
			}
		}
	}
}
