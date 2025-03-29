using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class belt : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public GameObject beltrot;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = rb.position;

        
        Vector3 teste = Vector3.forward;
        teste.y = beltrot.transform.rotation.y;
        Debug.Log(beltrot.transform.rotation);

        

        rb.position += teste * speed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
    }
}
