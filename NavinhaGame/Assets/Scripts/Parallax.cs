using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float tileSizeX, prlxSpeed, mXplr;
    public Rigidbody2D player;
    Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float newPosition = Mathf.Repeat(Time.time * prlxSpeed, tileSizeX);

        transform.position = currentPosition + ((Vector3.right * -newPosition) * mXplr);
    }
}