using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CustomEvent : MonoBehaviour {

    public UnityEvent events;

    public void TriggerEvents()
    {
        events.Invoke();
    }
	
}
