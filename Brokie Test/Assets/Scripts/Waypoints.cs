using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    private Transform CurrentTarget;

    private void Awake()
    {
        CurrentTarget = pointA;
    }

    private void Update()
    {
        if (transform.position == CurrentTarget.position)
        {

            SwitchPoint();
            //Debug.Log("here");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentTarget.position, .017f);
        }
    }

    private void SwitchPoint()
    {
        if(CurrentTarget == pointA) CurrentTarget = pointB; 
        else CurrentTarget = pointA;
    }
}
