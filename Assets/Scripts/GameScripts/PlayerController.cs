using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private UnitRTSController _rtsUnitController;

    private void Awake()
    {
        _rtsUnitController = GameObject.Find("RTSUnitController").GetComponent<UnitRTSController>();
    }

    public void SelectUnits(InputAction.CallbackContext context)
    {
        if (context.started)
            _rtsUnitController.OnSelectionStarted();
        
        if (context.canceled)
            _rtsUnitController.OnSelectionEnded();
    }

    public void MoveSelectedUnits(InputAction.CallbackContext context)
    {
        if(context.started)
            _rtsUnitController.MoveSelectedUnitsToMousePosition();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var stateToChange = 
            GameManager.Instance.CurrentState == GameState.Paused ? GameState.Playing : GameState.Paused;
        GameManager.Instance.ChangeGameState(stateToChange);
    }
}
