using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TreatCounterController : MonoBehaviour {

	private Text treatCounterText;
	private int treatCount = 0;

	void Start () {
		treatCounterText = GetComponent<Text>();
		UpdateTreatText ();
	}
		
	public void OnTreatCollected()
	{
		treatCount += 1;
		UpdateTreatText ();
	}

	void UpdateTreatText()
	{
		treatCounterText.text = "Treats Collected: " + treatCount;
	}
}
