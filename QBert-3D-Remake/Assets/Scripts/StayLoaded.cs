/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Prevents object from being destroyed.
 */

using UnityEngine;

public class StayLoaded : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
