using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingClouds : MonoBehaviour
{
    public Transform mainCamera;
    bool isCreated = false;
    public float speed = 0.5f;
    private void Start()
    {
    }

    private void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        if(isCreated) return;
        if (mainCamera.position.x > transform.position.x)
        {
            CreateCloud();
            isCreated = true;
        }
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    //Create cloud at the end of the screen
    private void CreateCloud()
    {
        var cloud = Instantiate(gameObject, transform.position, Quaternion.identity);
        cloud.transform.position = new Vector3(19, transform.position.y, transform.position.z);
    }
}
