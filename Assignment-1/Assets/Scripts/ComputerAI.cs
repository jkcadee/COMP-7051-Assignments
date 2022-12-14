using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerAI : MonoBehaviour
{
    private float upperBound = 22f;
    private float lowerBound = -22f;
    public float moveSpeed = 20f;

    private GameObject ball;
    private Vector3 ballPosition;

    private float direction;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");

    }

    public void FixedUpdate()
    {

        if (ball.GetComponent<BallBehaviour>().ballDirection > 0)
        {
            ballPosition = ball.transform.localPosition;

            // if (transform.position.z >= upperBound || transform.position.z <= lowerBound)
            // {
            //     //If the z value is higher, lessen the z value by 1.
            //     if (transform.position.z >= upperBound)
            //     {
            //         Vector3 v3 = new Vector3(1, 0, 0);
            //         transform.Translate(v3);

            //     }
            //     //If the z value is lower, increase the z value by 1.
            //     else
            //     {

            //         Vector3 v3 = new Vector3(-1, 0, 0);
            //         transform.Translate(v3);

            //     }

            // }
            // else
            // {
            if (ballPosition.z > transform.localPosition.z)
            {

                transform.localPosition += new Vector3(0, 0, +moveSpeed * Time.deltaTime);
                Vector3 pos = transform.position;
                pos.z = Mathf.Clamp(pos.z, lowerBound, upperBound);
                transform.position = pos;

            }

            if (ballPosition.z < transform.localPosition.z)
            {

                transform.localPosition -= new Vector3(0, 0, moveSpeed * Time.deltaTime);
                Vector3 pos = transform.position;
                pos.z = Mathf.Clamp(pos.z, lowerBound, upperBound);
                transform.position = pos;

            }

        }

    }
}
// }
