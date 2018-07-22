using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 15f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 15f;
    
    public float maxXRange = 6.5f;
    public float minXRange = -6.5f;
    public float maxYRange = 4f;
    public float minYRange = -4f;

    [SerializeField] GameObject[] guns;

    [Header("Screen Position based")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 6f;
    
    [Header("Control-Throw based")]
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float controlPitchFactor = -20f;



    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Use this for initialization
    void Start () {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            processTranslation();
            processRotation();
            processFiring();

        }
    }


    private void processTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffsetThisFrame = xThrow * xSpeed * Time.deltaTime;
        float yOffsetThisFrame = yThrow * ySpeed * Time.deltaTime;

        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffsetThisFrame, minXRange, maxXRange);
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffsetThisFrame, minYRange, maxYRange);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }

    private void processRotation()
    {

        float pitchDuetoPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDuetoControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDuetoPosition + pitchDuetoControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;


        float roll = xThrow * controlRollFactor; 

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    //Called by String Reference
    void PlayerDied()
    {
        print("Player Died");
        isControlEnabled = false;
    }

    void processFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns) //may affect death fx
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }



}
