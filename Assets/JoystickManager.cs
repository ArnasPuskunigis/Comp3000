using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    public FixedJoystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        CustomMobileInput.joystick = joystick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
