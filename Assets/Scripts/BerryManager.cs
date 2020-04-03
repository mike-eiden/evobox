using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryManager : MonoBehaviour
{

    public GameObject berriesChild;
    private bool hasBerries;

    // Prevent 2 different herbivores from both locking in on this bush. 
    private bool berriesLock;

    public bool BerriesLock
    {
        get => berriesLock;
        set => berriesLock = value;
    }

    void Start()
    {
        berriesLock = false;
        berriesChild.SetActive(true);
        hasBerries = true; 
        // Every 10 seconds there is a 50% chance that the bush respawns its berries. 
        InvokeRepeating(nameof(RespawnBerries), 1, 10);
    }

    private void RespawnBerries()
    {
        if (!hasBerries && Random.Range(0, 2) == 1)
        {
            berriesChild.SetActive(true);
            hasBerries = false;
            berriesLock = false; 
        }
    }

    // Function to be called by an herbivore. 
    public void EatBerries()
    {
        berriesChild.SetActive(false);
        hasBerries = false; 
    }
}
