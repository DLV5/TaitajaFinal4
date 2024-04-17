using UnityEngine;

public class MoveToVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private float _moveSpeed;

    private Vector3 _velocityVector;
    private Rigidbody2D _rigidbody2D;

    private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

    public void SetVelocity(Vector3 velocityVector) => _velocityVector = velocityVector;

    private void FixedUpdate() => _rigidbody2D.velocity = _velocityVector * _moveSpeed;
}
