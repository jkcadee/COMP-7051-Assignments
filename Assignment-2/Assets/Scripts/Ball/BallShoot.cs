using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BallShoot : MonoBehaviour
{
    InputActions ia;
    InputAction shoot;
    public GameObject body;
    public GameObject ballPrefab;
    GameObject ball;
    int score = 0;
    GameObject scoreCounter;

    void IncreaseScore()
    {
        score++;
        scoreCounter.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
    }

    void ShootBall(InputAction.CallbackContext _)
    {
        Destroy(ball);
        ball = Instantiate(ballPrefab, transform);
        ball.transform.SetParent(null);
        ball.transform.localEulerAngles = Vector3.zero;
        Physics.IgnoreCollision(ball.GetComponent<Collider>(), body.GetComponent<Collider>());
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        ball.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, body.transform.localEulerAngles.y, 0);
        rb.AddForce(ball.transform.rotation * new Vector3(0, 0, 200));
        ball.GetComponent<Ball>().action = IncreaseScore;
    }

    void Awake()
    {
        ia = new InputActions();
        shoot = ia.Player.Shoot;
    }

    private void Start()
    {
        scoreCounter = GameObject.Find("Score");
    }

    private void OnEnable()
    {
        shoot.Enable();
        shoot.performed += ShootBall;
    }

    private void OnDisable()
    {
        shoot.Disable();
        shoot.performed -= ShootBall;
    }
}
