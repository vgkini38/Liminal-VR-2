using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public Vector3 TargetP;
    public float MoveSpeed = 0.00f;
    public bool TargetAvailable = false;
    // Start is called before the first frame update
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {
        if (TargetAvailable)
            transform.position = Vector3.MoveTowards(transform.position, TargetP, MoveSpeed * Time.deltaTime);   
    }
}
