﻿using SimpleRailwaySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainUnit : MonoBehaviour
{
    public bool isEmpty;

    public GameObject frontBogie;
    public GameObject backBogie;

    public List<GameObject> Wheels = new List<GameObject>();

    public GameObject Wagon;
    public GameObject WagonGfx;

    public float frontBogieTravelledDistance;
    public float backBogieTravelledDistance;

    public float positionInFrontOfLastWagon = 0;

    [SerializeField] RailManager m_RailManager = null;
    public float m_Speed = 1f;

    float m_TravelledDistance = 0;

    Transform m_Transform;

    private void Awake()
    {
        m_Transform = transform;

        if (isEmpty)
        {
            enabled = false;

            Debug.Log(enabled);
        }
    }

    public void UpdatePositionAtStart()
    {
        if (isEmpty)
        {
            enabled = false;

            Debug.Log(enabled);
        }
        else
        {
            m_TravelledDistance += positionInFrontOfLastWagon;

            frontBogieTravelledDistance = positionInFrontOfLastWagon + Vector3.Distance(gameObject.transform.position, frontBogie.transform.position);
            backBogieTravelledDistance = positionInFrontOfLastWagon - Vector3.Distance(gameObject.transform.position, backBogie.transform.position);
        }
      
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 tmpPos = Vector3.zero;
        Vector3 tmpNormal = Vector3.zero;
        Vector3 tmpTangent = Vector3.zero;
        int tmpCurrSegmentIndex = -1;

        m_RailManager.GetPositionNormalTangent(m_TravelledDistance / m_RailManager.Length, out tmpPos, out tmpNormal, out tmpTangent, out tmpCurrSegmentIndex);
        Wagon.transform.position = tmpPos;
        Wagon.transform.rotation = Quaternion.LookRotation(tmpTangent, tmpNormal);

        m_TravelledDistance += Time.deltaTime * m_Speed;



        // move front bogie
        m_RailManager.GetPositionNormalTangent(frontBogieTravelledDistance / m_RailManager.Length, out tmpPos, out tmpNormal, out tmpTangent, out tmpCurrSegmentIndex);
        frontBogie.transform.position = tmpPos;
        frontBogie.transform.rotation = Quaternion.LookRotation(tmpTangent, tmpNormal);

        frontBogieTravelledDistance += Time.deltaTime * m_Speed;

        // move back bogie
        m_RailManager.GetPositionNormalTangent(backBogieTravelledDistance / m_RailManager.Length, out tmpPos, out tmpNormal, out tmpTangent, out tmpCurrSegmentIndex);
        backBogie.transform.position = tmpPos;
        backBogie.transform.rotation = Quaternion.LookRotation(tmpTangent, tmpNormal);

        backBogieTravelledDistance += Time.deltaTime * m_Speed;


        //oscilate wagon
        WagonGfx.transform.localRotation = Quaternion.Euler(0, 0, m_Speed /2 * Mathf.Cos(Time.time));


    }


}
