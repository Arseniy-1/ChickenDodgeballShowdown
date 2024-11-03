using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetScaner : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 150f;
    [SerializeField] private LayerMask _targetLayer;

    public Ball Ball { get; private set; }
    public PlayerBehaviour Player { get; private set; }

    private void Update()
    {
        Scan();
    }

    public void Scan()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);
        HashSet<ITarget> targets = new HashSet<ITarget>();

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out ITarget target) && (_targetLayer & (1 << hit.gameObject.layer)) != 0)
            {
                if (target is PlayerBehaviour player)
                {
                    Player = player;
                    Debug.Log(player);
                }
                else if (target is Ball ball)
                {
                    Ball = ball;
                }
            }
        }
    }
}