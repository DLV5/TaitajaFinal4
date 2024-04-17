using UnityEngine;
using Pathfinding;

public class UnitRTS : MonoBehaviour
{
    [SerializeField] private UnitTeam _team = UnitTeam.Ally;
    private GameObject _selectedGameObject;
    private Seeker _seeker;
    private UnitDamage _damage;

    public UnitTeam Team { get => _team; }

    private void Awake()
    {
        _selectedGameObject = transform.Find("Selected").gameObject;

        _seeker = GetComponent<Seeker>();
        _damage = GetComponent<UnitDamage>();

        SetSelectedVisible(false);
    }

    private void OnEnable()
    {
        _damage.OnTargetWasFound += OnTargetFound;
    }

    private void OnDisable()
    {
        _damage.OnTargetWasFound -= OnTargetFound;
    }

    public void SetSelectedVisible(bool visible) => _selectedGameObject.SetActive(visible);

    public void MoveTo(Vector3 targetPosition)
    {
        _seeker.StartPath(transform.position, targetPosition);
    }

    private void OnTargetFound(Transform target)
    {
        if (target.GetComponent<UnitRTS>().Team == _team) return;

        MoveTo(target.position);
    }
}
