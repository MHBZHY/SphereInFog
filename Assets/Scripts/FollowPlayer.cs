using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;

	public float distanceAway;			
	public float distanceUp;			
	public float smooth;				// how smooth the camera movement is

	private Vector3 offset;

	private Vector3 m_TargetPosition;
	private Transform pTransform;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		pTransform = player.transform;
	}

	void LateUpdate () {
		// setting the target position to be the correct offset from the 
		m_TargetPosition = pTransform.position + Vector3.up * distanceUp - pTransform.forward * distanceAway;

		// making a smooth transition between it's current position and the position it wants to be in
		transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);

		// make sure the camera is looking the right way!
		transform.LookAt(pTransform);
	}
	
//	// Update is called once per frame
//	void Update () {
//		transform.position = player.transform.position + offset;
//	}
}
