using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    Animator plantanim;
    public bool lake;
    // Start is called before the first frame update
    void Start()
    {
        plantanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (lake)
        {
            plantanim.SetBool("isgrown", true);

        }
    }
    public void CollisionDetected(string collision)
    {

        if (collision == "waterr" || lake)
        {
            plantanim.SetBool("isgrown", true);

        }

        if (collision == "water")
        {
            Debug.Log("Animating plant");

            plantanim.SetBool("isgrown", true);
            if(plantanim.GetBool("isgrown")){
                plantanim.SetTrigger("Grow");
            }
        }        


    }
}
