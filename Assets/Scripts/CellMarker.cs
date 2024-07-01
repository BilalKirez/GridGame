using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMarker : MonoBehaviour
{
    public bool isMarked = false;
    private SpriteRenderer spriteRenderer;
    public Sprite xSprite;
    public Sprite defaultSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
    }

    private void OnMouseDown()
    {
        if (!isMarked)
        {
            isMarked = true;
            spriteRenderer.sprite = xSprite;
            CheckForNeighbours();
        }
    }

    public void ResetCell()
    {
        isMarked = false;
        spriteRenderer.sprite = defaultSprite;
    }

    public void CheckForNeighbours()
    {

    }
}
