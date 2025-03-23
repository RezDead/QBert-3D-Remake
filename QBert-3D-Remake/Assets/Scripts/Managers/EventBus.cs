/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Script that enables the main event system used within this project.
 */

using System.Collections.Generic;
using UnityEngine.Events;

public class EventBus
{
    private static readonly IDictionary<GameEvents, UnityEvent> Events = new Dictionary<GameEvents, UnityEvent>();

    /// <summary>
    /// Adds a function to the list of functions called when an event is triggered
    /// </summary>
    /// <param name="eventType">Game event listening to</param>
    /// <param name="listener">Function to be called when published</param>
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

    /// <summary>
    /// Removes a function from being called when an event is published.
    /// </summary>
    /// <param name="eventType">Game event listening to</param>
    /// <param name="listener">Function to be removed from call list when published</param>
    public static void Unsubscribe(GameEvents eventType, UnityAction listener)
    {
        //If listening to an event remove that listening
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Triggers the call for all functions subscribed to event list.
    /// </summary>
    /// <param name="eventType">Event to be called</param>
    public static void Publish(GameEvents eventType)
    {
        //If event is in dictionary broadcast event call to listeners.
        if (Events.TryGetValue(eventType,out var thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
