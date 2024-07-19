/****************************************************************************/
/*!
\file   EventDispatcher.cs
\par    Email: Support@U-EAT.org
\par    Developed: Summer 2016
\brief

This script dispatches various events to its owner gameobject in response to input.

© 2016 U-EAT CC Attribution
*/
/****************************************************************************/
using UnityEngine;

//Custom classes for sending data inherit from the EventData class.
public class RotateEvent : EventData
{
    public enum Direction
    {
        Left,
        Right
    }

    public Direction RotationDir = Direction.Left;
}

public class EventDispatcher : MonoBehaviour
{

    RotateEvent RotateEventData = new RotateEvent();

    void Awake()
    {
        //Dispatching an event directly through the EventSystem class.
        EventSystem.DispatchEvent(gameObject, Events.Create);
    }

    // Use this for initialization
    void Start()
    {
        //Dispatching an event using the extension method.
        gameObject.DispatchEvent(Events.Initialize);
	}
	
	// Update is called once per frame
	void Update()
    {
        if(Input.anyKey)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotateEventData.RotationDir = RotateEvent.Direction.Left;
                //Dispatching an event with a custom name.
                gameObject.DispatchEvent("RotateEvent", RotateEventData);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                RotateEventData.RotationDir = RotateEvent.Direction.Right;
                //Dispatching an event with a custom name.
                gameObject.DispatchEvent("RotateEvent", RotateEventData);
            }
        }
    }

    void OnDestroy()
    {
        gameObject.DispatchEvent(Events.Destroy);
    }
}
