using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateColor()
    {
        spriteRenderer.color = isAlive ? Color.white : Color.black;
    }
}
