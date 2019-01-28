using UnityEngine;
using System.Collections;

public class BasicItem : PickUpDroppableItem
{
    public bool specialItem;

    private bool mWasRevealed;

    private GameObject lightPrefab;

    private GameObject mRevealLight;

    void Awake() {
        lightPrefab = GameObject.FindWithTag("lightforitem");
    }

    public void OnTriggerEnter(Collider other) {
        if (!specialItem || mRevealLight != null) { return; }
        if (other.GetComponent<BookController>() != null) {
             addLight();
        }
    }

    void addLight() {
        Vector3 lightPos = gameObject.transform.position;
        lightPos.z -= .13f;
        GameObject lightClone = Instantiate(lightPrefab, lightPos, transform.rotation);
        lightClone.transform.parent = gameObject.transform;
        mRevealLight = lightClone;
    }
}
