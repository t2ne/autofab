using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonRaycast : MonoBehaviour
{
    public Camera cam;
    public GameObject obj;
    public GameObject[] dividers;
    public GameObject D1;
    public GameObject D2;
    private Boolean estado = true;
    private Boolean estado2 = true;
    public GameObject converter;
    public GameObject S1;
    public GameObject S2;
    public GameObject S2S;
    public GameObject S2S_2;
    private GameObject SS;
    public GameObject teste;

    public Material M1;
    public Material M2;
    public Camera MainCam;
    public Camera PlayerCam;
    bool isFirstLine1 = true;
    bool isFirstLine2 = true;



    private void Start()
    {
        S1 = GameObject.Find("S1");
        S2 = GameObject.Find("S2");
        S2S = GameObject.Find("S2S");
        S2S_2 = GameObject.Find("S2S_2");
        SS = S1;
        converter = GameObject.Find("Converter");
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                obj=hit.transform.gameObject;
                Debug.Log(obj);
                switch (obj.name)
                {
                  
                    case "B3":
               
                            StartCoroutine(converter.GetComponent<Converter>().spawnWooden());
                        
                      
                        break;
                    case "B4":
                        StartCoroutine(converter.GetComponent<Converter>().spawnAtom());
                        break;
                    
                    case "S2S":
                        
                        if (isFirstLine1)
                        {
                            S2.GetComponent<initial>().CancelInvoke();
                        }
                        else
                        {
                            S2.GetComponent<initial>().InvokeRepeating("spawner", 1, S2.GetComponent<initial>().timer);
                        }

                        // Alternar o estado para a pr�xima chamada
                        isFirstLine1 = !isFirstLine1;
                        break;

                    case "S2S_2":

                        if (isFirstLine2)
                        {
                            S1.GetComponent<initial>().CancelInvoke();
                        }
                        else
                        {
                            S1.GetComponent<initial>().InvokeRepeating("spawner", 1, S1.GetComponent<initial>().timer);
                        }

                        // Alternar o estado para a pr�xima chamada
                        isFirstLine2 = !isFirstLine2;
                        break;

                    case "BS":
                        SS.GetComponent<initial>().CancelInvoke();
                        SS.GetComponent<initial>().aumentarProd();
                        SS.GetComponent<initial>().InvokeRepeating("spawner", 1, SS.GetComponent<initial>().timer);
                        break;
                    case "BF":
                        SS.GetComponent<initial>().CancelInvoke();
                        SS.GetComponent<initial>().diminuirProd();
                        SS.GetComponent<initial>().InvokeRepeating("spawner", 1, SS.GetComponent<initial>().timer);
                        break;
                }
            }
        }
        // Belt Speed
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                obj = hit.transform.gameObject; 
                if (obj.tag == "Belt")
                {
                    obj.GetComponent<ConveyorBelt>().speed += 2;
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                obj = hit.transform.gameObject;
                if (obj.tag == "Belt")
                {
                    obj.GetComponent<ConveyorBelt>().speed -= 2;
                }
            }
        }
        // Cameras
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerCam.GetComponent<Camera>().enabled = false;
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerCam.GetComponent<Camera>().enabled = true;
        }
    }

    
}
