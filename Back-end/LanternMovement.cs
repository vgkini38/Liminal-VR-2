using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternMovement : MonoBehaviour
{
    public Stack Lantern_Placement;
    public Stack Lantern_Speed;

    // Start is called before the first frame update

    public LanternMovement() {
        Lantern_Placement = new Stack(); Lantern_Speed = new Stack();
        var answer = AddLanternPositions();
        if (answer) { Debug.Log("Lantern Position and Speed adding successful.."); }
    }
    
    public Vector3 GetLaternTargetPosition()
    {
        try {
            return (Vector3)Lantern_Placement.Pop();
        }
        catch (System.Exception ex) { Debug.Log("Error At: Latern Target Position Pop.." + ex.Message); return new Vector3(0f, 0f, 0f); }
    }

    public float GetLanternMovementSpeed()
    {
        try { return (float) Lantern_Speed.Pop(); }
        catch (System.Exception ex){ Debug.Log("Error At: Latern Target Position Pop.." + ex.Message); return 0.00f; }
    }

    public bool AddLanternPositions()
    {
        try {
            Lantern_Placement.Push(new Vector3(2.000f, 0.255f, 0.350f)); Lantern_Speed.Push(1.10f);
            Lantern_Placement.Push(new Vector3(-1.84f, 0.255f, 1.670f)); Lantern_Speed.Push(1.40f);
            Lantern_Placement.Push(new Vector3(-3.25f, 0.255f, 4.010f)); Lantern_Speed.Push(1.80f);
            Lantern_Placement.Push(new Vector3(0.755f, 0.255f, 4.010f)); Lantern_Speed.Push(1.90f);
            Lantern_Placement.Push(new Vector3(3.766f, 0.255f, 4.010f)); Lantern_Speed.Push(2.00f);

            /*Lantern_Placement.Push(new Vector3());
            Lantern_Placement.Push(new Vector3());
            Lantern_Placement.Push(new Vector3());
            Lantern_Placement.Push(new Vector3());
            Lantern_Placement.Push(new Vector3());
            Lantern_Placement.Push(new Vector3());
            Lantern_Placement.Push(new Vector3());*/

            return true;
        }
        catch (System.Exception) { return false; }
    }
}
