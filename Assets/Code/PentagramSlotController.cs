using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PentagramSlotController : MonoBehaviour
{

    public bool isFilled = false;
    private SpriteRenderer spriteComponent;
    private Color activatedColor = new Color(0.8613477f, 0, 1f);

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
            spriteComponent.color = activatedColor;
            ForcePlayerDropItem(item);
            RenderBasicItemConsumedByPentagram(item);
        }
    }

    void ForcePlayerDropItem(BasicItem item) {
        PlayerController player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerController>();
        if (player1.CurrentlyHeldItem() == item) {
            player1.DropCurrentItem();
        }
        PlayerController player2 = GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerController>();
        if (player2.CurrentlyHeldItem() == item) {
            player2.DropCurrentItem();
        }
    }

    void RenderBasicItemConsumedByPentagram(BasicItem item) {
        if (item.GetComponentInChildren<Light>()) {
                item.GetComponentInChildren<Light>().color = activatedColor;
            }
        item.GetComponent<BoxCollider>().enabled = false;
        item.GetComponent<IsometricObject>().TargetOffset = -300;
    }
}
