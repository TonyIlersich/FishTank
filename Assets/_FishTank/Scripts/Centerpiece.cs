using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centerpiece : MonoBehaviour
{
	public Ring ring;

	private bool[] teamsReady = new bool[4] { false, false, false, false };

	private void OnTriggerEnter(Collider other)
	{
		int team = (int)other.GetComponent<ShotFish>().m_myTeam;
		teamsReady[team] = true;
		transform
			.GetChild(team)
			.GetComponent<Light>().enabled = true;
		bool allReady = true;
		for (int i = 0; i < 4; i++)
		{
			allReady &= teamsReady[i];
		}
		if (allReady)
		{
			ring.enabled = true;
		}
	}
}
