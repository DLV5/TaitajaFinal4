using UnityEngine;

public class UnitMoveTransformVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private float _moveSpeed;

    private Vector3 _velocityVector;

    public void SetVelocity(Vector3 velocityVector) => _velocityVector = velocityVector;

    private void FixedUpdate() => transform.position += _velocityVector * _moveSpeed * Time.deltaTime;
}
