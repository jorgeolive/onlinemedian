namespace MovingMedian.Logic
{
    public interface IMedian
    {
        void Add(int number);
        double? Value { get; }
    }
}
