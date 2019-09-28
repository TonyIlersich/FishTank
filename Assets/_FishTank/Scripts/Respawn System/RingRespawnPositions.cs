using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRespawnPositions : MonoBehaviour
{
    public List<SpawnPosition> m_spawnPositions;

    [System.Serializable]
    public struct SpawnPosition
    {
        public PlayerController.PlayerTeam m_playerType;
        public Transform m_fishSpawnPostion;
    }


    public Vector3 GetSpawnPosition(PlayerController p_newFish)
    {
        Vector3 m_testSpawnPos = new Vector3();
        
        foreach (SpawnPosition pos in m_spawnPositions)
        {
            if (pos.m_playerType == p_newFish.m_playerTeam)
            {
                return pos.m_fishSpawnPostion.position;
            }

        }
        return m_testSpawnPos;
    }


}
