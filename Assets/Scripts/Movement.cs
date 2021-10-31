using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Animator a;
    private Rigidbody rb;
    private Vector3 mdir = Vector3.zero;

    public float js = 2;

    public float t;

    
    private void Mve(float x, float y)
    {
        a.SetFloat("velx", x);
        a.SetFloat("vely", y);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            mdir.y = js * Time.deltaTime;
            a.SetFloat("Jump4Blend", 1);
            rb.AddForce(0, 3, 0);
        }
        else a.SetFloat("Jump4Blend", 0);
    }

    void Update()
    {
        if (a == null)
            return;

        var x = Input.GetAxis("Horizontal");
        float smod = Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1f;
        var y = Input.GetAxis("Vertical") * smod;
        Mve(x, y);
    }

    void Start()
    {
        a = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        t = 0;
    }

}
