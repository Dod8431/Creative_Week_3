using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {
		
	private Transform player;
	private Vector3 offset; 
	public float smoothSpeed = 0.125f;

	void Start () 
	{
		player = GameObject.Find ("Player").transform;
		offset = transform.position - player.transform.position;
	}
		
	void LateUpdate()
	{
		transform.SetPositionAndRotation (new Vector3 (Mathf.FloorToInt (transform.localPosition.x), transform.localPosition.y, (Mathf.FloorToInt (transform.localPosition.z * 1.155f) / 1.155f)), transform.localRotation);
	}

	void FixedUpdate()
	{
		Vector3 desiredPosition = player.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	}


}
