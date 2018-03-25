using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : PlayerControl {

	public GameObject firstGroundBlock;

	private GameObject presentBlock;
	private GameObject newBlock;

	// Use this for initialization
	void Start () {
		presentBlock = firstGroundBlock;
		newBlock = Instantiate (firstGroundBlock);
		newBlock.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (presentBlock.transform.position.z + presentBlock.transform.localScale.z / 2 - transform.position.z < 1) {
			ReplaceNewGroundBlock ();
		}
		if (newBlock && presentBlock.transform.position.z + presentBlock.transform.localScale.z / 2 - transform.position.z < -1) {
			presentBlock.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
			var block = newBlock;
			newBlock = presentBlock;
			presentBlock = block;
		}
	}

	private void ReplaceNewGroundBlock () {
		newBlock.transform.position = new Vector3 (presentBlock.transform.position.x, presentBlock.transform.position.y, presentBlock.transform.position.z + 5);
		newBlock.transform.localScale = presentBlock.transform.localScale;
	}
}
