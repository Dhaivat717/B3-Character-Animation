using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class MoveCamera : MonoBehaviour
{

    public float zoom = 10f;
    public float pitch = 1f;
    public GameObject target = null;
    private float d = 10f;
    private float YD = 3f;
    private float xTheta = 30f;
    private float yTheta = 0f;
    private float yThetaPrev = 0f;


    void Start()
    {
        if (target != null)
        {
            Vector3 fif = transform.position - target.transform.position;
            d = fif.magnitude;
            xTheta = transform.eulerAngles.x;
            yTheta = transform.eulerAngles.y;
            yThetaPrev = transform.eulerAngles.y;
        }
    }
    public void setTarget(GameObject target)
    {
        target = target;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            d -= Input.mouseScrollDelta.y * zoom * Time.deltaTime;
            xTheta -= geta("JK") * pitch;

            if (Mathf.Abs(yThetaPrev - target.transform.eulerAngles.y) > 1.0f)
                yTheta = target.transform.eulerAngles.y;

            yThetaPrev = target.transform.eulerAngles.y;

            Quaternion r = Quaternion.Euler(xTheta, yTheta, 0);
            Vector3 pt = r * Vector3.forward;
            Vector3 p = target.transform.position - d * pt + YD * Vector3.up;
            transform.SetPositionAndRotation(p, r);
        }
    }
    private float geta(string ax)
    {
        return Input.GetAxis(ax);
    }

}
