using UnityEngine;
using UnityEngine.XR;

public class BallHolder : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Transform _hand;
    [SerializeField] private CollisionHandler _collisionHandler;

    [field: SerializeField] public bool HasBall => _ball != null;

    private void OnEnable()
    {
        _collisionHandler.BallDetected += EquipBall;
    }

    private void OnDisable()
    {
        _collisionHandler.BallDetected -= EquipBall;
    }

    public bool TryGetBall(out Ball ball)
    {
        ball = null;

        if (HasBall)
        {
            _ball.transform.parent = null;
            ball = _ball;
            _ball = null;

            return true;
        }

        return false;
    }

    private void EquipBall(Ball ball)
    {
        _ball = ball;
        ball.Rigidbody.Sleep();
        ball.transform.parent = _hand.transform;
        ball.transform.position = _hand.transform.position;
    }
}
