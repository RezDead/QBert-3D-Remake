using System.Collections.Generic;
using UnityEngine.Events;

public class EventBus
{
    private static readonly IDictionary<GameEvents, UnityEvent> Events = new Dictionary<GameEvents, UnityEvent>();

    public static void Subscribe(GameEvents eventType, UnityAction listener)
    {
        //Check if event is in list, if so add listen
        if(Events.TryGetValue(eventType, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        //If not then add new event and add listener
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }

    public static void Unsubscribe(GameEvents eventType, UnityAction listener)
    {
        //If listening to an event remove that listening
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish(GameEvents eventType)
    {
        //If event is in dictionary broadcast event call to listeners.
        if (Events.TryGetValue(eventType,out var thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
