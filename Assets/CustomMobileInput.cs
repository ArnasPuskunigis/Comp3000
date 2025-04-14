using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomMobileInput
{
    public static FixedJoystick joystick;

    public static float GetAxis(string axisName)
    {
        if (joystick != null)
        {
            if (axisName == "Horizontal") return joystick.Horizontal;
            if (axisName == "Vertical") return joystick.Vertical;
        }

        return Input.GetAxis(axisName);
    }

}
