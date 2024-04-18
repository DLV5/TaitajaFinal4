using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Facility: MonoBehaviour
{
    public Sprite Sprite { get; private set; }

    [field: SerializeField] public int WidthInCells { get; private set; }
    [field: SerializeField] public int HeightInCells { get; private set; }

    public void Initialize()
    {
        Sprite = GetComponent<SpriteRenderer>().sprite;
    }
    public void StartActivity()
    {
        // To-Do Activity that facility will do
        Debug.Log("Me Active ! " + name);
    }
}
