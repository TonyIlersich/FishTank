using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
	// these properties can be fine-tuned in the inspector
	public bool reverse = false;
	public float stepInterval;
	public float stepDuration;
	public int stepsPerRotation;
	public float anticipationFactor = .05f;

	// these are used for adjustments
	private float startTime;
	private float startAngle;

	void Start()
	{
		startTime = Time.time;
		startAngle = transform.rotation.eulerAngles.y;
	}

	void Update()
	{
		UpdateAngle();
	}

	// rotates this gameobject periodically according to properties above
	void UpdateAngle()
	{
		float localTime = (Time.time - startTime);

		// calculate rotation of current step
		float step = Mathf.Floor(localTime / stepInterval % stepsPerRotation);
		float stepAngle = step / stepsPerRotation;

		// calculate the rotation due to transition
		float t = Mathf.Clamp(localTime % stepInterval, 0, stepDuration) / stepDuration;
		float transitionAngle = Smoothstep(t, anticipationFactor) / stepsPerRotation;

		// rotate
		float angle = (stepAngle + transitionAngle) * (reverse ? -1f : 1f);
		transform.rotation = Quaternion.Euler(0, angle * 360 + startAngle, 0);
	}

	// used to smoothly transition a value from 0 to 1 with an optional under-/over-shoot
	float Smoothstep(float x, float anticipationFactor = 0)
	{
		// this function is only defined for [0, 1]
		x = Mathf.Clamp01(x);

		// calculate base result bound in [0,1]
		float step = (Mathf.Sin(2 * Mathf.PI * (x + .5f))) / (2 * Mathf.PI) + x;

		// bias result with additional sine wave as anticipation
		return step + anticipationFactor * Mathf.Sin(2 * Mathf.PI * -x);
	}
}
