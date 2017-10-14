using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject tether;
	public Vector3 offset;
	
	void LateUpdate () {
		transform.position = tether.transform.position + offset;
	}
}
