using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInput : MonoBehaviour
{
	public int m_playerId;

	private PlayerController m_playerController;
	private Player m_playerInputController;

	private void Start()
	{
		m_playerController = GetComponent<PlayerController>();
		m_playerInputController = ReInput.players.GetPlayer(m_playerId);
	}

	private void Update()
	{
		Vector2 movementInput = new Vector2(m_playerInputController.GetAxis("MoveHorizontal"), m_playerInputController.GetAxis("MoveVertical"));
		m_playerController.SetMovementInput(movementInput);
		Vector2 aimInput = new Vector2(m_playerInputController.GetAxis("AimHorizontal"), m_playerInputController.GetAxis("AimVertical"));
		m_playerController.SetLookInput(aimInput);

		if (m_playerInputController.GetButtonDown("RightTrigger"))
		{
			m_playerController.RightTrigger();
		}
	}
}
