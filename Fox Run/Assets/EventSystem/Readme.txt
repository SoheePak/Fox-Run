public class RandomClass
{

        public void Start()
        {
            -This is how you listen to a GameObject for an event.
            //The first parameter is the GameObject you are listening to.
            //The second parameter is the string name of the event you are listening to. 
            //(There is a static class of static strings in the Events.cs file you can add to.)
            //The final parameter is the function you are calling. This function must only have one parameter of type 'EventData' and return void.
            EventSystem.EventConnect(this, Events.DefaultEvent, SayHi);
            //(You can also call 'Connect' on any GameObject to connect to an event.)

            -This is how you send an event.
            //The first parameter is the object you are dispatching to.
            //The second parameter is the string name of the event you are dispatching.
            //The final parameter is an instance of a class that derives from 'EventData'. If this is not specified a default EventData is used.
            EventSystem.EventSend(this, Events.DefaultEvent, new StringEvent("Hello World"));
            //(You can also call 'DispatchEvent' on any object to send an event to it.)

            -This is how you disconnect from an event.
            //The first parameter is the object you are disconnecting from.
            //The second parameter is the name of the event yo are disconnecting from.
            //The final parameter can either be the object which hosts the function the event calls or the function itself.
            //(You must provide the function itself if you are disconnecting a static function. Or null to disconnect ALL static functions.)
            EventSystem.EventDisconnect(this, Events.DefaultEvent, SayHi);
            //(You can also call 'Disconnect' on any object to disconnect from an event.)

        }

        -This is an example of a function that can be called by an event.
        //The only parameter MUST be of type 'EventData'.
        //You can then typecast the EventData to one of its derived types if you so wish.
        //(This also means that you can send several different types of EventData through the same event.)
        void SayHi(EventData eventData)
        {
            var data = (eventData as StringEvent);
            Debug.Log(data.Message);
        }

    }

    //This is an example of a custom event. It MUST inherit from 'EventData'.
    public class StringEvent : EventData
    {
        public String Message;
        public StringEvent(String message = "")
        {
            Message = message;
        }
    }