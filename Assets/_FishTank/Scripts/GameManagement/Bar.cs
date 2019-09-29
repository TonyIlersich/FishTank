using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public Transform m_player;
    public float m_height = 15.5f;
    private void Awake()
    {
        transform.SetParent(null);
    }
    private void Update()
    {
        transform.position = new Vector3(m_player.position.x, m_height, m_player.position.z);
    }
}
