using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGameObject : MonoBehaviour
{
    public bool AlwaysInstantTrack = false;
    public float TrackSpeed = 0.02f;
    public float MinX = float.NegativeInfinity;
    public float MaxX = float.PositiveInfinity;
    public GameObject Target;

    int mInstantTrackFrames = 3; // so that the camera doesn't need to be placed exactly over the spawn point

    void Update()
    {
        if (mInstantTrackFrames-- > 0)
        {
            transform.position = new Vector3(Target.transform.position.x, transform.position.y, transform.position.z);
        }

        if (AlwaysInstantTrack) {
            transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);
            return;
        }

        Vector3 pos = transform.position;
        pos.x = Mathf.Max(MinX, pos.x);
        pos.x = Mathf.Min(MaxX, pos.x);
        pos.x = Mathf.Lerp(pos.x, Target.transform.position.x, TrackSpeed);
        transform.position = pos;
    }
}
