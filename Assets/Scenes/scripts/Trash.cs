using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Trash : MonoBehaviour
{
    public List<GameObject> onHit;
    public GameObject Output1;
    public GameObject Output2;
    public GameObject merger;
    public int bola1;
    public int bola2;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("bola1: " + bola1 + "bola2: " + bola2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        switch (collision.gameObject.name)
        {
            case "bola1(Clone)":
                bola1++;
                break;
            case "bola2(Clone)":
                bola2++;
                break;
            
        }
        onHit.Add(collision.gameObject);          
    }

    void FixedUpdate()
    {
        for (int i = 0; i <= onHit.Count - 1; i++)
        {
            Destroy(onHit[i]);
        }
        if (bola2 > 0 && bola1 > 0)
        {
            Instantiate(Output2, merger.transform.position, merger.transform.rotation);
            bola2 -= 1;
            bola1 -= 1;
        }
        if (bola1>0)
        {
            Instantiate(Output1, merger.transform.position, merger.transform.rotation);
            bola1 -= 1;
        }
    }
    void spawner()
    {
        bola2 -= 1;
        bola1 -= 1;
    }
}
