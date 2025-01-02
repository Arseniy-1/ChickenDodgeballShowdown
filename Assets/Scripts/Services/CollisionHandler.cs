using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Ball> BallDetected;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            Interact(interactable);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IInteractable interactable))
        {
            Interact(interactable);
        }
    }

    private void Interact(IInteractable interactable)
    {
        if (interactable is Ball ball)
        {
            BallDetected.Invoke(ball);
        }
    }
}
