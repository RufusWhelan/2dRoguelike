using System;
using System.Collections.Generic;
using UnityEngine;

public class statesAndValues : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float fieldOfView;
    [SerializeField] private string[] states = {"idle","pursuit"};
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
