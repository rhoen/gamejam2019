using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramController : MonoBehaviour
{
    const int ITEM_COUNT_TO_WIN = 5;
    public int CollectedItemCount = 0;

    void OnIncorrectItem() {

    }

    void OnCorrectItem() {
        // play first item or almost win sound or win sound
    }

    public void OnTriggerEnter(Collider other) {
        if (other.GetComponent<BasicItem>().specialItem) {
            OnCorrectItem();
        } else {
            OnIncorrectItem();
        }
    }

}
