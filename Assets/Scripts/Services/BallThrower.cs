using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallThrower : MonoBehaviour
{
    private float _force = 1200f;

    public void Throw(Ball ball)
    {
        ball.Rigidbody.AddForce(transform.forward * _force, ForceMode.Force);
    }
}
