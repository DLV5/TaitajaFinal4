using UnityEngine;

public class UnitDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bloodParticles;

    private UnitHealth _health;

    private void Awake()
    {
        _health = GetComponent<UnitHealth>();
    }

    private void OnEnable()
    {
        _health.OnUnitDied += OnUnitDied;
    }
    
    private void OnDisable()
    {
        _health.OnUnitDied -= OnUnitDied;
    }

    private void OnUnitDied()
    {
        Instantiate(_bloodParticles, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
    }
}
