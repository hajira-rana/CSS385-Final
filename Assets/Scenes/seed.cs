using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Seed : MonoBehaviour
{
    public float maxChargeTime = 5f;
    public GameObject player;
    SeedMechanics script;
    public float maxForce = 100f;
    private float chargeTime = 0f;
    private bool isCharging = false;
    private Rigidbody rb;
    Collider colliderr;
    public string seedtype;
    bool watered = false;
    public bool onGrass;
    public GameObject plant;
    bool lake =  false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliderr = GetComponent<Collider>();
        colliderr.enabled = false;
        player = GameObject.Find("player");
        script = player.GetComponent<SeedMechanics>();
    }

    void Update()
    {
        if (watered && script.dothing)
        {
            grow(transform.position);
            script.dothing = false;
        }else if (lake && onGrass)
        {
            grow(transform.position);
        }
    }

    public void StartCharging()
    {
        if (!isCharging)
        {
            isCharging = true;
            chargeTime = 0f;
        }

        if (chargeTime < maxChargeTime)
        {
            chargeTime += Time.deltaTime;  // Accumulate charge time
        }

    }

    public void ThrowSeed(Vector3 direction)
    {
        isCharging = false;

        float throwForce = Mathf.Lerp(0, maxForce, chargeTime / maxChargeTime);

        Debug.Log($"Throwing seed with force: {throwForce} and direction: {direction}");
        colliderr.enabled = true;
        rb.useGravity = true;
        rb.velocity = direction * throwForce;
    }
    void grow(Vector3 pos)
    {
        Quaternion x = new Quaternion();
        watered = false;
        Plant blue = Instantiate(plant, transform.position, x).GetComponent<Plant>();
        blue.lake = lake;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("grass") || collision.gameObject.CompareTag("evilgrass")) && onGrass == false)
        {
            onGrass = true;
        }
    }
    public void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("water") && onGrass)
        {
            watered = true;
        }             
        if (collision.gameObject.CompareTag("waterr"))
        {
            lake = true;
        }
        if (collision.gameObject.CompareTag("rail"))
        {
            Debug.Log("inrange");
            script.pickupsInRange.Add(this.gameObject);
            Material mater = GetComponent<MeshRenderer>().material;
            mater.SetFloat("_Outline_Width", 50);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("rail"))
        {
            Debug.Log("outtarange");
            script.pickupsInRange.Remove(this.gameObject);
            Material mater = GetComponent<MeshRenderer>().material;
            mater.SetFloat("_Outline_Width", 0);
        }

    }

}

