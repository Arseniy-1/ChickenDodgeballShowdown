using System.Collections;
using UnityEngine;

public class BallThrower : MonoBehaviour
{
    [SerializeField] private float _minForce = 200f;
    [SerializeField] private float _maxForce = 1200f;
    [SerializeField] private float _chargeTime = 2;

    [SerializeField] private float _currentForce;
    private Coroutine _charging;

    public void StartCharging()
    {
        _currentForce = _minForce;
        _charging = StartCoroutine(Charging());
    }

    public void StopCharging()
    {
        if (_charging != null)
            StopCoroutine(_charging);
    }

    public void Throw(Ball ball)
    {
        ball.Rigidbody.AddForce(transform.forward * _currentForce, ForceMode.Force);
    }

    private IEnumerator Charging()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _chargeTime)
        {
            elapsedTime += Time.deltaTime;
            _currentForce = Mathf.Lerp(_minForce, _maxForce, elapsedTime / _chargeTime);

            yield return null;
        }

        _currentForce = _maxForce;
    }
}
