using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    public FixedJoystick joystickL;
    public FixedJoystick joystickR;

    // Start is called before the first frame update
    void Start()
    {
        CustomMobileInput.joystickLeft = joystickL;
        CustomMobileInput.joystickRight = joystickR;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
