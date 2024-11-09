using System;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public event Action<TriggerZone> HasBall;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Ball ball))
        {
            HasBall?.Invoke(this);
        }
    }
}
