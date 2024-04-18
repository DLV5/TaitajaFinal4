using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlashMaterials
{
    public Material TargetMaterial;
    public Material BaseMaterial;
    public SpriteRenderer Renderer;
}

public class ColoredFlash : MonoBehaviour
{
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;

    [Tooltip("All materials of gameobject.")]
    [SerializeField] private List<FlashMaterials> targetMaterials;

    // The SpriteRenderer that should flash.
    private SpriteRenderer spriteRenderer;

    // The material that was in use, when the script started.
    private Material originalMaterial;

    // The currently running coroutine.
    private Coroutine flashRoutine;

    private UnitHealth _health;

    private void Awake()
    {
        _health = GetComponent<UnitHealth>();
    }

    private void OnEnable()
    {
        _health.OnUnitDamaged += Flash;
    }

    private void Start()
    {
        SetUpFlashMaterials();
    }

    private void OnDisable()
    {
        _health.OnUnitDamaged -= Flash;
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private void SetUpFlashMaterials()
    {
        foreach (FlashMaterials material in targetMaterials)
        {
            material.TargetMaterial = new Material(material.TargetMaterial);
        }
    }

    private IEnumerator FlashRoutine()
    {
        foreach (FlashMaterials material in targetMaterials)
        {
            material.Renderer.material = material.TargetMaterial;
        }

        yield return new WaitForSeconds(duration);

        foreach (FlashMaterials material in targetMaterials)
        {
            material.Renderer.material = material.BaseMaterial;
        }

        flashRoutine = null;
    }
}