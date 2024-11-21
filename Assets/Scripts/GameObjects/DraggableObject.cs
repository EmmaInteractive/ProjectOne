using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DraggableObject : MonoBehaviour, IEventHandler
{
    public DraggableObject()
    {
        this.CaptureMouse();
    }

    public void SendEvent(EventBase e)
    {
        
    }

    public void HandleEvent(EventBase evt)
    {
        ;
    }

    public bool HasTrickleDownHandlers()
    {
        return false;
    }

    public bool HasBubbleUpHandlers()
    {
        return false;
    }
}
