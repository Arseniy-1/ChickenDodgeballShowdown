using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Health _health;

    public void Initialize(Health health)
    {
        _health = health;
    }

    private void OnEnable()
    {
        _health.Died += RaiseDeath;
    }

    private void OnDisable()
    {
        _health.Died -= RaiseDeath;
    }

    private void RaiseDeath()
    {
        Destroy(gameObject);
    }
}
