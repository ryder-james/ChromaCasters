using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GolemGrid))]
public class GolemGridEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		GolemGrid grid = (GolemGrid) target;

		if (GUILayout.Button("Generate Grid")) {
			grid.UpdateGrid();
		}
	}
}
