using System;
using Unity.VisualScripting;
using UnityEngine;

public class Disc : MonoBehaviour
{
    public void Start()
    {
        LevelManager.instance.discsInScene++;
        EventBus.Subscribe(GameEvents.StartRound, OnStartRound);
    }

    private void OnStartRound()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        LevelManager.instance.discsInScene++;
    }

    public void DiscHit()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        LevelManager.instance.discsInScene--;
    }
    
}
