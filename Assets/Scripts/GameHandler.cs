using UnityEngine;

public class GameHandler : MonoBehaviour {
	[SerializeField] private GolemGrid grid = null;
	[SerializeField] private GameObject[] golemPrefabs = null;

	public void CreateGolem(Vector2 pos) {
		GameObject golem = Instantiate(golemPrefabs[Random.Range(0, golemPrefabs.Length)]);

		bool added = grid.AddEntity(golem.GetComponent<GridEntity>(), pos);

		if (!added) {
			Destroy(golem);
		}
	}

	public void Step() {
		grid.Step();
	}
}
