namespace Core.GameEvent
{
    public class SystemEvent
    {
        public static LifeDownEvent LifeDownEvent = new LifeDownEvent();
    }

    public class LifeDownEvent : GameEvent { }
}