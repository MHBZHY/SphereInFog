using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side {
	left,
	right,
	front,
	back
}

public interface PlayerControlDelegate {
	// called when player's position update
	void positionUpdate (Vector3 position);
	// player at side, need new block, give delegator the order of needed new block
	string playerIsAtSide (Side side, int needBlockOrder);
}