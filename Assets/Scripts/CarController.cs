using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	public float CarSpeed;

	void Update () {
		transform.Translate (Vector3.up * Time.deltaTime * CarSpeed);
	}
}
