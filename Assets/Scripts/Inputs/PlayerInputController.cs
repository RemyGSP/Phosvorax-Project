using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    static public PlayerInputController Instance { get; private set; }
    private Vector3 movementDirection;
    private Vector2 cursorPosition;
    private bool isRolling;
    private bool isAttacking;
    private bool isShooting;
    private bool isUsingAbility;
    private int abilityPressed;

    private void Start()
    {
        Instance = this;
    }

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

    public Vector3 GetPlayerInputDirection()
    {
        return movementDirection;
    }

    public void OnAim(InputValue inputValue)
    {
        cursorPosition = inputValue.Get<Vector2>();
    }
    public Vector2 GetCursorPosition()
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
    public bool IsShooting()
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
    public bool IsRolling()
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
    public bool IsAttacking()
    {
        return isAttacking;
    }
    
    public void OnAbility1(InputValue inputValue)
    {
        if (inputValue.isPressed)
            isUsingAbility = true;
        else
            isUsingAbility = false;
        abilityPressed = 1;
    }

    public bool IsUsingAbility()
    {
        return isUsingAbility;
    }

    public int GetCurrentAbility()
    {
        return abilityPressed;
    }
    
}
