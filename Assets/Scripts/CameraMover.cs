﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float freeLookSensitivity = 3f;
    public float zoomSensitivity = 10f;
    
    private bool look = false;
  
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            Camera.main.transform.position = Camera.main.transform.position + (-Camera.main.transform.right * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
            Camera.main.transform.position = Camera.main.transform.position + (Camera.main.transform.right * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.UpArrow))
            Camera.main.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow))
            Camera.main.transform.position = Camera.main.transform.position + (-Camera.main.transform.forward * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightControl))
            Camera.main.transform.position = Camera.main.transform.position + (Camera.main.transform.up * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Slash))
            Camera.main.transform.position = Camera.main.transform.position + (-Camera.main.transform.up * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Quote))
            Camera.main.transform.position = Camera.main.transform.position + (Vector3.up * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightShift))
            Camera.main.transform.position = Camera.main.transform.position + (-Vector3.up * movementSpeed * Time.deltaTime);

        if (look)
        {
            float newRotationX = Camera.main.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;
            float newRotationY = Camera.main.transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivity;
            Camera.main.transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }

        float axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            var zoomSensitivity = this.zoomSensitivity;
            Camera.main.transform.position = Camera.main.transform.position + Camera.main.transform.forward * axis * zoomSensitivity;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Startlook();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Stoplook();
        }
    }

    void OnDisable()
    {
        Stoplook();
    }

    public void Startlook()
    {
        look = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Stoplook()
    {
        look = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
