using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowRing : MonoBehaviour
{
	public LayerMask ringLayer;

    void Start()
    {
    }

    void Update()
    {
		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 10f, ringLayer))
		{
			// assume the ring is the parent of whatever we are standing on
			transform.SetParent(hitInfo.transform.parent);
		}
		else
		{
			transform.SetParent(null);
		}
    }
}
