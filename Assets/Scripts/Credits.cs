using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{

    private float position = 0.0f;
    private float on_update_movement = 1.0f;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, on_update_movement, 0);
    }
}
