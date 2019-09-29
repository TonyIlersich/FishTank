using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public Transform m_player;
    private void Awake()
    {
        transform.SetParent(null);
    }
    private void Update()
    {
        transform.position = new Vector3(m_player.position.x, transform.position.y, m_player.position.z);
    }
}
