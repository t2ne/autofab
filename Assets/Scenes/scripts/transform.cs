using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class transform : MonoBehaviour
{
    public List<GameObject> onZone;
    public GameObject FinalProduct;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        for (int i = onZone.Count - 1; i >= 0; i--)
        {
            if (onZone[i] == null)
            {
                // Remove the object if it is null (i.e., missing)
                onZone.RemoveAt(i);
            }
        }
        for (int i = 0; i <= onZone.Count - 1; i++)
            {
                if (onZone[i].name == "WoodenB(Clone)" )
                {
                    //Instantiate(FinalProduct, onZone[i].gameObject.transform);
                    onZone[i].gameObject.transform.localScale = Vector3.one/5;
                }
            if (onZone[i].name == "AtomB(Clone)")
            {
                //Instantiate(FinalProduct, onZone[i].gameObject.transform);
                onZone[i].gameObject.transform.localScale = Vector3.one / 2;
            }
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (!onZone.Contains(collider.gameObject))
        {
            onZone.Add(collider.gameObject);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (!onZone.Contains(collider.gameObject))
        {
            onZone.Remove(collider.gameObject);

        }
    }
}
