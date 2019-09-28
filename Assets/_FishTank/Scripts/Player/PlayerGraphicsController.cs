using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphicsController : MonoBehaviour
{
	public Transform m_wheel;

	private PlayerController m_playerController;

	private void Start()
	{
		m_playerController = GetComponent<PlayerController>();
	}

	private void Update()
	{
		TurnWheel();
	}

	private void TurnWheel()
	{
		if (m_playerController.m_rigidbody.velocity != Vector3.zero)
		{
			float angle = Mathf.Atan2(m_playerController.m_rigidbody.velocity.z, m_playerController.m_rigidbody.velocity.x) * Mathf.Rad2Deg;
			m_wheel.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
		}
	}
}
