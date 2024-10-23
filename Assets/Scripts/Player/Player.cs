using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;

    private InputHandler _inputHandler;
    private Health _health;

    private Transform _hand;
    private bool _hasBall = false;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _health = GetComponent<Health>();
        _inputHandler = GetComponent<InputHandler>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += Interact;
        _health.Died += RaiseDeath;
        _inputHandler.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= Interact;
        _health.Died -= RaiseDeath;
        _inputHandler.Clicked -= OnClicked;
    }

    public void TakeDamage(float amount)
    {
        _health.TakeDamage(amount);
    }

    private void OnClicked()
    {
        if (_hasBall)
        {

        }
        else
        {

        }
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
