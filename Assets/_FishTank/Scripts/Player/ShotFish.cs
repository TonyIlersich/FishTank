using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFish : MonoBehaviour
{
    [HideInInspector]
    public PlayerController.PlayerTeam m_myTeam;

    public List<AudioClip> m_flopSounds;
    private AudioSource m_aSource;

	private void Start()
	{
		m_aSource = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter(Collision collision)
    {
        m_aSource.Stop();
        m_aSource.clip = m_flopSounds[Random.Range(0, m_flopSounds.Count)];
        m_aSource.Play();
    }
}
