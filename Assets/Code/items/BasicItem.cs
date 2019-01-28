using UnityEngine;
using System.Collections;

public class BasicItem : PickUpDroppableItem
{
    public bool specialItem;

    private bool mWasRevealed;

    public void OnTriggerEnter(Collider other) {
        if (!specialItem) { return; }
        if (other.GetComponent<BookController>() != null) {
             mWasRevealed = true;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    void addLight() {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
