using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFlop : MonoBehaviour
{
    private Rigidbody m_rb;
    public List<AudioClip> m_flopSounds;
    private AudioSource m_aSource;
    public float m_torqueAdd;

    [Header("Scale Down")]
    public float m_startScaleTime;
    private SelfDestruct m_selfDestruct;
    private void OnEnable()
    {
        
        if (m_rb == null)
        {
            m_selfDestruct = GetComponent<SelfDestruct>();
            m_rb = GetComponent<Rigidbody>();
            m_aSource = GetComponent<AudioSource>();
        }
        StartCoroutine(ScaleDown());
        transform.localScale = Vector3.one;
        m_rb.AddTorque((Random.Range(-m_torqueAdd, m_torqueAdd)), (Random.Range(-m_torqueAdd, m_torqueAdd)), (Random.Range(-m_torqueAdd, m_torqueAdd)), ForceMode.Impulse);
    }

    private IEnumerator ScaleDown()
    {
        yield return new WaitForSeconds(m_startScaleTime);
        float scaleTime = Mathf.Abs(m_startScaleTime - m_selfDestruct.m_destructTime);
        float time = 0;
        while (time < m_selfDestruct.m_destructTime)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, time / scaleTime);
            yield return null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        m_aSource.Stop();
        m_aSource.clip = m_flopSounds[Random.Range(0, m_flopSounds.Count)];
        m_aSource.Play();
    }

}
