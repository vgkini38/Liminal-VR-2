using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Floating : MonoBehaviour
{
    public float waterLevel = 15.1f;
    public float floatThreshold = 5.0f;
    public float waterDensity = 0.125f;
    public float downForce = 0.5f;

    float forceFactor;
    Vector3 floatForce;

    void FixedUpdate()
    {
        forceFactor = 1.0f - ((transform.position.y - waterLevel) / floatThreshold);
        if (forceFactor > 0.0f)
        {
            floatForce = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().mass * waterDensity);
            floatForce += new Vector3(0.0f, -downForce, 0.0f);
            GetComponent<Rigidbody>().AddForceAtPosition(floatForce, transform.position);
        }

    }
}

