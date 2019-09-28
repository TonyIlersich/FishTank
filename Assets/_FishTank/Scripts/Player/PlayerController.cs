using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public enum PlayerTeam { _0, _1, _2, _3}
    public PlayerTeam m_playerTeam;

	public float m_movementSpeed;
	public float m_accelerationTime;

	public Transform m_turret;

	[HideInInspector]
	public Rigidbody m_rigidbody;
	private Vector3 m_velocitySmoothing;
	private Vector2 m_movementInput;
	private Vector2 m_lookInput;

	private void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		MovePlayer();
		Aim();
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

		m_turret.rotation = Quaternion.AngleAxis(-aimDegrees, Vector3.up);

		/*
		if (m_gunnerAimInput.normalized.magnitude != 0)
		{
			m_crosshair.rotation = Quaternion.Euler(0, 0, aimDegrees);
			m_crosshair.position = m_shootPivotPoint.position + pCircle;
			m_lastPos = pCircle;
		}
		else
		{
			m_crosshair.position = m_shootPivotPoint.position + m_lastPos;
		}
		*/
	}

	private void MovePlayer()
	{
		Vector3 targetVelocity = new Vector3(m_movementInput.x, 0, m_movementInput.y) * m_movementSpeed;
		Vector3 setVelocity = Vector3.SmoothDamp(m_rigidbody.velocity, targetVelocity, ref m_velocitySmoothing, m_accelerationTime);
		m_rigidbody.velocity = new Vector3(setVelocity.x, m_rigidbody.velocity.y, setVelocity.z);
	}
}
