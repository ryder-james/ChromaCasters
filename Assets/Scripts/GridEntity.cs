using System.Collections;
using UnityEngine;

public class GridEntity : MonoBehaviour {
	[SerializeField] private float speed = 2;

	public GolemGrid Grid { get; set; }

	private bool isMoving = false;

	public void Move(Vector3 direction) {
		StartCoroutine(nameof(MoveAsync), direction);
	}

	private IEnumerator MoveAsync(Vector3 direction) {
		while (isMoving) {
			yield return null;
		}

		isMoving = true;
		Grid.MovingEntities++;
		Vector3 start = transform.position;
		Vector3 end = transform.position + direction;
		for (float t = 0; t < 1; t += Time.deltaTime * speed) {
			transform.position = Vector3.Lerp(start, end, t);
			yield return null;
		}
		transform.position = end;
		isMoving = false;
		Grid.MovingEntities--;
	}
}
