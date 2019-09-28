using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFlop : MonoBehaviour
{
    private Rigidbody m_rb;
    public List<AudioClip> m_flopSounds;
    private AudioSource m_aSource;
    public float m_torqueAdd;

    private void OnEnable()
    {
        if (m_rb == null)
        {
            m_rb = GetComponent<Rigidbody>();
            m_aSource = GetComponent<AudioSource>();
        }
        m_rb.AddTorque((Random.Range(-m_torqueAdd, m_torqueAdd)), (Random.Range(-m_torqueAdd, m_torqueAdd)), (Random.Range(-m_torqueAdd, m_torqueAdd)), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_aSource.Stop();
        m_aSource.clip = m_flopSounds[Random.Range(0, m_flopSounds.Count)];
        m_aSource.Play();
    }

}
