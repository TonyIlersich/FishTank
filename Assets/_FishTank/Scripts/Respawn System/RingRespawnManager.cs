using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRespawnManager : MonoBehaviour
{
    
    public static RingRespawnManager Instance { get; private set; }
    public List<GameObject> m_rings;

    private void Awake()
    {
        Instance = this;
    }


    public Vector3 RespawnFishPosition(PlayerController p_player)
    {
        Transform currentFish = p_player.transform;
        GameObject respawnRing = null;
        foreach (GameObject ring in m_rings)
        {
            
            if (ring.transform == currentFish.transform.parent)
            {
                
                break;
            }
            respawnRing = ring;
            
        }
        if (respawnRing == null)
        {
            return m_rings[0].GetComponent<RingRespawnPositions>().GetSpawnPosition(p_player);
        }
        else
        {
            p_player.transform.parent = respawnRing.transform;
            return respawnRing.GetComponent<RingRespawnPositions>().GetSpawnPosition(p_player);
        }

    }
}
