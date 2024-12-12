using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerInformation
{
    public float[] position;

    public PlayerInformation(MoveMent player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

}
