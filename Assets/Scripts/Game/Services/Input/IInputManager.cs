using DylanJay.Framework;
using UnityEngine;

namespace DylanJay.Services
{
    public class ButtonInput
    {
        private string buttonName;

        public ButtonInput(string axisName) { this.buttonName = axisName; }

        public bool isPressed
        {
            get
            {
                return Input.GetButton(buttonName);
            }
        }

        public bool wasPressed
        {
            get
            {
                return Input.GetButtonDown(buttonName);
            }
        }

        public bool wasReleased
        {
            get
            {
                return Input.GetButtonUp(buttonName);
            }
        }
    }

    public class AxisInput
    {
        private string axisName;

        public AxisInput(string axisName) { this.axisName = axisName; }

        public float value
        {
            get
            {
                return Input.GetAxis(axisName);
            }
        }

        public float valueRaw
        {
            get
            {
                return Input.GetAxisRaw(axisName);
            }
        }
    }

    public class Axis2DInput
    {
        public AxisInput x;
        public AxisInput y;

        public Axis2DInput(string xAxisName, string yAxisName)
        {
            x = new AxisInput(xAxisName);
            y = new AxisInput(yAxisName);
        }
    }

    public interface IInputManager : IService
    {
        Axis2DInput move { get; }
        ButtonInput jump { get; }
    }
}

