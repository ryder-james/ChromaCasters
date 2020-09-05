using UnityEngine;

public static class Direction {
	public static Vector3 Up => Vector3.forward;
	public static Vector3 Down => Vector3.back;
	public static Vector3 Left => Vector3.left;
	public static Vector3 Right => Vector3.right;

	public static Vector3 Random => All[UnityEngine.Random.Range(0, All.Length)];

	public static Vector3[] All = new Vector3[] { Up, Down, Left, Right };
}
