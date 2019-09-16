using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circular : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f), 90f * Time.deltaTime);
    }
}
