using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Converter : MonoBehaviour
{
    public List<GameObject> onHit = new List<GameObject>();
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

    public float interval = 1.0f;

    void Start()
    {
    }

    void Update()
    {
        // Trigger coroutines with key presses (for testing)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(spawnWooden());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(spawnAtom());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == null) return;

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
        // Destroy & clean up list
        for (int i = onHit.Count - 1; i >= 0; i--)
        {
            if (onHit[i] != null)
            {
                Destroy(onHit[i]);
            }
            onHit.RemoveAt(i);
        }

        // Auto-trigger coroutines if conditions met
        if (PBLint == 0 && Wooden >= 1)
        {
            StartCoroutine(spawnWooden());
        }

        if (PBint == 0 && Atom >= 1)
        {
            StartCoroutine(spawnAtom());
        }

        // Update UI
        Pcount.text = "Atom: " + Atom.ToString();
        Bcount.text = "Wooden: " + Wooden.ToString();
    }

    public IEnumerator spawnWooden()
    {
        PBLint++;

        while (PBLint >= 1 && Wooden >= 1)
        {
            Instantiate(Wo, merger.transform.position, merger.transform.rotation);
            Wooden -= 1;
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
