using UnityEngine;

public class TargetProvider : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 150f;
    [SerializeField] private Entitys _targetEntity;

    public Ball Ball { get; private set; }
    public EntityBehaviour TargetEntity { get; private set; }

    private void Update()
    {
        Scan();
    }

    public void SelectBall(Ball ball)
    {
        Ball = ball;
    }

    public void DeselectBall()
    {
        Ball = null;
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
                    }
                }
            }
        }
    }
}
