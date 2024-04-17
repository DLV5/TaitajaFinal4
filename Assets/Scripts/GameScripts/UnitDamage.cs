using System;
using System.Collections;
using UnityEngine;

public class UnitDamage : MonoBehaviour
{
    public event Action<Transform> OnTargetWasFound;

    [SerializeField] private float _detectionRadius = 2f;

    [SerializeField] private int _damage = 2;

    private float _scanRepeatRate = 1f;

    private bool _isAttacking = false;

    private void Start()
    {
        InvokeRepeating("StartTryToFindTargetCoroutine", 0f, _scanRepeatRate);
    }

    private void StartTryToFindTargetCoroutine()
    {
        StartCoroutine(TryToFindTarget());
    }

    private IEnumerator TryToFindTarget()
    {
        if (_isAttacking) yield return null;

        yield return new WaitForSeconds(1);

        RaycastHit2D[] hits = ScanZone();

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Ally"))
            {
                OnTargetWasFound?.Invoke(hit.transform);
            }
        }
    }

    private RaycastHit2D[] ScanZone()
    {
        return Physics2D.CircleCastAll(transform.position, _detectionRadius, Vector2.zero);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            IDamagable damagable = collision.transform.GetComponent<IDamagable>();

            if(damagable != null)
            {
                Debug.Log("Damaged: " + collision.transform.name);
                damagable.TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
