using UnityEngine;

namespace UnityGradient.Utility
{
    public class GradientBase
    {
        private GradientBase() { }
        public GradientBase(Gradient brush)
        {
            _brush = brush;
        }

        public Gradient Brush { get { return _brush; } set { _brush = value; } }

        private Gradient _brush;
    }
}
