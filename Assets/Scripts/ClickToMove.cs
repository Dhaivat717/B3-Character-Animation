using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class ClickToMove : MonoBehaviour
{

    public Transform g;

    private UnityEngine.AI.NavMeshAgent agt;
    private bool sel;
    private bool m;
    private Vector3 hd;

    

    void Update()
    {
        Vector3 ht = transform.position - vert(transform.position.y);
        print(ht + ", " + hd);
        if ((hd - ht).sqrMagnitude <= pow2(agt.stoppingDistance))
        {
            m = false;
        }

        if (m)
        {
            agt.isStopped = false;
        }
        else
        {
            agt.isStopped = true;
        }
    }

    void Start()
    {
        agt = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m = true;
        sel = false;
        Destination(g.position);
        agt.enabled = true;
    }

    public void Select()
    {
        sel = true;
    }
    
    void OnTriggerEnter(Collider coll)
    {
        GameObject o = coll.gameObject;
        if (canMove() && o.CompareTag("Agent") && getDest(o) == getDestination() && !cMove(o))
        {
            m = false;
        }
    }

    public Vector3 getDestination()
    {
        return agt.destination;
    }

    private Vector3 getDest(GameObject go)
    {
        return go.GetComponent<ClickToMove>().getDestination();
    }

    public void Deselect()
    {
        sel = false;
    }

    public void Destination(Vector3 d)
    {
        agt.destination = d;
        hd = d - vert(d.y);
        m = true;
    }

    public bool canMove()
    {
        return m;
    }

    private bool cMove(GameObject go)
    {
        return go.GetComponent<ClickToMove>().canMove();
    }
 
}
