using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PentagramSlotController : MonoBehaviour
{

    public bool isFilled = false;
    private SpriteRenderer spriteComponent;
    private Color activatedColor = new Color(0, 0.02857161f, 1f);

    private void Awake()
    {
        spriteComponent = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isFilled) {
            return;
        }
        BasicItem item = other.gameObject.GetComponent<BasicItem>();
        if (item && item.specialItem)
        {
            isFilled = true;
            item.GetComponent<BoxCollider>().enabled = false;
            spriteComponent.color = activatedColor;
        }
    }
}
