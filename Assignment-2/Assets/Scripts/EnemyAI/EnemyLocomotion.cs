using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotion : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    public Vector3 currentVelocity;
    private float yInput;
    private Vector3 resetPoint;
    private Vector3 resetRotation;

    public void ResetPosition()
    {
        transform.position = resetPoint;
        transform.localEulerAngles = resetRotation;
    }

    private void Start()
    {
        resetPoint = transform.position;
        resetRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();
        // currentVelocity = GetComponent<Rigidbody>().velocity;
        // if (currentVelocity.z > 0)
        // {
        //     yInput = 1f;
        // }
        animator.SetFloat("InputX", 0f);
        animator.SetFloat("InputY", 1f);
    }
}
