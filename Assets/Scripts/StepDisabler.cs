using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StepDisabler : MonoBehaviour {
	[SerializeField] private GolemGrid grid = null;

	private Button button;

	private void Start() {
		button = GetComponent<Button>();
	}

	private void Update() {
		button.interactable = grid.MovingEntities == 0;
	}
}
