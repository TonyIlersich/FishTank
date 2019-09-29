using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameFinished : UnityEvent { }
public class WinAnimation : MonoBehaviour
{
    public Transform m_winningPlayer;
    private Camera mainCamera;
    private Vector3 m_cameraStart;
    public float m_cameraRotateSpeed;
    
    [Header("Lerp values")]
    public Transform m_cameraPosition;
    public AnimationCurve m_cameraLerp;
    public float m_lerpTime;
    public float m_displayTextTime;
    private bool m_winningTextShown;
    public UnityEngine.UI.Text m_winningText;

    public float m_orthoSizeTarget;
    private float m_orthoSizeBegin;
    public float m_showAnimationTime = 3;

    public GameFinished m_gameFinishedEvent;
    public GameFinished m_winAnimComplete;
    private float m_cameraEulerX;
    public float m_newEuler = 15f;


    private void Start()
    {
        mainCamera = Camera.main;
        m_cameraStart = mainCamera.transform.position;
        m_orthoSizeBegin = mainCamera.orthographicSize;
        m_cameraEulerX = mainCamera.transform.eulerAngles.x;
    }

    public void ShowWinAnimation(Transform p_winningPlayer, Transform p_newCameraPos)
    {
        m_cameraPosition = p_newCameraPos;
        m_winningPlayer = p_winningPlayer;
        StartCoroutine(WinAnimationCoroutine());
    }
    private IEnumerator WinAnimationCoroutine()
    {
        float timer = 0;
        m_winningPlayer.LookAt(m_winningPlayer.transform.position - Vector3.down);
        m_winningPlayer.GetComponent<Animator>().SetBool("Won", true);
        m_gameFinishedEvent.Invoke();
        while (timer < m_lerpTime)
        {
            timer += Time.deltaTime;
            float percent = m_cameraLerp.Evaluate(timer / m_lerpTime);
            mainCamera.transform.position = Vector3.Lerp(m_cameraStart, m_cameraPosition.position, percent);
            mainCamera.orthographicSize = Mathf.Lerp(m_orthoSizeBegin, m_orthoSizeTarget, percent);

            mainCamera.transform.eulerAngles = new Vector3(Mathf.Lerp(m_cameraEulerX, m_newEuler, percent),0,0);
            

            if (!m_winningTextShown)
            {
                if (timer > m_displayTextTime)
                {
                    m_winningTextShown = true;
                    m_winningText.gameObject.SetActive(true);
                }
            }

            yield return null;

        }
        yield return new WaitForSeconds(m_showAnimationTime);
        m_winAnimComplete.Invoke();

    }



}
 