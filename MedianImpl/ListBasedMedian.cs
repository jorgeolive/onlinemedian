using System.Collections.Generic;

namespace MovingMedian.Logic
{
    public class ListBasedMedian : IMedian
    {
        private List<int> _list;
        private double _median = double.NaN;
        private bool _updateOnAdd = false;

        public ListBasedMedian(bool? updateOnAdd = null)
        {
            _updateOnAdd = updateOnAdd ?? false;
            _list = new();
        }

        public void Add(int value)
        {
            _list.Add(value);

            if (this._updateOnAdd)
            {
                CalculateMedian();
            }
        }
        
        private void CalculateMedian()
        {

            if (_list == null || _list.Count == 0)
            {
                _median = double.NaN;
            }

            _list.Sort();

            int middleIndex = _list.Count / 2;

            if (_list.Count % 2 == 0)
            {
                int middleValue1 = _list[middleIndex - 1];
                int middleValue2 = _list[middleIndex];
                _median = (double)(middleValue1 + middleValue2) / 2;
            }
            else
            {
                _median = _list[middleIndex];
            }
        }

        public double? Value
        {
            get
            {
                CalculateMedian();

                return _median;
            }
        }
    }
}
