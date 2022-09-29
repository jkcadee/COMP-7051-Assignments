using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class BallBehaviour : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 30f;

    /// <summary>
    /// Starts the ball's movement.
    /// 1 for direction moves right, -1 moves left.
    /// </summary>
    /// <param name="direction">1 = right, -1 = left</param>
    void StartBallMovement(int direction)
    {
        Vector3 v3 = new Vector3(direction, 0, Random.Range(-0.9f, 0.9f)) * speed;
        rb.AddForce(v3, ForceMode.VelocityChange);
    }

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        StartBallMovement(1);

    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Player2")
            return;

        Rigidbody otherBody = other.gameObject.GetComponent<Rigidbody>();

        Vector3 v3 = new Vector3(otherBody.position.x - rb.position.x, 0, otherBody.position.z - rb.position.z);
        rb.velocity = v3.normalized * -speed;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.velocity = rb.velocity.normalized * speed;

    }
}
