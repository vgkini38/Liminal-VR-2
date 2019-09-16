using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public LanternMovement LM;
    int t = 0;
    // Start is called before the first frame update
    void Start()
    {
        LM = new LanternMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (t == 1000)
        { Myfunction("Sphere"); t++; }
        else if (t == 2000)
        { Myfunction("Sphere1"); t++; }
        else if (t == 3000)
        { Myfunction("Sphere2"); t++; }
        else if (t == 4000)
        { Myfunction("Sphere3"); t++; }
        else if (t == 5000)
        { Myfunction("Sphere4"); t++; }
        else
            t++;

        Debug.Log(t);
    }

    public Vector3 LanternPosition { get { return LM.GetLaternTargetPosition(); } }
    public float LanternMovementSpeed { get { return LM.GetLanternMovementSpeed(); } }

    void Myfunction(string name)
    {
        var lantern = GameObject.Find(name);
        lantern.GetComponent<Lantern>().MoveSpeed = LanternMovementSpeed;
        lantern.GetComponent<Lantern>().TargetP = LanternPosition;
        lantern.GetComponent<Lantern>().TargetAvailable = true;
        Debug.Log("Lantern "+ name + " Done...");
    }
}
