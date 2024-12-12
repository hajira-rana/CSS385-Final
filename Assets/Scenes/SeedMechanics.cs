using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class SeedMechanics : MonoBehaviour
{
    public CameraMode cam;
    public bool isAiming;

    SkinnedMeshRenderer look;
    GameObject seedObject;
    public GameObject seed;
    Seed seedScript;

    public GameObject waterer;
    public GameObject can;
    public GameObject hand;
    MoveMent playerr;
    Camera mainCamera;
    bool isThrown = false;
    public int slot = 0;
    string[] items = { "Pickup", "Pickup", "Seeds" };

    public TMP_Text coins;
    public Dig dialogue;
    int SeedAmnt = 10;
    PauseMenu menu;

    public bool water;
    public bool dothing = false;
    void Update()
    {
        check();
        if (water)
        {
            waterPlants();
        }

    }
    void Start()
    {
        playerr = GetComponent<MoveMent>();
        look = GetComponentInChildren<SkinnedMeshRenderer>();
        seedScript = seed.GetComponent<Seed>();
        menu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        dialogue = GameObject.Find("Dialogue").GetComponent<Dig>();


        playerr.playerInput.CharacterControls.MainAction.started += OnAction;
        playerr.playerInput.CharacterControls.SlotChange.started += slotChange;
        coins.text = "Equipped: " + items[slot];


    }
    public void aiming()
    {
        isThrown = false;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 16, transform.position.z);
        seedObject = Instantiate(seed, pos, transform.rotation);
        seedScript = seedObject.GetComponent<Seed>();
        seedScript.seedtype = items[slot];
        cam.aimMode();

        look.enabled = false;

    }
    public void check()
    {
        if (isAiming)
        {
            if (look.enabled == true)
            {
                look.enabled = false;
            }


            if (Input.GetKey(KeyCode.R))
            {

                seedScript.StartCharging();
            }

            if (Input.GetKeyUp(KeyCode.R))
            {
                mainCamera = Camera.main;
                seedScript.ThrowSeed(mainCamera.transform.forward);
                isThrown = true;
                aiming();

            }

        }
        if (seedObject != null && !isThrown && !isAiming)
        {
            Destroy(seedObject);

        }
        if (!isAiming && look.enabled == false)
        {

            look.enabled = true;
        }
    }
    public void slotChange(InputAction.CallbackContext context)
    {
        if (!isAiming)
        {
            slot++;
            if (slot >= items.Length)
            {
                slot = 0;
            }
        }
        coins.text = "Equipped: " + items[slot];
        if (slot == 2)
        {
            coins.text = "Equipped: " + items[slot] + " x  " + SeedAmnt;

        }
    }

    void waterPlants()
    {    
        can.gameObject.SetActive(true);
        waterer.GetComponent<Collider>().enabled = true;
        Vector3 pos = new Vector3(hand.transform.position.x, hand.transform.position.y - 2, hand.transform.position.z);
        waterer.transform.position = pos;
    }
    void done()
    {
        water = false;
        waterer.GetComponent<Collider>().enabled = false;
        can.gameObject.SetActive(false);
        Debug.Log("Dotthing true");
        dothing = true;
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (isAiming)
        {
            isAiming = !isAiming;
            cam.exitAim();

        }
        else
        {
            switch (items[slot])
            {
                case "Pickup":
                    PickUp();
                    break;
                case "Seeds":
                    isAiming = !isAiming;
                    if (isAiming)
                    {
                        cam.aimMode();
                        aiming();
                    }
                    else
                    {
                        cam.exitAim();
                    }
                    break;

                case "Can":
                    water = true;
                    playerr.waterAnim();
                    break;

                default:
                    Debug.Log("Item not recognized");
                    break;
            }
        }
    }
    public List<GameObject> pickupsInRange = new List<GameObject>();

    void PickUp()
    {
        GameObject closestPickup = null;
        float smallestDist = 400;

        foreach (GameObject pickup in pickupsInRange)
        {
 if (pickup != null)
        {
            float dist = Vector3.Distance(pickup.transform.position, transform.position);
            if (dist < smallestDist)
            {
                smallestDist = dist;
                closestPickup = pickup;
            }
        }
        }

        if (closestPickup != null)
        {
            Debug.Log("Picked up and destroyed: " + closestPickup.name);
            if (closestPickup.CompareTag("Victory"))
            {
                WinScreen();
            }if (closestPickup.CompareTag("seed"))
            {
                SeedAmnt++;
                coins.text = "Equipped: " + items[slot] + " x  " + SeedAmnt;

            }
            if (closestPickup.CompareTag("can"))
            {
                dialogue.gameObject.SetActive(true);
                string[] myArray = { "I got a watering can!","I can press SPACE to use it","I can press 'F' to switch between using my can or picking stuff up", "I wish I had something to use it on though" };
                dialogue.meow(myArray);
                items[1] = "Can";

            }
            if(pickupsInRange.Contains(closestPickup)){
            pickupsInRange.Remove(closestPickup);
            }
            Destroy(closestPickup);
        }

    }
    void WinScreen()
    {
        menu.Win();
    }

}
