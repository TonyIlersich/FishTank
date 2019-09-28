using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centerpiece : MonoBehaviour
{
	public Ring ring;

	private void OnTriggerEnter(Collider other)
	{
		GetComponent<Light>().enabled = true;
		ring.enabled = true;
	}
}
