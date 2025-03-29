using UnityEngine;

public class initial : MonoBehaviour
{
    public int id;
    public Transform initiate;
    public GameObject objecter;
    public float currentpace;
    public float maxpace;
    public float timer = 1.9f;
    public float randomer = 1.9f;
    public GameObject buttonHit;
    public GameObject aumentar;
    public GameObject diminuir;
    
    


    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("spawner", 1, timer);
        buttonHit = null;
        if (id == 3)
        {
            CancelInvoke();
        }
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            InvokeRepeating("spawner", 1, timer);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 spawnPos = new Vector3(0, 0, 0);
            Instantiate(objecter, spawnPos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            CancelInvoke();
            aumentarProd();
            InvokeRepeating("spawner", 1, timer);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            CancelInvoke();
            diminuirProd();
            InvokeRepeating("spawner", 1, timer); 
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CancelInvoke();
            timer = Random.Range(0.1f, 1f);
            randomizer();

        }
    }
    private void FixedUpdate()
    {
    }


    public void aumentarProd()
    {
        if (timer >= 10f)
            return;
        timer += 0.2f;
    }

    public void diminuirProd()
    {
        if (timer <= 0.3f)
            return;
        timer -= 0.2f;
    }
    void randomizer()
    {
        //CancelInvoke();
        spawner();
        
        InvokeRepeating("randomizer", randomer, timer);
    }
    public void spawner()
    {
        Instantiate(objecter, initiate.position, initiate.rotation);
        randomer = Random.Range(1f, 5f);

    }
}
