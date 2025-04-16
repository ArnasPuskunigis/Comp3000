using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomMobileInput
{
    public static FixedJoystick joystickLeft;
    public static FixedJoystick joystickRight;
    public static float GetAxis(string axisName)
    {
        if (joystickLeft != null)
        {
            if (axisName == "Horizontal") return joystickLeft.Horizontal;
            if (axisName == "Vertical") return joystickLeft.Vertical;
        }

        return Input.GetAxis(axisName);
    }

    public static float GetAimX()
    {
        return joystickRight.Horizontal;
    }

    public static float GetAimY()
    {
        return joystickRight.Vertical;
    }


}
