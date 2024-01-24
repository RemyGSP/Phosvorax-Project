using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    
    static private Vector3 movementDirection;
    static private Vector2 cursorPosition;
    static private bool isRolling;
    static private bool isAttacking;
    static private bool isShooting;
    public void OnMove(InputValue moveValue) 
    {
        var temporalMovementDirection = moveValue.Get<Vector2>();
        Vector3 toConvert = new Vector3(temporalMovementDirection.x, 0, temporalMovementDirection.y);
        movementDirection = IsoVectorConvert(toConvert);

    }
    public Vector3 IsoVectorConvert(Vector3 convert)
    {
        Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 skewedInput = matrix.MultiplyPoint3x4(convert);
        return skewedInput;
    }

    static public Vector3 GetPlayerInputDirection()
    {
        return movementDirection;
    }

    public void OnAim(InputValue inputValue)
    {
        cursorPosition = inputValue.Get<Vector2>();
    }
    static public Vector2 GetCursorPosition()
    {
        return cursorPosition;
    }

    public void OnFire(InputValue inputValue)
    {
        if (inputValue.isPressed)
            isShooting = true;
        else
            isShooting = false;    
    }
    static public bool IsShooting()
    {
        return isShooting;
    }


    public void OnRoll(InputValue inputValue)
    {
        if (inputValue.isPressed)
            isRolling = true;
        else 
            isRolling = false;
    }    
    static public bool IsRolling()
    {
        return isRolling;
    }

    public void OnBasicAttack(InputValue inputValue)
    {
        if (inputValue.isPressed)
            isAttacking = true;
        else 
            isAttacking = false;
    }    
    static public bool IsAttacking()
    {
        return isAttacking;
    }
    

    
}
