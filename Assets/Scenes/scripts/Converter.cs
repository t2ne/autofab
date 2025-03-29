using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class Converter : MonoBehaviour
{
    public List<GameObject> onHit;
    public GameObject merger;
    public GameObject Wo;
    public GameObject Ato;
    public int Atom;
    public int Wooden;
    public TextMeshPro Pcount;
    public TextMeshPro Bcount;
    public TextMeshPro PBcount;
    public TextMeshPro PBLcount;
    public int PBint;
    public int PBLint;


    public float interval = 1.0f; // The interval in seconds between instances





    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {

        switch (collision.gameObject.name)
        {
            case "WoodenB(Clone)":
                Wooden++;
                break;
            case "AtomB(Clone)":
                Atom++;
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
        Pcount.text = "Atom: " + Atom.ToString();
        Bcount.text = "Wooden: " + Wooden.ToString(); 
        PBcount.text = PBint.ToString();
        PBLcount.text = PBLint.ToString();

    }
    public IEnumerator spawnWooden()
    {
        PBLint++;

        // Loop enquanto PBLint for maior ou igual a 1 e Wooden for maior ou igual a 0
        while (PBLint >= 1 && Wooden >= 1)
        {
            Instantiate(Wo, merger.transform.position, merger.transform.rotation);
            Wooden -= 1;


            // Se Wooden tornar-se menor que 0, pare o loop
            PBLint--;

            yield return new WaitForSeconds(interval);
            
        }
    }
    public IEnumerator spawnAtom()
    {
        PBint++;
        while (PBint >= 1 && Atom >= 1)
        {
            
            Instantiate(Ato, merger.transform.position, merger.transform.rotation);
            Atom -= 1;
            PBint--;
            yield return new WaitForSeconds(interval);
            
        }
        
    }
    
        
    
}
