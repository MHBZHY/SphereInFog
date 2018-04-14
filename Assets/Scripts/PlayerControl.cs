using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float speed;
	public float sideDistanceControl;
	public string firstPrefabName;
	public string stageNum;

	public GameObject firstGroundBlock;

	private Rigidbody rb;
	private GameObject presentBlock;
	private string presentPrefabName;
	private GameObject newBlock;
	private int blockCounter = 0;
	private Side presentSide;

	private PlayerControlDelegate cusDelegate = null;

	private float anglesX = 0.0f;
	private float anglesY = 0.0f;

	struct SideDistance {
		public float left, right, front, back;
//		List<float> lst = new List<float> { left, right, front, back };
//
//		public bool isMin (float* num) {
//			lst.Sort ();
//			return num == &lst [0];
//		}
	}

	private SideDistance SD = new SideDistance ();

	void Start () {
		rb = GetComponent<Rigidbody> ();
		presentBlock = firstGroundBlock;
		presentPrefabName = firstPrefabName;
		newBlock = Instantiate (firstGroundBlock);
		newBlock.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
		cusDelegate = (PlayerControlDelegate)Assembly.GetExecutingAssembly().CreateInstance("Stage" + stageNum);
	}

	// move control
	void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Vector3 vec = new Vector3 (horizontal, 0.0f, vertical);
		rb.AddForce (vec * speed);
	}

	// update per frame
	void Update () {
		SD.left = presentBlock.transform.position.x - presentBlock.transform.localScale.x / 2 - transform.position.x;
		SD.right = presentBlock.transform.position.x + presentBlock.transform.localScale.x / 2 - transform.position.x;
		SD.front = presentBlock.transform.position.z + presentBlock.transform.localScale.z / 2 - transform.position.z;
		SD.back = presentBlock.transform.position.z - presentBlock.transform.localScale.z / 2 - transform.position.z;

		if (cusDelegate != null) {
			// tell delegate position has updated
			cusDelegate.positionUpdate (transform.position);

			// if player has at side
			string newPrefabName = null;
//			if (SD.front < 1 && SD.isMin(&SD.front)) {
			if (SD.front < sideDistanceControl) {
				presentSide = Side.front;
				newPrefabName = cusDelegate.playerIsAtSide (presentSide, blockCounter);
//			} else if (SD.right < 1 && SD.isMin(&SD.right)) {
			} else if (SD.right < sideDistanceControl) {
				presentSide = Side.right;
				newPrefabName = cusDelegate.playerIsAtSide (presentSide, blockCounter);
//			} else if (SD.left > -1 && SD.isMin(&SD.left)) {
			} else if (SD.left > -sideDistanceControl) {
				presentSide = Side.left;
				newPrefabName = cusDelegate.playerIsAtSide (presentSide, blockCounter);
//			} else if (SD.back > -1 && SD.isMin(&SD.back)) {
			} else if (SD.back > -sideDistanceControl) {
				presentSide = Side.back;
				newPrefabName = cusDelegate.playerIsAtSide (presentSide, blockCounter);
			} else {
				return;
			}

			if (newPrefabName == null) {
				return;
			}

			// create new block and place it
			if (newPrefabName != presentPrefabName) {
				Destroy (newBlock);		// destroy the cache block
				newBlock = (GameObject)Resources.Load ("Prefabs/" + newPrefabName);
				presentPrefabName = newPrefabName;
			}
			switch (presentSide) {
			case Side.front:
				newBlock.transform.position = new Vector3 (
					presentBlock.transform.position.x,
					presentBlock.transform.position.y,
					presentBlock.transform.position.z + presentBlock.transform.localScale.z
				);
				break;
			case Side.back:
				newBlock.transform.position = new Vector3 (
					presentBlock.transform.position.x,
					presentBlock.transform.position.y,
					presentBlock.transform.position.z - presentBlock.transform.localScale.z
				);
				break;
			case Side.left:
				newBlock.transform.position = new Vector3 (
					presentBlock.transform.position.x - presentBlock.transform.localScale.x,
					presentBlock.transform.position.y,
					presentBlock.transform.position.z
				);
				break;
			case Side.right:
				newBlock.transform.position = new Vector3 (
					presentBlock.transform.position.x + presentBlock.transform.localScale.x,
					presentBlock.transform.position.y,
					presentBlock.transform.position.z
				);
				break;
			default:
				break;
			}
			newBlock.transform.localScale = presentBlock.transform.localScale;

			// if need remove old block
			if (presentSide == Side.front && SD.front < -sideDistanceControl
				|| presentSide == Side.back && SD.back > sideDistanceControl
				|| presentSide == Side.right && SD.right < -sideDistanceControl
				|| presentSide == Side.left && SD.left > sideDistanceControl) {
				hideOldBlock ();
			}
		} else {
			// no delegate has pointed
			ReplaceNewGroundBlock ();
		}
	}

	// default forward generator
	private void ReplaceNewGroundBlock () {
		if (presentBlock.transform.position.z + presentBlock.transform.localScale.z / 2 - transform.position.z < sideDistanceControl) {
			newBlock.transform.position = new Vector3 (presentBlock.transform.position.x, presentBlock.transform.position.y, presentBlock.transform.position.z + presentBlock.transform.localScale.z);
			newBlock.transform.localScale = presentBlock.transform.localScale;
		}
		if (newBlock && presentBlock.transform.position.z + presentBlock.transform.localScale.z / 2 - transform.position.z < -sideDistanceControl) {
			hideOldBlock ();
		}
	}

	// hide old block
	private void hideOldBlock () {
		presentBlock.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
		var block = newBlock;
		newBlock = presentBlock;
		presentBlock = block;
	}
}
