using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    public NavMeshAgent agt;
    public Camera cam;

    

    void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit h;

            if (Physics.Raycast(r, out h))
            {
                agt.SetDestination(h.point);
            }
        }
    }
}