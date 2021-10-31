using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorScript : MonoBehaviour
{

    private GameObject selected;
    private Rigidbody selectedPlayer;
    public float walkingSpeed = 10f;
    public float sensi = 3f;
    public float zoom = 10f;
    private bool seeing = false;


    public void StopLooking()
    {
        seeing = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void Start()
    {
        selected = null;
        selectedPlayer = null;
    }

    public void StartLooking()
    {
        seeing = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void OnDisable()
    {
        StopLooking();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            Camera.main.transform.position = Camera.main.transform.position + (-Camera.main.transform.right * walkingSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            Camera.main.transform.position = Camera.main.transform.position + (Camera.main.transform.right * walkingSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
            Camera.main.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * walkingSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            Camera.main.transform.position = Camera.main.transform.position + (-Camera.main.transform.forward * walkingSpeed * Time.deltaTime);


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit/*, 100*/))
            {

                if (hit.transform.tag == "Agent")
                {
                    if (selected != null)
                        selected.GetComponent<ClickToMove>().Deselect();
                    selected = hit.transform.gameObject;
                    selected.GetComponent<ClickToMove>().Select();
                }

                else if (selected != null)
                {
                    selected.GetComponent<ClickToMove>().Destination(hit.transform.gameObject.GetComponent<Collider>().ClosestPointOnBounds(hit.point));
                }
            }


        }

        if (Input.GetKey(KeyCode.Q))
            Camera.main.transform.position = Camera.main.transform.position + (Camera.main.transform.up * walkingSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.E))
            Camera.main.transform.position = Camera.main.transform.position + (-Camera.main.transform.up * walkingSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.R))
            Camera.main.transform.position = Camera.main.transform.position + (Vector3.up * walkingSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.F))
            Camera.main.transform.position = Camera.main.transform.position + (-Vector3.up * walkingSpeed * Time.deltaTime);

        if (seeing)
        {
            float newRotationX = Camera.main.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensi;
            float newRotationY = Camera.main.transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensi;
            Camera.main.transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }

        float axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            var zoom = this.zoom;
            Camera.main.transform.position = Camera.main.transform.position + Camera.main.transform.forward * axis * zoom;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartLooking();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            StopLooking();
        }



    }






}
