using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlertMan : MonoBehaviour
{
    public float radius;
    public float angle;

    //public GameObject enemyRef;

    public GameObject[] enemyRef;

    public LayerMask targetMask;

    public ParticleSystem _radarSys;

    public bool inRange;

    void Start()
    {
        //enemyRef = GameObject.FindGameObjectWithTag("Enemy");

        enemyRef = GameObject.FindGameObjectsWithTag("Enemy");


        _radarSys.Play();



        StartCoroutine(RangeRoutine());
    }


    void Update()
    {
        if (enemyRef.Length == 0)
        {
            Debug.Log("No enemies in range");
        }

        //if(!inRange)
        //{
        //    _radarSys.Pause();
        //}
        //else
        //{
        //    _radarSys.Play();
        //}
    }

    IEnumerator RangeRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FOVCheck();
        }
    }

    private void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            

            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < angle / 2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);

                
                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, targetMask))
                    
                    inRange = true;
                    
                else
                    inRange = false;
            }
            else
                inRange = true;
            
                
        }
        else if (inRange)
            inRange = false;
    }
}
