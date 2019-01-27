using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameAnimatorScript : MonoBehaviour
{

    public float animationWait;
    public Sprite[] AnimationArray;
    private SpriteRenderer spriteComponent;

    private void Awake()
    {
        spriteComponent = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Animate()
    {
        foreach (Sprite sprite in AnimationArray)
        {
            spriteComponent.sprite = sprite;
            yield return new WaitForSeconds(animationWait);
        }
    }
}
