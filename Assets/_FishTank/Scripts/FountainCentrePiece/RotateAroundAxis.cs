using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundAxis : MonoBehaviour
{
    public float rotationSpeed, rotationDirection, randomNumber, randomDirection, tempo;
    public bool rotateSelf, rotateAround, isRing, rotateSelfCentre;
    
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

        if (rotateSelfCentre)
        {
            SelfRotateCentre();
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

    void SelfRotateCentre()
    {
        float apparentTime = Time.time * tempo;
        float apparentDeltaTime = Time.deltaTime * tempo;
        float value = 5f * Mathf.Cos(apparentTime); //* Mathf.Sin(Time.time);
        //Debug.Log("Value" + value);
        transform.Rotate(0, value * rotationDirection * rotationSpeed * Time.deltaTime, 0);
    }

    void RotateAround()
    {
        //Debug.Log("rotating");
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
        //Debug.Log("Ring Rotating");
        transform.RotateAround(rotationAxis.position, rotationSpeed * randomDir, - rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
