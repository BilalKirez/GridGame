using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMarker : MonoBehaviour
{
    public bool isMarked = false;
    private SpriteRenderer spriteRenderer;
    public Sprite xSprite;
    public Sprite defaultSprite;
    public Vector2Int index;
    public GridManager gridManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
    }

    private void OnMouseDown()
    {
        if (isMarked)
            return;

        gridManager.connectedMarkedCells.Clear();
        isMarked = true;
        spriteRenderer.sprite = xSprite;
        gridManager.CheckExplosion(this);
    }

    public void ResetCell()
    {
        isMarked = false;
        spriteRenderer.sprite = defaultSprite;
    }
}
