using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EntityGrid))]
public class EntityGridEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		EntityGrid grid = (EntityGrid) target;

		if (GUILayout.Button("Generate Grid")) {
			grid.UpdateGrid();
		}
	}
}
