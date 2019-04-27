using UnityEngine;
using System.Collections;

public class BasicItem : PickUpDroppableItem
{
    public bool specialItem;

    private bool mWasRevealed = false;

    private GameObject lightPrefab;

    void Awake() {
        lightPrefab = GameObject.FindWithTag("lightforitem");
    }

    public void OnTriggerEnter(Collider other) {
        if (other.GetComponent<BookController>()) {
        bool shouldReveal = specialItem && !mWasRevealed;
            if (shouldReveal) {
                mWasRevealed = true;
                addLight();
                DemonManager.Instance.SpawnEnemyAtPosition(gameObject.transform.position);
            }
        }
    }

    public override void PickUp() {
        if (gameObject.GetComponent<BoxCollider>() != null) { 
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public override void Drop() {
        if (gameObject.GetComponent<BoxCollider>() != null) { 
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void addLight() {
        Vector3 lightPos = gameObject.transform.position;
        lightPos.z -= .1f;
        GameObject lightClone = Instantiate(lightPrefab, lightPos, transform.rotation);
        lightClone.transform.parent = gameObject.transform;
    }
}
