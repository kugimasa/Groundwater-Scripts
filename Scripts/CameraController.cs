using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float cameraDist = 10.0f;
    [SerializeField] float rotSpeed = 100.0f;
    [SerializeField] float elevSpeed = 0.01f;
    [SerializeField] float posY = 10.0f;
    [SerializeField] bool canRotate = false;
    // Right -1: Left 1
    [SerializeField] int rotDirection = 1;

    private enum CameraPos {LeftDown, LeftUp, RightUp, RightDown};
    CameraPos currentPos;
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        SetInitialPos();
    }
    void Update()
    {
        Rotate(rotDirection);
        Elevation();
    }
    public void SetInitialPos()
    {
        currentPos = CameraPos.LeftDown;
        transform.position = new Vector3(-1.0f * cameraDist, posY, 1.0f * cameraDist);
        transform.rotation = Quaternion.Euler(33.0f, 135.0f, 0.0f);
    }

    private void Rotate(int direction)
    {
        Vector3 originPos = new Vector3(0.0f, transform.position.y, 0.0f);
        Quaternion target;
        switch (currentPos)
        {
            case CameraPos.RightDown:
                target = Quaternion.Euler(33.0f, 45.0f, 0.0f);
                break;
            case CameraPos.RightUp:
                target = Quaternion.Euler(33.0f, 315.0f, 0.0f);
                break;
            case CameraPos.LeftUp:
                target = Quaternion.Euler(33.0f, 225.0f, 0.0f);
                break;
            case CameraPos.LeftDown:
                target = Quaternion.Euler(33.0f, 135.0f, 0.0f);
                break;
            default:
                target = Quaternion.Euler(33.0f, 45.0f, 0.0f);
                break;
        }
        if (Quaternion.Angle(transform.rotation, target) <= 1)
        {
            canRotate = false;
            transform.rotation = target;
        } else if (canRotate) {
            transform.RotateAround(Vector3.zero, direction * Vector3.up, rotSpeed * Time.deltaTime);
        }
    }

    private void Elevation()
    {
        float currentPosY = transform.position.y;
        if (currentPosY <= posY) 
        {
            transform.Translate(Vector3.up * elevSpeed, Space.World);
        }
    }

    public void ChangeState(int direction)
    {
        if (!canRotate) 
        {
            canRotate = true;
            rotDirection = direction;
            // Right Rotation
            if (direction == -1)
            {
                switch (currentPos)
                {
                    case CameraPos.LeftDown:
                        currentPos = CameraPos.RightDown;
                        break;
                    case CameraPos.RightDown:
                        currentPos = CameraPos.RightUp;
                        break;
                    case CameraPos.RightUp:
                        currentPos = CameraPos.LeftUp;
                        break;
                    case CameraPos.LeftUp:
                        currentPos = CameraPos.LeftDown;
                        break;
                }
            }
            // Left Rotation
            else 
            {
                switch (currentPos)
                {
                    case CameraPos.LeftDown:
                        currentPos = CameraPos.LeftUp;
                        break;
                    case CameraPos.LeftUp:
                        currentPos = CameraPos.RightUp;
                        break;
                    case CameraPos.RightUp:
                        currentPos = CameraPos.RightDown;
                        break;
                    case CameraPos.RightDown:
                        currentPos = CameraPos.LeftDown;
                        break;
                }
            }
        }
    }

    public void ChangePosY(float posY)
    {
        this.posY = posY;
    }
}
