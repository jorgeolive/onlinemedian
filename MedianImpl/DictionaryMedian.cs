using System;
using System.Collections.Generic;

namespace MovingMedian.Logic
{
    public class DictionaryMedian : IMedian
    {
        private readonly SortedDictionary<int, int> _numberCounts;
        private uint _numberOfSamples = 0;
        private double _median = double.NaN;
        private bool _updateOnAdd = false;

        public DictionaryMedian(bool? updateOnAdd = null)
        {
            _updateOnAdd = updateOnAdd ?? false;
            _numberCounts = new();
        }

        public double? Value
        {
            get
            {
                CalculateMedian();
                return _median;
            }
        }

        private void CalculateMedian()
        {
            var keys = _numberCounts.Keys;

            int temporarySum = 0;
            bool unevenSamples = _numberOfSamples % 2 != 0;
            var medianPosition = Convert.ToUInt32(Math.Abs(_numberOfSamples / 2)) + 1;
            int? previousKey = 0;

            foreach (var key in keys)
            {
                temporarySum += _numberCounts[key];

                if (temporarySum < medianPosition)
                {
                    previousKey = key;
                    continue;
                }
                else if (temporarySum >= medianPosition)
                {
                    if (unevenSamples)
                    {
                        _median = key;
                    }
                    else
                    {
                        _median = previousKey == 0 ? key : (double)(((double)key + (double)previousKey) / 2);
                    }

                    break;
                }
                else if (temporarySum > medianPosition)
                {
                    _median = key;
                    break;
                }
            }
        }

        public void Add(int number)
        {
            _numberOfSamples++;

            if (_numberCounts.ContainsKey(number))
            {
                _numberCounts[number]++;
            }
            else
            {
                _numberCounts[number] = 1;
            }

            if (this._updateOnAdd)
            {
                CalculateMedian();
            }
        }
    }
}

