using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EndGameAnimatorScript : MonoBehaviour
{

    public float animationWait;
    public Sprite[] AnimationArray;
    private SpriteRenderer spriteComponent;

    public bool playOnStart = false;
    public bool repeatForever = false;
    public bool removeOnFinish = true;
    public float startDelayMs = 0f;

    private void Awake()
    {
        spriteComponent = GetComponent<SpriteRenderer>();
    }

    void Start() {
        if (playOnStart) {
            StartCoroutine(Animate());
        }
    }

    public IEnumerator Animate()
    {
        if (startDelayMs > 0) {
            yield return new WaitForSeconds(startDelayMs/1000f);
        }
        do {
            foreach (Sprite sprite in AnimationArray)
            {
                spriteComponent.sprite = sprite;
                yield return new WaitForSeconds(animationWait);
            }
        } while(repeatForever);
        if (removeOnFinish) {
            spriteComponent.sprite = null;
        }
    }
}
