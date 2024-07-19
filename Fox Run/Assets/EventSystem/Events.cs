﻿/****************************************************************************/
/*!
\file   Events.cs
\par    Email: Support@U-EAT.org
\par    Developed: Summer 2016
\brief

This file contains optional custom declarations for events to be used by the EventSystem.
This class also has a custom property drawer in the inspector. When created as a public variable, these
classes will appear in a dropdown menu.
Why I am using static readonly instead of const: http://bit.ly/1WT8tmB

© 2016 U-EAT CC Attribution
*/
/****************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Events
{
    //Leave DefaultEvent as the first event defined in this class.
    public static readonly String DefaultEvent = "DefaultEvent";
    //All of the following events are examples of commonly used events. None of them are dispatched automatically.
    public static readonly String KeyboardEvent = "KeyboardEvent";
    public static readonly String MouseUp = "MouseUp";
    public static readonly String MouseDown = "MouseDown";
    public static readonly String MouseEnter = "MouseEnter";
    public static readonly String MouseExit = "MouseExit";
    public static readonly String MouseDrag = "MouseDrag";
    public static readonly String MouseOver = "MouseOver";
    
    public static readonly String Create = "CreateEvent";
    public static readonly String Initialize = "InitializeEvent";
    public static readonly String LogicUpdate = "LogicUpdate";
    public static readonly String LateUpdate = "LateUpdateEvent";
    public static readonly String Destroy = "DestroyEvent";

    //OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    public static readonly String OnCollisionEnter = "OnCollisionEnter";
    //Sent when an incoming collider makes contact with this object's collider (2D physics only).
    public static readonly String OnCollisionEnter2D = "OnCollisionEnter2D";
    //OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider.
    public static readonly String OnCollisionExit = "OnCollisionExit";
    //Sent when a collider on another object stops touching this object's collider (2D physics only).
    public static readonly String OnCollisionExit2D = "OnCollisionExit2D";
    //OnCollisionStay is called once per frame for every collider/rigidbody that is touching rigidbody/collider.
    public static readonly String OnCollisionStay = "OnCollisionStay";
    //Sent each frame where a collider on another object is touching this object's collider (2D physics only).
    public static readonly String OnCollisionStay2D = "OnCollisionStay2D";

#if UNITY_EDITOR
    //Whether or not to display a text input box or a dropdown menu.
    public bool AsString = false;
#endif
    //Nonstatic
    public string EventName = DefaultEvent;
    public Events() { }
    public Events(string eventName)
    {
        EventName = eventName;
    }
    //This class is essentially a string with a fancy inspector.
    public static implicit operator string(Events value)
    {
        return value.EventName;
    }
    public static implicit operator Events (string value)
    {
        return new Events(value);
    }
}

//Custom events
public class IntegerEvent : EventData
{
    public int Value;
    public IntegerEvent(int intValue = 0)
    {
        Value = intValue;
        
    }
    public static implicit operator int(IntegerEvent value)
    {
        return value.Value;
    }
    public static implicit operator IntegerEvent(int value)
    {
        return new IntegerEvent(value);
    }
}
public class FloatEvent : EventData
{
    public float Value;
    public FloatEvent(float floatValue = 0.0f)
    {
        Value = floatValue;
    }
    public static implicit operator float (FloatEvent value)
    {
        return value.Value;
    }
    public static implicit operator FloatEvent(float value)
    {
        return new FloatEvent(value);
    }
}
public class DoubleEvent : EventData
{
    public double Value;
    public DoubleEvent(double doubleValue = 0.0)
    {
        Value = doubleValue;
    }
    public static implicit operator double (DoubleEvent value)
    {
        return value.Value;
    }
    public static implicit operator DoubleEvent(double value)
    {
        return new DoubleEvent(value);
    }
}
public class StringEvent : EventData
{
    public string Value;
    public StringEvent(string stringValue = "")
    {
        Value = stringValue;

    }
    public static implicit operator string (StringEvent value)
    {
        return value.Value;
    }
    public static implicit operator StringEvent(string value)
    {
        return new StringEvent(value);
    }
}

public class CollisionEvent2D : EventData
{
    public Collision2D StoredCollision;
    public CollisionEvent2D(Collision2D collision = null)
    {
        StoredCollision = collision;
    }

    public static implicit operator Collision2D(CollisionEvent2D value)
    {
        return value.StoredCollision;
    }

    public static implicit operator CollisionEvent2D(Collision2D value)
    {
        return new CollisionEvent2D(value);
    }
}

public class CollisionEvent3D : EventData
{
    public Collision StoredCollision;
    public CollisionEvent3D(Collision collision = null)
    {
        StoredCollision = collision;
    }

    public static implicit operator Collision(CollisionEvent3D value)
    {
        return value.StoredCollision;
    }

    public static implicit operator CollisionEvent3D(Collision value)
    {
        return new CollisionEvent3D(value);
    }
}

//A custom property drawer for the 'Events' class.
//It can toggle between a string input field and a dropdown menu of all the events.
#if UNITY_EDITOR
namespace CustomInspector
{
    using System.Text;
    using UnityEditor;
    //Custom values for how the property scales when the inspector is resized.
    //These values very closely match with Unity's.
    public static class InspectorValues
    {
        //The width of an average text label.
        public static readonly float LabelWidth = 120;
        //The minimum possible width before clipping will occur.
        public static readonly float MinRectWidth = 340;
        //How fast the property scales with the width of the window.
        public static readonly float WidthScaler = 2.21f;
        //The general amount of padding from the inner edge of the inspector. (Towards the center of the screen)
        public static readonly float EdgePadding = 14;
    }
    
    [CustomPropertyDrawer(typeof(Events))]
    public class EventPropertyDrawer : PropertyDrawer
    {
        //The names of all the events in the 'Events' class.
        static string[] EventNames;
        //The string values of all the events in the 'Events' class.
        static string[] EventValues;
        
        //The width of the toggle button.
        const float ToggleWidth = 70;
        static Events LastEvents = new Events();
        //The parsing of the 'Events' class only needs to happen once, on compile.
        static EventPropertyDrawer()
        {
            List<string> eventNames = new List<string>();
            List<string> eventValues = new List<string>();
            var info = typeof(Events).GetFields();
            foreach (var i in info)
            {
                if (i.IsStatic && i.FieldType == typeof(string))
                {
                    eventValues.Add(i.Name);
                    eventNames.Add(i.GetValue(i.FieldType) as string);
                }
            }
            
            EventNames = eventNames.ToArray();
            EventValues = eventValues.ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var objectRef = property.FindPropertyRelative("EventName");
            var asStringRef = property.FindPropertyRelative("AsString");
            LastEvents.AsString = asStringRef.boolValue;
            LastEvents.EventName = objectRef.stringValue;
            LastEvents = Draw(position, LastEvents, label);
            objectRef.stringValue = LastEvents.EventName;
            asStringRef.boolValue = LastEvents.AsString;
            property.serializedObject.ApplyModifiedProperties();
        }

        static public Events Draw(Rect position, object eventsObj, GUIContent content)
        {
            Events events = eventsObj as Events;
            var labelRect = new Rect(position.x, position.y, InspectorValues.LabelWidth, position.height);
            //Draw the label.
            
            //Split up the property drawer into rectangles.
            var propStartPos = labelRect.position.x + labelRect.width;
            if (position.width > InspectorValues.MinRectWidth)
            {
                propStartPos += (position.width - InspectorValues.MinRectWidth) / InspectorValues.WidthScaler;
            }
            labelRect.width = propStartPos - 15;
            EditorGUI.SelectableLabel(labelRect, content.text);

            var toggleRect = new Rect(propStartPos, position.y, ToggleWidth, position.height);
            var eventRect = new Rect(toggleRect.position.x + toggleRect.width, position.y, position.width - (toggleRect.position.x + toggleRect.width) + InspectorValues.EdgePadding, position.height);
            //Was the property marked 'AsString' on the previous frame?
            var prevAsString = events.AsString;
            //Is the property marked 'AsString' this frame?
            events.AsString = EditorGUI.ToggleLeft(toggleRect, "AsString", events.AsString);
            if (!events.AsString && EventNames.Length != 0)
            {
                var index = IndexOf(EventNames, events.EventName);
                if (index == -1)
                {
                    if (events.AsString == prevAsString)
                    {
                        events.EventName = EditorGUI.TextField(eventRect, events.EventName);
                        events.AsString = true;
                        return events;
                    }
                    //Unchecking 'AsString' with a string value that is NOT a pre-existing event will reset its index to zero. (DefaultEvent)
                    index = 0;
                }
                events.EventName = EventNames[EditorGUI.Popup(eventRect, "", index, EventValues)];

            }
            else
            {
                events.EventName = EditorGUI.TextField(eventRect, events.EventName);
            }
            return events;
        }

        static int IndexOf(string[] eventNames, string name)
        {
            for(int i = 0; i < eventNames.Length; ++i)
            {
                if(eventNames[i] == name)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
#endif