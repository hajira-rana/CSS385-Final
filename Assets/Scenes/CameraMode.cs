using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cinemachine;
using UnityEngine;


public class CameraMode : MonoBehaviour
{
    CinemachineFreeLook camerara;
    CinemachineComposer top, mid, low;

    void Awake()
    {
        camerara = GetComponent<CinemachineFreeLook>();
        top = camerara.GetRig(0).GetCinemachineComponent<CinemachineComposer>();
        mid = camerara.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        low = camerara.GetRig(2).GetCinemachineComponent<CinemachineComposer>();

    }

    public void aimMode()
    {
        camerara.m_RecenterToTargetHeading.m_WaitTime = 0;
        camerara.m_RecenterToTargetHeading.m_RecenteringTime = 0;
        camerara.m_RecenterToTargetHeading.m_enabled = true;

        camerara.m_YAxis.m_InvertInput = false;

        camerara.m_Orbits[0].m_Radius = 7;
        camerara.m_Orbits[1].m_Radius = 7;
        camerara.m_Orbits[2].m_Radius = 7;

        camerara.m_Orbits[0].m_Height = 17;
        camerara.m_Orbits[1].m_Height = 16.5f;
        camerara.m_Orbits[2].m_Height = 16;

        top.m_TrackedObjectOffset.y = 20;
        mid.m_TrackedObjectOffset.y = 15;
        low.m_TrackedObjectOffset.y = 10;

        camerara.m_XAxis.m_MinValue = camerara.m_XAxis.Value - 30;
        camerara.m_XAxis.m_MaxValue = camerara.m_XAxis.Value + 30;
        camerara.m_XAxis.m_Wrap = false;
        camerara.m_RecenterToTargetHeading.m_enabled = false;

        Debug.Log("aiming");
    }

    public void exitAim()
    {
        camerara.m_RecenterToTargetHeading.m_enabled = false;
        camerara.m_YAxis.m_InvertInput = true;


        top.m_TrackedObjectOffset.y = 12;
        mid.m_TrackedObjectOffset.y = 11;
        low.m_TrackedObjectOffset.y = 12;

        camerara.m_Orbits[0].m_Radius = 16;
        camerara.m_Orbits[1].m_Radius = 24;
        camerara.m_Orbits[2].m_Radius = 16;

        camerara.m_Orbits[0].m_Height = 24;
        camerara.m_Orbits[1].m_Height = 16;
        camerara.m_Orbits[2].m_Height = 5;

        camerara.m_XAxis.m_MinValue = -180;
        camerara.m_XAxis.m_MaxValue = 180;
        camerara.m_XAxis.m_Wrap = true;

    }
}
