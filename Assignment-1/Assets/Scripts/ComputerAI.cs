using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerAI : MonoBehaviour
{
    private float upperBound = 20f;
    private float lowerBound = -20f;
    public float moveSpeed = 35f;

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

            if (transform.position.z >= upperBound || transform.position.z <= lowerBound)
            {
                //If the z value is higher, lessen the z value by 1.
                if (transform.position.z >= upperBound)
                {
                    Vector3 v3 = new Vector3(1, 0, 0);
                    transform.Translate(v3);

                }
                //If the z value is lower, increase the z value by 1.
                else
                {

                    Vector3 v3 = new Vector3(-1, 0, 0);
                    transform.Translate(v3);

                }

            }
            else
            {
                if (transform.localPosition.z > lowerBound && ballPosition.z > transform.localPosition.z)
                {

                    transform.localPosition += new Vector3(0, 0, +moveSpeed * Time.deltaTime);
                }

                if (transform.localPosition.z < upperBound && ballPosition.z < transform.localPosition.z)
                {

                    transform.localPosition -= new Vector3(0, 0, moveSpeed * Time.deltaTime);
                }
            }

        }
    }
}
