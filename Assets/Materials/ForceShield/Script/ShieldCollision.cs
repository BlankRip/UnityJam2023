﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{

    [SerializeField] string[] _collisionTag;
    [SerializeField] AudioData hitAudio;
    float hitTime;
    Material mat;

    void Start()
    {
        if (GetComponent<Renderer>())
        {
            mat = GetComponent<Renderer>().sharedMaterial;
		    mat.SetFloat("_HitTime", 0);
        }
	}

    void Update()
    {

        if (hitTime > 0)
        {
            float myTime = Time.fixedDeltaTime * 1000;
            hitTime -= myTime;
            if (hitTime < 0)
            {
                hitTime = 0;
            }
            mat.SetFloat("_HitTime", hitTime);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if(_collisionTag.Length == 0)
            return;
        
        for (int i = 0; i < _collisionTag.Length; i++)
        {

            if (collision.transform.CompareTag(_collisionTag[i]))
            {
                ContactPoint[] _contacts = collision.contacts;
                for (int i2 = 0; i2 < _contacts.Length; i2++)
                {
                    mat.SetVector("_HitPosition", transform.InverseTransformPoint(_contacts[i2].point));
                    hitTime = 500;
                    mat.SetFloat("_HitTime", hitTime);
                }
                Sound2D.Instance.PlayOneShotAudio(hitAudio);
            }
        }
    }
}

