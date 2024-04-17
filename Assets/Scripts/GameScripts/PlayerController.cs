using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private RTSUnitController _rtsUnitController;

    private void Awake()
    {
        _rtsUnitController = GameObject.Find("RTSUnitController").GetComponent<RTSUnitController>();
    }

    public void SelectUnits(InputAction.CallbackContext context)
    {
        if (context.started)
            _rtsUnitController.OnSelectionStarted();
        
        if (context.canceled)
            _rtsUnitController.OnSelectionEnded();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var stateToChange = 
            GameManager.Instance.CurrentState == GameState.Paused ? GameState.Playing : GameState.Paused;
        GameManager.Instance.ChangeGameState(stateToChange);
    }
}
