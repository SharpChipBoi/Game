using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPosition : MonoBehaviour
{
    private GameObject tracker;

    private Material grassMat;

    // Start is called before the first frame update
    void Start()
    {
        grassMat = GetComponent<Renderer>().material;
        tracker = GameObject.Find("ThirdPersonPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 trackerPos = tracker.GetComponent<Transform>().position;

        grassMat.SetVector("_trackerPosition", trackerPos);
    }
}
