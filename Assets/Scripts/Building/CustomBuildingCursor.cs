using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CustomBuildingCursor : MonoBehaviour
{
    [SerializeField]
    private Color _permissionColor;
    private bool _hasPermissionColor = false;
    [SerializeField]
    private Color _prohibitionColor;
    private bool _hasProhibitionColor = false;

    private Color _normalColor;

    private SpriteRenderer _spriteRenderer;
    private Facility _associatedFacility;
    private Vector3 _initialScale;
    public Facility AssociatedFacility
    {
        get
        {
            return _associatedFacility;
        }
        set
        {
            if(value == null)
            {
                _associatedFacility = value;
                return;
            }
            _spriteRenderer.enabled = true;
            _associatedFacility = value;
            transform.localScale = value.transform.localScale;
            _spriteRenderer.sprite = value.Sprite;
        }
    }

    public void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;

        _initialScale = transform.localScale;
        _normalColor = _spriteRenderer.material.color;
    }

    private void LateUpdate()
    {
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Making new position for the cursor by converting mouse position into wolrdspace position
        transform.position = newPos;
    }

    /// <summary>
    /// Called when the object is built or building is canceled, disables cursor visualization
    /// </summary>
    public void ResetCursor()
    {
        _spriteRenderer.enabled = false;
        _associatedFacility = null;
        transform.localScale = _initialScale;
        SetNormalColor();
    }

    public void HighlightPermission()
    {
        if (_hasPermissionColor)
            return;
        _spriteRenderer.color = _permissionColor;
        _hasPermissionColor = true;
        _hasProhibitionColor = false;
    }

    public void HighlightProhibition()
    {
        if (_hasProhibitionColor)
            return;
        _spriteRenderer.color = _prohibitionColor;
        _hasProhibitionColor = true;
        _hasPermissionColor = false;
    }
    private void SetNormalColor()
    {
        _spriteRenderer.color = _normalColor;
    }
}
