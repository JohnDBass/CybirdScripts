using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private float sensitivity = 0.1f, controllerSensitivity = 0.1f, maxLook = 2f,
        minLook = -1f, aimedSens = 0.2f, aimedMaxLook = 4f, aimedMinLook = -4f;

    [SerializeField]
    private GameObject crosshair;

    private float oSensitivity, oMaxLook, oMinLook, oFollowX, oFollowY, oFollowZ, oXDamp,
        oYDamp, oZDamp, oTrackedX, oTrackedY, oTrackedZ, oHDamp, oVDamp, oSoftW, oSoftH;

    private CinemachineComposer composer;
    private CinemachineTransposer transposer;
    private CinemachineCollider cineCollider;

    private bool aimed = false;

    private void Start()
    {
        cineCollider = GetComponent<CinemachineCollider>();
        composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
        transposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        oSensitivity = sensitivity;
        oMaxLook = maxLook;
        oMinLook = minLook;
        oFollowX = transposer.m_FollowOffset.x;
        oFollowY = transposer.m_FollowOffset.y;
        oFollowZ = transposer.m_FollowOffset.z;
        oXDamp = transposer.m_XDamping;
        oYDamp = transposer.m_YDamping;
        oZDamp = transposer.m_ZDamping;
        oTrackedX = composer.m_TrackedObjectOffset.x;
        oTrackedY = composer.m_TrackedObjectOffset.y;
        oTrackedZ = composer.m_TrackedObjectOffset.z;
        oHDamp = composer.m_HorizontalDamping;
        oVDamp = composer.m_VerticalDamping;
        oSoftW = composer.m_SoftZoneWidth;
        oSoftH = composer.m_SoftZoneHeight;
    }


    void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;
        float controllerVertical = (Input.GetAxis("Controller Y") * controllerSensitivity) * -1;
        composer.m_TrackedObjectOffset.y += vertical;
        composer.m_TrackedObjectOffset.y += controllerVertical;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, minLook, maxLook);

        if (Input.GetButton("Fire2"))
        {
            if (!aimed)
            {
                cineCollider.enabled = false;
                transposer.m_FollowOffset.x = 0.297f;
                transposer.m_FollowOffset.y = 0.316f;
                transposer.m_FollowOffset.z = -0.24f;
                transposer.m_XDamping = 0;
                transposer.m_YDamping = 0;
                transposer.m_ZDamping = 0;
                composer.m_TrackedObjectOffset.x = -0.47f;
                composer.m_TrackedObjectOffset.y = 0.25f;
                composer.m_TrackedObjectOffset.z = 6.94f;
                composer.m_HorizontalDamping = 0;
                composer.m_VerticalDamping = 0;
                composer.m_SoftZoneWidth = 0;
                composer.m_SoftZoneHeight = 0;
                sensitivity = aimedSens;
                minLook = aimedMinLook;
                maxLook = aimedMaxLook;
                crosshair.gameObject.SetActive(true);
                aimed = true;
            }

        }
        if (Input.GetButtonUp("Fire2"))
        {
            cineCollider.enabled = true;
            transposer.m_FollowOffset.x = oFollowX;
            transposer.m_FollowOffset.y = oFollowY;
            transposer.m_FollowOffset.z = oFollowZ;
            transposer.m_XDamping = oXDamp;
            transposer.m_YDamping = oYDamp;
            transposer.m_ZDamping = oZDamp;
            composer.m_TrackedObjectOffset.x = oTrackedX;
            composer.m_TrackedObjectOffset.y = oTrackedY;
            composer.m_TrackedObjectOffset.z = oTrackedZ;
            composer.m_HorizontalDamping = oHDamp;
            composer.m_VerticalDamping = oVDamp;
            composer.m_SoftZoneWidth = oSoftW;
            composer.m_SoftZoneHeight = oSoftH;
            sensitivity = oSensitivity;
            minLook = oMinLook;
            maxLook = oMaxLook;
            crosshair.gameObject.SetActive(false);
            aimed = false;
        }
    }
}
