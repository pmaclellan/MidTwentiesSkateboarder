using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	/* Public Variables */

	[Range(0.1f, 3.0f)]
	public float PushImpulse;

	[Range(0.0f, 3.14f)]
	public float TurnStepSize; //radians

	[Range(0.0f, 1.0f)]
	public float TurnDampingFactor; // [0.0-1.0]

	public float BigPushTime;

	[Range(0.0f, 2.0f)]
	public float MinTimeBetweenPushes; //time in seconds before you can push again

	public float MaxSpeed;

	/* Private Variables */

	private float PushTimer;
	private float LastPushTime = 0.0f;
	private Vector2 PreviousMovementDirection;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		PreviousMovementDirection = Vector2.up;
	}

	void Update() {
		if (rb2d.velocity.magnitude > 0) {
			float turnAngle = Vector2.SignedAngle (Vector2.up, rb2d.velocity);
			transform.rotation = Quaternion.AngleAxis(turnAngle, Vector3.forward);
		}
	}

	void FixedUpdate() {
		if (Input.GetButtonDown("Push"))
		{
			PushTimer = Time.time;
		} else if (Input.GetButtonUp("Push") && Time.time - LastPushTime > MinTimeBetweenPushes) {
			float pushForceMultiplier = 1.0f;
			//check for big push
			if (Time.time - PushTimer > BigPushTime) {
				pushForceMultiplier = 2.0f;
			}

			if (rb2d.velocity.magnitude > 0) {
				rb2d.AddForce (rb2d.velocity.normalized * PushImpulse * pushForceMultiplier, ForceMode2D.Impulse);
			} else {
				rb2d.AddForce (PreviousMovementDirection * PushImpulse * pushForceMultiplier, ForceMode2D.Impulse);
			}
			LastPushTime = Time.time;
		}

		float turnInput = Input.GetAxis ("Horizontal");
		if (turnInput != 0.0f) {
			if (rb2d.velocity.y > 0) {
				//find curretn direction of motion
				float theta = Mathf.Acos (rb2d.velocity.normalized.x);
				//calculate turn amount based on input and current velocity
				float newTheta = theta + (TurnStepSize * turnInput * rb2d.velocity.magnitude * -1.0f);
				Vector2 newUnitVelocity = new Vector2 (Mathf.Cos (newTheta), Mathf.Sin (newTheta));
				rb2d.velocity = newUnitVelocity * rb2d.velocity.magnitude * TurnDampingFactor;
			} else if (rb2d.velocity.y < 0) {
				float theta = Mathf.Acos (rb2d.velocity.normalized.x) * -1.0f;
				float newTheta = theta + (TurnStepSize * turnInput * rb2d.velocity.magnitude * -1.0f);
				Vector2 newUnitVelocity = new Vector2 (Mathf.Cos (newTheta), Mathf.Sin (newTheta));
				rb2d.velocity = newUnitVelocity * rb2d.velocity.magnitude * TurnDampingFactor;
			}
			//if velocity.y is 0, we don't want to turn
		}

		//store our movement direction so we can push the right way after stopping
		if (rb2d.velocity.magnitude > 0) {
			PreviousMovementDirection = rb2d.velocity.normalized;
		}

		//finally, let's clamp our speed
		rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, MaxSpeed);
	}
}
