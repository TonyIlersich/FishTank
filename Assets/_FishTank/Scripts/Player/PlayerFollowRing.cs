using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowRing : MonoBehaviour
{
	public LayerMask ringLayer;
    public Transform m_currentRing;

    void Start()
    {
    }

    void Update()
    {
		RaycastHit hitInfo;
		if (Physics.SphereCast(transform.position + Vector3.up*2, 1, Vector3.down, out hitInfo, Mathf.Infinity, ringLayer))
		{
			// assume the ring is the parent of whatever we are standing on
			transform.SetParent(hitInfo.transform.parent);
            m_currentRing = transform.parent;
        }
		else
		{
			transform.SetParent(null);
			
		}
    }

}
