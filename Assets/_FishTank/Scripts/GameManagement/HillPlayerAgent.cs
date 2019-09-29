using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillPlayerAgent : MonoBehaviour
{
    
    public PlayerController.PlayerTeam m_team;
    public float m_currentAmount;

    public UnityEngine.UI.Image m_bar;

    
    
    public void ChangeScore(float p_changeAmount)
    {
        m_currentAmount += p_changeAmount;
        if (m_currentAmount < 0)
        {
            m_currentAmount = 0;
        }
        else if (m_currentAmount > 100)
        {
            m_currentAmount = 100;
        }


    }
}
