using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameComplete : UnityEvent {}

public class Hill : MonoBehaviour
{
    private List<PlayerController> m_playersInHill = new List<PlayerController>();
    public List<PlayerController> m_allPlayers;

    public float m_maxBarAmount = 100;
    public float m_hillIncreaseRPS = 10f, m_hillDecreaseRPS;
    public float m_hillCheckRate = .3f;
    private float m_timer;

    public HillPlayerAgent[] m_playersBars = new HillPlayerAgent[4];

    

    public GameComplete m_gameCompleteEvent;


    public LayerMask m_playerLayer;

    public float m_hillRadius;

    public WinAnimation m_winAnim;
    private bool m_gameComplete = false;
    private HillPlayerAgent m_winningPlayer;
    private void Update()
    {
        ShowPlayerBars();   
        if (!m_gameComplete)
        {
            CollectPlayersInHill();
            HillTimer();
            
        }

    }

    private void CollectPlayersInHill()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, m_hillRadius, m_playerLayer);
        float m_fishInHill = 0;
        m_playersInHill.Clear();
        foreach (Collider col in cols)
        {
            m_playersInHill.Add(col.GetComponentInParent<PlayerController>());
            m_fishInHill++;
        }
    }

    private void HillTimer()
    {
        if (m_timer > m_hillCheckRate)
        {
            m_timer = 0;
            CalculateScore();
        }

        m_timer += Time.deltaTime;
    }

    private void CalculateScore()
    {
        if (m_playersInHill.Count == 1)
        {
            foreach (HillPlayerAgent currentPlayerStats in m_playersBars)
            {
                if (currentPlayerStats.m_team == m_playersInHill[0].m_playerTeam)
                {
                    
                    currentPlayerStats.ChangeScore(m_hillIncreaseRPS* m_hillCheckRate);
                    if (currentPlayerStats.m_currentAmount >= m_maxBarAmount)
                    {
                        m_winningPlayer = currentPlayerStats;
                        WinGame();
                    }
                }
                else
                {
                    currentPlayerStats.ChangeScore(m_hillDecreaseRPS);
                }
            }
        }
        else
        {
            foreach (PlayerController allPlays in m_allPlayers)
            {
                if (!m_allPlayers.Contains(allPlays))
                {
                    foreach(HillPlayerAgent currentPlayerStats in m_playersBars)
                    {
                        if(currentPlayerStats.m_team == allPlays.m_playerTeam)
                        {
                            currentPlayerStats.ChangeScore(m_hillDecreaseRPS);
                        }
                    }
                }
            }
        }

        
    }
    private void WinGame()
    {
        m_gameComplete = true;

        foreach (PlayerController player in m_allPlayers)
        {
            player.enabled = false;
        }

        m_gameCompleteEvent.Invoke();
        if (m_winAnim!=null)
        {
            m_winAnim.ShowWinAnimation(m_winningPlayer.transform, m_winningPlayer.m_winningCameraPos);
        }
    }

    private void ShowPlayerBars()
    {
        foreach (HillPlayerAgent playerBarStats in m_playersBars)
        {
            playerBarStats.m_bar.fillAmount = playerBarStats.m_currentAmount / m_maxBarAmount;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_hillRadius);
    }
}
