using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;

    private Health _health;

    private Transform _hand;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += Interact;
        _health.Died += RaiseDeath;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= Interact;
        _health.Died -= RaiseDeath;
    }

    public void TakeDamage(float amount)
    {
        _health.TakeDamage(amount);
    }

    private void Interact(IInteractable interactable)
    {
        if(interactable is Ball ball)
        {
            EnquipBall(ball);
        }
    }

    private void EnquipBall(Ball ball)
    {
        ball.transform.parent = _hand.transform;
        ball.transform.position = _hand.transform.position;
    }

    private void RaiseDeath()
    {
        Destroy(gameObject);
    }
}
