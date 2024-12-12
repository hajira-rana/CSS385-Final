using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAN : MonoBehaviour
{
    public GameObject player;
    SeedMechanics script;


    // Start is called before the first frame update    
    void Start()
    {

        player = GameObject.Find("player");
        script = player.GetComponent<SeedMechanics>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("rail"))
        {
            script.pickupsInRange.Add(this.gameObject);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("rail"))
        {
            script.pickupsInRange.Remove(this.gameObject);

        }

    }
}
