using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CustomBuildingCursor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public Sprite CursorView
    {
        private get
        {
            return _spriteRenderer.sprite;
        }
        set
        {
            _spriteRenderer.sprite = value;
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Making new position for the cursor by converting mouse position into wolrdspace position
        transform.position = newPos;
    }
}
