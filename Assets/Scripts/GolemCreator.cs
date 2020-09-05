using UnityEngine;

public class GolemCreator : MonoBehaviour {
	[SerializeField] private GameHandler handler = null;

	private Camera mainCamera;

	private void Start() {
		mainCamera = Camera.main;
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit)) {
				if (hit.collider.CompareTag("Tile")) {
					GameObject tile = hit.collider.transform.parent.gameObject;
					int col = int.Parse(tile.name[0].ToString());
					int row = int.Parse(tile.name[tile.name.Length - 1].ToString());
					handler.CreateGolem(new Vector2(col, row));
				}
			}
		}
	}
}
