using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreatPickupController : MonoBehaviour {

	public UnityEvent TreatCollected;

	[Range(10, 1000)]
	public int Value;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			TreatCollected.Invoke ();
			gameObject.SetActive (false);
		}
	}
}
