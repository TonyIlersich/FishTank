using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundAxis : MonoBehaviour
{
    public float rotationSpeed, rotationDirection, randomNumber;
    public bool rotateSelf, rotateAround, isRing;

    public Transform rotationAxis;

    public Vector3 randomDir;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomVector();
    }

    void GenerateRandomVector()
    {
        randomNumber = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateSelf)
        {
            SelfRotate();
        }

        if (rotateAround)
        {
            RotateAround();
        }

        if(isRing)
        {
            RingRotate();
        }
    }

    void SelfRotate()
    {
        transform.Rotate(0, rotationDirection * rotationSpeed * Time.deltaTime, 0);
    }

    void RotateAround()
    {
        Debug.Log("rotating");
        transform.RotateAround(rotationAxis.position, Vector3.up, rotationDirection * rotationSpeed * Time.deltaTime);
    }

    void RingRotate()
    {

        if (randomNumber == 0)
        {
            randomDir = new Vector3 (0,1,1);
        }

        if (randomNumber == 1)
        {
            //randomDir = Vector3.right;
            randomDir = new Vector3(1, 0, 1);
        }

        if (randomNumber == 2)
        {
            //randomDir = Vector3.forward;
            randomDir = new Vector3(1, 1, 0);
        }
        Debug.Log("Ring Rotating");
        transform.RotateAround(rotationAxis.position, rotationSpeed * randomDir, - rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
