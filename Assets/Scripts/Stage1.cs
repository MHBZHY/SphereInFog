using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : PlayerControlDelegate {

	private List<Dictionary<string, string>> blockList;
	private Dictionary<string, Side> directionToSide;

	public Stage1 () {
		blockList = new List<Dictionary<string, string>> {
			new Dictionary<string, string> {
				{"name", "Basic_Square_Block"},
				{"side", "left"}
			},
			new Dictionary<string, string> {
				{"name", "Basic_Square_Block"},
				{"side", "front"}
			}
		};
		directionToSide = new Dictionary<string, Side> {
			{ "left", Side.left },
			{ "right", Side.right },
			{ "front", Side.front },
			{ "back", Side.back }
		};
	}
	
	// PlayerControlDelegate method
	// player at side, need new block
	public string playerIsAtSide (Side side, int needBlockOrder) {
//		Dictionary<string, string> dic = blockList [needBlockOrder];
//		if (directionToSide [dic ["side"]] == side) {
//			return dic ["name"];
//		}
		return "Basic_Square_Block";
	}

	public void positionUpdate (Vector3 position) {}
}
