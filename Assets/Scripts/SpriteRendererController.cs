using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererController : MonoBehaviour
{
    public SpriteData spriteData;

    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Init();
    }

    public void Init() {
        spriteRenderer.sprite = spriteData.sprite;
    }

    void Update() {
        spriteRenderer.sprite = spriteData.sprite;
    }
}
