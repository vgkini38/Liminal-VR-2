using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    bool status = false;
    private bool canFade=true;
    private Color alphaColor;
    
    GameObject light;
    int vishwascounter = 0;
    GameObject flare;
    public GameObject txt;

    public Vector3 TargetP;
    public float MoveSpeed = 0.00f;
    public bool TargetAvailable = false;

    void Start()
    {
        canFade = true;
        alphaColor = txt.GetComponent<TextMesh>().color;
        alphaColor.a = 0;
        StartCoroutine(vgk());
        this.tag = "lantern";
        this.gameObject.layer = 9;
        light = GameObject.Find(this.name + "/PointLight");
        flare = GameObject.Find(this.name + "/FlareMobile");
        light.GetComponent<Light>().enabled = false;
        flare.SetActive(false);
        Debug.Log("Lantern Information Loaded");
        this.GetComponent<Floating>().enabled = true;
       
    }
    IEnumerator vgk()
    {

        yield return new WaitForSeconds(3);
        if (vishwascounter == 0)
        {
            txt.SetActive(true);
            float timeToFade = 2.0f;
            float timer = 0.0f;
            while (timer <= timeToFade)
            {
                timer += Time.deltaTime;
                float lerp_Percentage = timer / timeToFade;
                if (canFade)
                {
                    txt.GetComponent<TextMesh>().color = Color.Lerp(txt.GetComponent<TextMesh>().color, alphaColor, lerp_Percentage);
                }
            }
            txt.GetComponent<TextMesh>().text = "Please light the lanterns by hovering on them for 5 seconds";
           
        }
    }
    public void Update()
    {
       
        // this.GetComponent<Floating>().enabled = true;

        if (TargetAvailable)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;

            transform.position = Vector3.MoveTowards(transform.position, TargetP, MoveSpeed * Time.deltaTime);

            if ((Math.Abs(transform.position.x - TargetP.x) + Math.Abs(transform.position.z - TargetP.z)) < 4)
            {
                this.GetComponent<Rigidbody>().isKinematic = false;
                TargetAvailable = false;
            }
        }
        else
        {
            if ((Math.Abs(transform.position.x - TargetP.x) + Math.Abs(transform.position.z - TargetP.z)) < 4)
            { this.GetComponent<Rigidbody>().isKinematic = false; }
            else
                this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    


    public void LitItUp()
    {
        vishwascounter++;
        light.GetComponent<Light>().enabled = true;
        this.tag = "Untagged";
        StopFlare();
    }

    public void StartFlare()
    {
        flare.SetActive(true);
    }

    public void StopFlare()
    {
        flare.SetActive(false);
    }
}

