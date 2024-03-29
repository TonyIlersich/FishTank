﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class OnPlayerEvent : UnityEvent { }
public class PlayerController : MonoBehaviour
{
	public enum PlayerTeam { _0, _1, _2, _3}
    public PlayerTeam m_playerTeam;

	public float m_movementSpeed;
	public float m_accelerationTime;

	public Transform m_turret;

	public float m_reelForce;
	public float m_shootForce;
	
	public Transform m_fishShootPosition;
	public float m_fishSnapDistance;

	public GameObject m_fishPrefab;

	public float m_recallTime;
	private float m_recallTimer;

	public float m_shootBufferTime;
	private float m_shootBufferTimer;

	[HideInInspector]
	public Rigidbody m_rigidbody;
	private Vector3 m_velocitySmoothing;
	private Vector2 m_movementInput;
	private Vector2 m_lookInput;
	private bool m_fishIsCast;
	private Rigidbody m_currentFishObject;
	private LineRenderer m_fishReel;

    [Header("Events")]
    public PlayerEvents m_playerEvents;
    [System.Serializable]
    public struct PlayerEvents
    {
        public OnPlayerEvent m_shootEvent;
    }

	private void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
		m_fishReel = GetComponentInChildren<LineRenderer>();
	}

	private void Update()
	{
		MovePlayer();
		Aim();

		if (m_fishIsCast)
		{
			CheckFishDistance();
		}

		if (!m_fishIsCast && m_shootBufferTimer < m_shootBufferTime)
		{
			m_shootBufferTimer += Time.deltaTime;
		}
	}

	public void SetMovementInput(Vector2 p_input)
	{
		m_movementInput = p_input;
	}

	public void SetLookInput(Vector2 p_input)
	{
		m_lookInput = p_input;
	}

	private void Aim()
	{
		float theta = Mathf.Atan2(m_lookInput.y, m_lookInput.x);
		float aimDegrees = theta * Mathf.Rad2Deg;
		Vector3 pCircle = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);

		if (m_lookInput.magnitude != 0f)
		{
			m_turret.rotation = Quaternion.AngleAxis(-aimDegrees, Vector3.up);
		}
	}

	private void MovePlayer()
	{
		float frameAccel = m_movementSpeed / m_accelerationTime * Time.deltaTime;

		Vector3 targetHorizontalVelocity = new Vector3(m_movementInput.x, 0, m_movementInput.y) * m_movementSpeed;
		Vector3 currentHorizontalVelocity = new Vector3(m_rigidbody.velocity.x, 0, m_rigidbody.velocity.z);
		Vector3 newHorizontalVelocity = Vector3.MoveTowards(currentHorizontalVelocity, targetHorizontalVelocity, frameAccel);

		m_rigidbody.velocity = new Vector3(newHorizontalVelocity.x, m_rigidbody.velocity.y, newHorizontalVelocity.z);
	}

	public void RightTrigger()
	{
		if (m_fishIsCast)
		{
			ReelFish();
		}
		else
		{
			if (m_shootBufferTimer >= m_shootBufferTime)
			{
				m_playerEvents.m_shootEvent.Invoke();
				ShootFish();

				m_shootBufferTimer = 0;
			}


		}
	}

	private void ShootFish()
	{
        
		m_currentFishObject = ObjectPooler.instance.NewObject(m_fishPrefab, m_fishShootPosition.position, Quaternion.identity).GetComponent<Rigidbody>();

        m_currentFishObject.GetComponent<ShotFish>().m_myTeam = m_playerTeam;

		m_currentFishObject.velocity = m_rigidbody.velocity;
		m_currentFishObject.AddForce(m_fishShootPosition.transform.up * m_shootForce, ForceMode.Impulse);

		m_fishIsCast = true;
	}

	
	private void ReelFish()
	{
		Vector3 dirToPlayer = transform.position - m_currentFishObject.position;
		float flopForce = dirToPlayer.y > -2 ? 200 : 0;
		m_currentFishObject.AddForce(dirToPlayer.normalized * m_reelForce + Vector3.up * flopForce, ForceMode.Impulse);
	}

	private void CheckFishDistance()
	{
		m_recallTimer += Time.deltaTime;

		m_fishReel.SetPosition(0, m_fishShootPosition.position);
		m_fishReel.SetPosition(1, m_currentFishObject.position);

		if (Vector3.Distance(transform.position, m_currentFishObject.transform.position) < m_fishSnapDistance && m_recallTimer >= m_recallTime)
		{
			RecallFish();
		}
	}

	public void RecallFish()
	{
		if (m_currentFishObject != null)
		{
			ObjectPooler.instance.ReturnToPool(m_currentFishObject.gameObject);
		}

		
		m_recallTimer = 0;

		m_fishReel.SetPosition(0, m_fishShootPosition.position);
		m_fishReel.SetPosition(1, m_fishShootPosition.position);

		m_fishIsCast = false;
	}
}
