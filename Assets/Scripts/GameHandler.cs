using System.Collections;
using UnityEngine;

public class GameHandler : MonoBehaviour {
	[SerializeField] private EntityGrid grid = null;
	[SerializeField] private GameObject[] golemLenses = null;
	[SerializeField] private GameObject[] golemPrefabs = null;

	private int SelectedGolem { get; set; } = 0;

	public void CreateGolem(Vector2 pos) {
		if (grid.MovingEntities > 0) {
			return;
		}

		GameObject golem = Instantiate(golemPrefabs[SelectedGolem]);

		bool added = grid.AddEntity(golem.GetComponent<GridEntity>(), pos);

		if (!added) {
			Destroy(golem);
		} else {
			Step();
		}
	}

	public void SelectGolem(int golemIndex) {
		SelectedGolem = golemIndex;
		for (int i = 0; i < golemLenses.Length; i++) {
			golemLenses[i].SetActive(i != SelectedGolem);
		}
	}

	public void Step() {
		grid.Step();
	}
}
