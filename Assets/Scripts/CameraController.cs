using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform cameramain;
    public Vector3 offset;
    public Transform target;
    public ParticleSystem particle;


    private void Awake()
    {
        var x = particle.main;
        x.stopAction = ParticleSystemStopAction.Destroy;
        

        instance = this;
    }
    public void Update()
    {
        if (!target) return;
        transform.position = target.position + offset;
        cameramain.LookAt(target);
    }
}
