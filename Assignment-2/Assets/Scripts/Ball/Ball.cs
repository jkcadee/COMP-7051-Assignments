using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioSource vineBoom;
    public Action action;

    private void OnCollisionEnter(Collision other)
    {
        vineBoom.Play();

        if(other.gameObject.tag == "Enemy")
        {
            action();
            Destroy(gameObject);
        }
    }
}
