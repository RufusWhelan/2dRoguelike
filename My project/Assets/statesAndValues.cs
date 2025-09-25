using System;
using System.Collections.Generic;
using UnityEngine;

public class statesAndValues : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float fieldOfView;
    private string[] states = {"idle", "pursuit1", "pursuit2"};
    private string[] instanceStates;
    [SerializeField] private string currentState;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
