using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class LookAt : MonoBehaviour
{
    public Transform head = null;
    public Vector3 lookAtTargetPosition;
    public float cool = 0.2f;
    public float heat = 0.2f;
    public bool seeing = true;

    private Vector3 eyesOnPos;
    private Animator ani;
    private float w = 0.0f;

    void Start()
    {
        if (!head)
        {
            enabled = false;
            return;
        }
        ani = GetComponent<Animator>();
        lookAtTargetPosition = head.position + transform.forward;
        eyesOnPos = lookAtTargetPosition;
    }

    void OnAnimatorIK()
    {
        lookAtTargetPosition.y = head.position.y;
        float lookAtTargetWeight = seeing ? 1.0f : 0.0f;

        Vector3 curDir = eyesOnPos - head.position;
        Vector3 futDir = lookAtTargetPosition - head.position;

        curDir = Vector3.RotateTowards(curDir, futDir, 6.28f * Time.deltaTime, float.PositiveInfinity);
        eyesOnPos = head.position + curDir;

        float blendTime = lookAtTargetWeight > w ? heat : cool;
        w = Mathf.MoveTowards(w, lookAtTargetWeight, Time.deltaTime / blendTime);
        ani.SetLookAtWeight(w, 0.2f, 0.5f, 0.7f, 0.5f);
        ani.SetLookAtPosition(eyesOnPos);
    }
}