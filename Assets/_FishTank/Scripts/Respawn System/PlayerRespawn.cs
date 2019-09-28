using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnRespawnEvents : UnityEvent { };

public class PlayerRespawn : MonoBehaviour
{
    public string m_deathTag = "Death";
    public float m_repsawnTime = 3f;
    private Vector3 m_repsawnPos;
    public GameObject m_visuals;
    public GameObject m_deathParticle, m_respawnParticle;
    private ObjectPooler m_pooler;
    private RingRespawnManager m_respawnManager;
    private PlayerController m_playerController;
    private Coroutine m_respawnCoroutine;
    private Rigidbody m_rb;

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
        m_playerController.enabled = false;
        m_events.m_diedEvent.Invoke();
        m_visuals.SetActive(false);
        m_pooler.NewObject(m_deathParticle, transform.position, Quaternion.identity);

        transform.position = m_respawnManager.RespawnFishPosition(m_playerController);
        m_rb.isKinematic = true;
        m_respawnCoroutine =  StartCoroutine(RespawnFunction());

    }

    private IEnumerator RespawnFunction()
    {
        yield return new WaitForSeconds(m_repsawnTime);
        m_pooler.NewObject(m_respawnParticle, transform.position, Quaternion.identity);
        m_visuals.SetActive(true);
        m_playerController.enabled = true;
        m_rb.isKinematic = false;
        m_events.m_repsawnEvent.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == m_deathTag)
        {
            Die();
        }
    }


}
