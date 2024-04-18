using System;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private UnitTeam _team;

    private void OnDisable()
    {
        switch (_team)
        {
            case UnitTeam.Ally:
                GameManager.Instance.ChangeGameState(GameState.Lost);
                break;
            case UnitTeam.Enemy:
                GameManager.Instance.ChangeGameState(GameState.Lost);
                break;
        }
    }
}
