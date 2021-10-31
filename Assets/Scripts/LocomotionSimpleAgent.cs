using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class LocomotionSimpleAgent : MonoBehaviour
{   
    UnityEngine.AI.NavMeshAgent agt;
    Animator anim;
    Vector2 sdpos = Vector2.zero;
    Vector2 vel = Vector2.zero;

    public float speed = 1.0f;
    public bool run = true;
    public Slider slider;
    public Toggle checkbox;
    

    void Update()
    {
        UnityEngine.AI.NavMeshHit h;
        int jmask = 4;

        if (!agt.SamplePathPosition(UnityEngine.AI.NavMesh.AllAreas, 0.0F, out h))
            if ((h.mask & jmask) != 0)
            {
                anim.SetFloat("Jump4Blend", 1);
            }
            else
            {
                anim.SetFloat("Jump4Blend", 0);
            }

        Vector3 wdpos = agt.nextPosition - transform.position;

        float dypos = Vector3.Dot(transform.forward, wdpos);
        float dxpos = Vector3.Dot(transform.right, wdpos);
        
        Vector2 dpos = new Vector2(dxpos, dypos);

        float s = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        sdpos = Vector2.Lerp(sdpos, dpos, s);

        if (Time.deltaTime > 1e-5f)
            vel = sdpos / Time.deltaTime;

        bool smove = vel.magnitude > 0.5f && agt.remainingDistance > agt.radius;

        anim.SetBool("move", smove);
        anim.SetFloat("velx", vel.x);
        
        float smod = Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1f;
        float y = 0;
        if (vel.y <= 0.5)
            y = vel.y;  
        else
        {
            y = smod;
        }
        anim.SetFloat("vely", y);

        if (GetComponent<LookAt>() != null)
        {
            LookAt lat = GetComponent<LookAt>();
            if (lat)
                lat.lookAtTargetPosition = agt.steeringTarget + transform.forward;
        }

        if (wdpos.magnitude > agt.radius)
            transform.position = agt.nextPosition - 0.9f * wdpos;
        
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        agt = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agt.updatePosition = false;
    }

    void OnAnimatorMove()
    {
        transform.position = agt.nextPosition;
    }

    public void OnRunValueChanged(bool check)
    {
        run = check;
    }

    public void OnValueChanged(float newValue)
    {
        speed = newValue;
        agt.speed = speed * 5;
    }
}
