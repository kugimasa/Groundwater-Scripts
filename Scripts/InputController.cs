using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public static bool UP_KEY, DOWN_KEY, RIGHT_KEY, LEFT_KEY, 
                        UP_KEY_UP, DOWN_KEY_UP, RIGHT_KEY_UP, LEFT_KEY_UP,
                        UP_KEY_DOWN, DOWN_KEY_DOWN, RIGHT_KEY_DOWN, LEFT_KEY_DOWN,
                        JUMP_KEY, CAMERA_L_KEY, CAMERA_R_KEY = false;

    // Update is called once per frame
    void Update()
    {
        UP_KEY = Input.GetKey(KeyCode.W);
        DOWN_KEY = Input.GetKey(KeyCode.S);
        RIGHT_KEY = Input.GetKey(KeyCode.D);
        LEFT_KEY = Input.GetKey(KeyCode.A);

        UP_KEY_UP = Input.GetKeyUp(KeyCode.W);
        DOWN_KEY_UP = Input.GetKeyUp(KeyCode.S);
        RIGHT_KEY_UP = Input.GetKeyUp(KeyCode.D);
        LEFT_KEY_UP = Input.GetKeyUp(KeyCode.A);

        UP_KEY_DOWN = Input.GetKeyDown(KeyCode.W);
        DOWN_KEY_DOWN = Input.GetKeyDown(KeyCode.S);
        RIGHT_KEY_DOWN = Input.GetKeyDown(KeyCode.D);
        LEFT_KEY_DOWN = Input.GetKeyDown(KeyCode.A);

        JUMP_KEY = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.K);
        CAMERA_L_KEY = Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.J);
        CAMERA_R_KEY = Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.L);
    }
}
