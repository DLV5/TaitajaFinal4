using System;
using System.Collections;
using UnityEngine;

public class UnitDamage : MonoBehaviour
{
    public event Action<Vector3> OnDamagedOtherUnit;
    public event Action<Transform> OnTargetWasFound;

    [SerializeField] private float _detectionRadius = 2f;

    [SerializeField] private int _damage = 2;
    [SerializeField] private float _damageFriequencyPerSecond = 1;

    private float _scanRepeatRate = 1f;

    private bool _isAttacking = false;
    private IDamagable _currentTarget;

    private void OnEnable()
    {
        InvokeRepeating("StartTryToFindTargetCoroutine", 0f, _scanRepeatRate);
    }

    private void StartTryToFindTargetCoroutine()
    {
        StartCoroutine(TryToFindTarget());
    }
    
    private void StopTryToFindTargetCoroutine()
    {
        StopCoroutine(TryToFindTarget());
    }

    private void OnDisable()
    {
        StopTryToFindTargetCoroutine();
        CancelInvoke();
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
        if (!(collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Ally")))
            return;

        if (collision.transform.CompareTag(gameObject.transform.tag))
            return;

        IDamagable damagable = collision.transform.GetComponent<IDamagable>();

        if(damagable != null)
        {
            _isAttacking = true;

            _currentTarget = damagable;

            StartCoroutine(DamageWithFriequency());

            OnDamagedOtherUnit?.Invoke(collision.transform.position);
        }
        
    }

    private IEnumerator DamageWithFriequency()
    {
        while (_isAttacking)
        {
            _currentTarget.TakeDamage(_damage);
            yield return new WaitForSeconds(1 / _damageFriequencyPerSecond);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _currentTarget = null;
        _isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
