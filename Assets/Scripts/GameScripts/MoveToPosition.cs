using UnityEngine;

public class MoveToPosition : MonoBehaviour, IMovePosition
{
    private Vector3 _targetPosition;

    private void Awake()
    {
        _targetPosition = transform.position;
    }

    private void Update()
    {
        Vector3 targetDirection = (_targetPosition - transform.position).normalized;

        if (Vector3.Distance(_targetPosition, transform.position) < 1f) targetDirection = Vector3.zero;

        GetComponent<IMoveVelocity>().SetVelocity(targetDirection);
    }

    public void SetMovePosition(Vector3 targetPosition) => _targetPosition = targetPosition;
}
