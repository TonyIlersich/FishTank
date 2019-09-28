using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnRespawnEvents : UnityEvent { };

public class PlayerRespawn : MonoBehaviour
{
    public string m_deathTag = "Death";
    public float m_respawnTime = 3f;
    public float m_respawnParticleStartTime;
    public GameObject m_visuals;
    public GameObject m_deathParticle, m_respawnParticle;
    private ObjectPooler m_pooler;
    private RingRespawnManager m_respawnManager;
    private PlayerController m_playerController;
    private Coroutine m_respawnCoroutine;
    private Rigidbody m_rb;
    private bool m_died;

    public RespawnEvents m_events;
    [System.Serializable]
    public struct RespawnEvents
    {
        public OnRespawnEvents m_diedEvent;
        public OnRespawnEvents m_repsawnEvent;
    }

    private void Start()
    {
        m_playerController = GetComponent<PlayerController>();
        m_pooler = ObjectPooler.instance;
        m_respawnManager = RingRespawnManager.Instance;
        m_rb = GetComponent<Rigidbody>();
    }

    public void Die()
    {
        if (!m_died)
        {

            m_died = true;
            m_playerController.enabled = false;
            m_events.m_diedEvent.Invoke();
            m_visuals.SetActive(false);
            m_pooler.NewObject(m_deathParticle, transform.position, Quaternion.identity);

            transform.position = m_respawnManager.RespawnFishPosition(m_playerController);
            m_rb.isKinematic = true;
            m_respawnCoroutine = StartCoroutine(RespawnFunction());
            print("died");
        }
    }

    private IEnumerator RespawnFunction()
    {
        
        float currentTimer = 0;
        bool respawnShowed = false;
        while (currentTimer < m_respawnTime)
        {
            currentTimer += Time.deltaTime;
            if (!respawnShowed)
            {
                if (currentTimer > m_respawnParticleStartTime)
                {
                    respawnShowed = true;
                    m_pooler.NewObject(m_respawnParticle, transform.position, Quaternion.identity);
                }
            }
            yield return null;
        }
        m_visuals.SetActive(true);
        m_playerController.enabled = true;
        m_rb.isKinematic = false;
        m_events.m_repsawnEvent.Invoke();
        m_died = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == m_deathTag)
        {
            Die();
        }
    }


}
