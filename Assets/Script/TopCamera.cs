using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCamera : MonoBehaviour
{
    public float totalspeed;
    public float scrollspeed;
    public float movespeed;
    // Start is called before the first frame update
    void Start()
    {
        totalspeed = 20f;
        scrollspeed = 20f;
        movespeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float keyH = Input.GetAxis("Horizontal");
        float keyV = Input.GetAxis("Vertical");
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        Vector3 updown = new Vector3(1f, 0f, 0f);
        Vector3 leftright = new Vector3(0f, 1f, 0f);
        Vector3 inout = new Vector3(0f, 0f, 1f);
        movespeed = transform.position.y / 500f;
        transform.Translate(updown * keyH * movespeed);
        transform.Translate(leftright * keyV * movespeed);
        transform.Translate(inout * wheelInput * scrollspeed);
    }
}
