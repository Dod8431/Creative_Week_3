using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour {

	void LateUpdate()
	{
		transform.SetPositionAndRotation (new Vector3 (Mathf.FloorToInt (transform.localPosition.x), transform.localPosition.y, (Mathf.FloorToInt (transform.localPosition.z * 1.155f) / 1.155f)), transform.localRotation);
	}
}
