
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemFader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    ///<summary>
    /// 逐渐不透明
    /// </summary>
    public void FadeIn()
    {
        Color targetColour = new Color(1, 1, 1, 1);
        spriteRenderer.DOColor(targetColour, Settings.ItemFadeDuration);
    }
    
    ///<summary>
    /// 逐渐半透明
    /// </summary>
    public void FadeOut()
    {
        Color targetColour = new Color(1, 1, 1, Settings.FadeAlpha);
        spriteRenderer.DOColor(targetColour, Settings.ItemFadeDuration);
    }
}
