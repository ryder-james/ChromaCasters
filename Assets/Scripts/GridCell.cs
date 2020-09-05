using UnityEngine;

public class GridCell : MonoBehaviour {
	public bool IsOccupied => Occupant != null;
	public bool HasGolem => Golem != null;

	public GridEntity Occupant {
		get => occupant;
		set { 
			occupant = value;
			if (occupant != null) {
				GolemBase golem = occupant.GetComponent<GolemBase>();
				if (golem != null) {
					Golem = golem;
				}
			}
		}
	}

	public GolemBase Golem { get; private set; }

	private GridEntity occupant;

	public GridEntity PassOccupant(GridCell receiver) {
		GridEntity prevOccupant = receiver.Occupant;

		receiver.occupant = Occupant;
		receiver.Golem = Golem;

		occupant = null;

		return prevOccupant;
	}

	public static GridEntity PassOccupant(GridCell from, GridCell to) {
		return from.PassOccupant(to);
	}
}
