using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour, IInteractable, ITarget
{
    public Rigidbody Rigidbody {get; private set;}

    public Transform Transform => transform;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
