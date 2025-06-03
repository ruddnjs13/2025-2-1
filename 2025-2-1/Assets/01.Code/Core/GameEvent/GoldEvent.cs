namespace Core.GameEvent
{
    public class GoldEvent
    {
        public static GetGoldEvent getGoldEvent = new GetGoldEvent();
        public static SpendGoldEvent spendGolEvent = new SpendGoldEvent();
    }

    public class GetGoldEvent : GameEvent
    {
        public int getAmount;

        public GetGoldEvent Initialize(int amount)
        {
            getAmount = amount;
            return this;
        }
    }

    public class SpendGoldEvent : GameEvent
    {
        public int spendAmount;
        public SpendGoldEvent Initialize(int amount)
        {
            spendAmount = amount;
            return this;
        }
    }
}