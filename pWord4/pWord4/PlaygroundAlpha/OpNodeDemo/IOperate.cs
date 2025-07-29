namespace OpNodeDemo
{
    public interface IOperate : IChange
    {
        public string Symbol { get; }
        PNode Operate(PNode node);
    }

    public interface IChange
    {
        bool Changed { get; }
        bool Change(PNode node);
        void ChangeFalse(PNode pNode);
    }
}