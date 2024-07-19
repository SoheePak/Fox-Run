/****************************************************************************/
/*!
\file   EventDispatcher.cs
\par    Email: Support@U-EAT.org
\par    Developed: Summer 2016
\brief

This script rotates its owner when it recieves a RotationEvent with a name that
matches the ListenToEvent member.

© 2016 U-EAT CC Attribution
*/
/****************************************************************************/
using UnityEngine;

public class RotateOnEvent : MonoBehaviour
{
    public GameObject ListenTargetObject;
    public Events ListenToEvent = Events.DefaultEvent;
    public float RotateSpeed = 30;
	void Awake ()
    {
        //Ensure that there is a object to connect to.
	    if(ListenTargetObject == null)
        {
            throw new System.Exception("RotateOnEvent must have an object that it is listening to");
        }
        ListenTargetObject.Connect(ListenToEvent, OnEventFunction);
	}
	
	
	void OnEventFunction(EventData data)
    {
        //The EventData sent mut be typecast to type that was sent.
        RotateEvent rotateData = (RotateEvent)data;
        var rotSpeed = new Vector3(0, 0, RotateSpeed) * Time.smoothDeltaTime;
        if(rotateData.RotationDir == RotateEvent.Direction.Right)
        {
            rotSpeed *= -1;
        }
        transform.eulerAngles += rotSpeed;
	}
}
