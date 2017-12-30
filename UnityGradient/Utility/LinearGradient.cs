using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UnityGradient.Utility
{
    public class LinearGradient : GradientBase
    {
        public LinearGradient(Gradient brush, Vector2 startPoint, Vector2 endPoint) : base(brush)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
        }

        public Vector2 StartPoint { get { return _startPoint; } set { _startPoint = value; } }
        public Vector2 EndPoint { get { return _endPoint; } set { _endPoint = value; } }

        private Vector2 _startPoint;
        private Vector2 _endPoint;
    }
}
