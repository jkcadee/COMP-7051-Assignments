using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class BallBehaviour : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 30f;

    void StartBallMovement()
    {
        Vector3 v3 = new Vector3(speed, 0, speed);
        rb.AddForce(v3, ForceMode.VelocityChange);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartBallMovement();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Player2")
            return;

        Rigidbody otherBody = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.velocity = rb.velocity.normalized * speed;
    }
}
