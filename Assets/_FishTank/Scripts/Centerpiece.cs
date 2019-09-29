using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centerpiece : MonoBehaviour
{
	public Ring ring;
    public string m_sceneName;

	private readonly bool[] teamsReady = new bool[4] { false, false, false, false };
	private readonly bool[] teamsInside = new bool[4] { false, false, false, false };

	private void OnTriggerEnter(Collider other)
	{
		ShotFish shotFish = other.GetComponentInParent<ShotFish>();
		PlayerInput playerInput = other.GetComponentInParent<PlayerInput>();
		if (shotFish)
		{
			int team = (int)shotFish.m_myTeam;
			teamsReady[team] = true;
			transform
				.GetChild(team)
				.gameObject
				.SetActive(true);
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
		else if (playerInput)
		{
			int team = playerInput.m_playerId;
			teamsInside[team] = true;
			bool allInside = true;
			for (int i = 0; i < 4; i++)
			{
				allInside &= teamsInside[i];
			}
			if (allInside)
			{
                UnityEngine.SceneManagement.SceneManager.LoadScene(m_sceneName);
			}
		}
	}
}
