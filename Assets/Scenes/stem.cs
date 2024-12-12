using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stem : MonoBehaviour
{
     void OnTriggerEnter(Collider collision)
     {
        transform.parent.GetComponent<Plant>().CollisionDetected(collision.tag);
     }

}
