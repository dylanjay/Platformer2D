using UnityEngine;
using DylanJay.Framework;

namespace DylanJay.Services
{
    public class InputManager : IInputManager
    {
        Axis2DInput _move = new Axis2DInput("Horizontal", "Vertical");
        Axis2DInput IInputManager.move
        {
            get
            {
                return _move;
            }
        }

        ButtonInput _jump = new ButtonInput("Jump");
        ButtonInput IInputManager.jump
        {
            get
            {
                return _jump;
            }
        }
    }
}
