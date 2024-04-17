using UnityEngine;

public class SelectionAreaUI : MonoBehaviour
{
    [SerializeField] private Transform _selectionAreaTransform;
    private bool _shouldScaleArea = false;

    private Vector3 _startPosition;

    private void Update()
    {
        if (_shouldScaleArea)
            ScaleSelectionArea();
    }

    public void OnSelectionStarted(Vector3 startPosition)
    {
        _selectionAreaTransform.gameObject.SetActive(true);
        _shouldScaleArea = true;

        _startPosition = startPosition;
    }
    
    public void OnSelectionEnded()
    {
        _selectionAreaTransform.gameObject.SetActive(false);
        _shouldScaleArea = false;
    }

    private void ScaleSelectionArea()
    {
        Vector3 currentMousePosition = Utilities.GetMouseWorldPosition();

        Vector3 lowerLeft = new Vector3(
            Mathf.Min(_startPosition.x, currentMousePosition.x),
            Mathf.Min(_startPosition.y, currentMousePosition.y)
            );

        Vector3 upperRight = new Vector3(
            Mathf.Max(_startPosition.x, currentMousePosition.x),
            Mathf.Max(_startPosition.y, currentMousePosition.y)
            );

        _selectionAreaTransform.position = lowerLeft;
        _selectionAreaTransform.localScale = upperRight - lowerLeft;
    }
}
