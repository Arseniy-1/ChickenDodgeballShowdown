using UnityEngine;

public class TargetScaner : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 150f;
    [SerializeField] private Entitys _targetEntity;

    public Ball Ball { get; private set; }
    public EntityBehaviour TargetEntity { get; private set; }

    private void Update()
    {
        Scan();
    }

    public void Scan()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out ITarget target))
            {
                if (target is EntityBehaviour entity)
                {
                    if (entity.EntityType == _targetEntity)
                    {
                        TargetEntity = entity;
                        Debug.Log(entity);
                    }
                }
                else if (target is Ball ball)
                {
                    Ball = ball;
                }
            }
        }
    }
}

public enum Entitys
{
    Player, Enemy
}
