using System.Collections.Generic;

namespace MovingMedian.Logic
{

    public class PriorityQueueBasedMedian: IMedian
    {
        private int? _median;
        private readonly PriorityQueue<int, int> _prioritySmaller;
        private readonly PriorityQueue<int, int> _priorityBigger;

        public PriorityQueueBasedMedian()
        {
            _priorityBigger = new PriorityQueue<int, int>();
            _prioritySmaller = new PriorityQueue<int, int>();
        }

        public void Add(int value)
        {
            //first element
            if (_median is null && _priorityBigger.Count == 0 && _prioritySmaller.Count == 0)
            {
                _median = value;
                return;
            }

            //second element
            if (_median is not null && _priorityBigger.Count == 0 && _prioritySmaller.Count == 0)
            {
                if (value < _median)
                {
                    _prioritySmaller.Enqueue(value, -value);
                    _priorityBigger.Enqueue(_median.Value, _median.Value);
                    _median = null;

                    return;
                }

                if (value > _median)
                {
                    _priorityBigger.Enqueue(value, value);
                    _prioritySmaller.Enqueue(_median.Value, -_median.Value);
                    _median = null;
                    return;
                }

                if (value == _median)
                {
                    _priorityBigger.Enqueue(value, value);
                    _prioritySmaller.Enqueue(value, -value);
                    _median = null;
                    return;
                }

                _median = value;
                return;
            }

            //succesive elements
            if (_priorityBigger.Count != 0 && _prioritySmaller.Count != 0)
            {
                //caso 1: numero par de elementos
                if (_median is null)
                {
                    //el valor es justo la mediana, caso a
                    if (_priorityBigger.Peek() == value || _prioritySmaller.Peek() == value)
                    {
                        _median = value;
                        return;
                    }

                    //el valor es justo la mediana, caso b
                    if (_priorityBigger.Peek() > value && _prioritySmaller.Peek() < value)
                    {
                        _median = value;
                        return;
                    }

                    //el valor es mas pequeño que el primer elemento de la cola izquierda
                    if (_prioritySmaller.Peek() > value)
                    {
                        _median = _prioritySmaller.EnqueueDequeue(value, -value);
                        return;
                    }

                    if (_priorityBigger.Peek() < value)
                    {
                        _median = _priorityBigger.EnqueueDequeue(value, value);
                        return;
                    }
                }

                //caso 2: numero par de elementos
                if (_median is not null)
                {
                    //el valor es justo la mediana, empujamos el valor a los lados.
                    if (_median == value)
                    {
                        _priorityBigger.Enqueue(value, value);
                        _prioritySmaller.Enqueue(value, -value);
                        _median = null;
                        return;
                    }

                    //el valor es mas pequeño que el primer elemento de la cola izquierda
                    if (_prioritySmaller.Peek() > value)
                    {
                        _prioritySmaller.Enqueue(value, -value);
                        _priorityBigger.Enqueue(_median.Value, _median.Value);
                        _median = null;
                        return;
                    }

                    //el valor es mas grande que el primer elemento de la cola derecha
                    if (_priorityBigger.Peek() < value)
                    {
                        _priorityBigger.Enqueue(value, value);
                        _prioritySmaller.Enqueue(_median.Value, -_median.Value);
                        _median = null;
                        return;
                    }
                }
            }
        }

        public double? Value
        {
            get
            {
               if(_median is not null) { 
                    return _median.Value; 
                }

               if( _median is null && _prioritySmaller.Count != 0) {
                    return ((double)_prioritySmaller.Peek() + (double)_priorityBigger.Peek()) / 2;
                }

                return double.NaN;
            }
        }
    }
}
