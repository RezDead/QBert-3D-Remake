using UnityEngine;

public class Disc : MonoBehaviour
{
    private bool _active = true;
    
    public void OnEnable()
    {
        EventBus.Subscribe(GameEvents.EndRound, EndRound);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe(GameEvents.EndRound, EndRound);
    }

    private void EndRound()
    {
        if (!_active) return;
        
        if (PlayerData.instance.currLevel > 1)
        {
            PlayerData.instance.currentScore += 100;
        }
        else
        {
            PlayerData.instance.currentScore += 50;
        }
    }

    public void Start()
    {
        EventBus.Subscribe(GameEvents.StartRound, OnStartRound);
    }

    private void OnStartRound()
    {
        _active = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DiscHit()
    {
        _active = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
}
