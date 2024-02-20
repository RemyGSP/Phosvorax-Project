using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputController : MonoBehaviour
{

    static public PlayerInputController Instance { get; private set; }
    private Vector3 movementDirection;
    private Vector2 cursorPosition;
    private bool isRolling;
    private bool isAttacking;
    private bool isShooting;
    private bool isUsingAbility;
    private bool isKeyboard;
    private bool isGamepad;
    private bool isCanceling;
    private int abilityPressed;
    private void Start()
    {

        abilityPressed = 1;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Este singleton esta ya puesto en escena: " + gameObject.name);
            Destroy(gameObject);
        }

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

    public void OnCancel(InputValue inputValue)
    {
        if (inputValue.isPressed)
            isCanceling = true;
        else
            isCanceling = false;
    }

    public bool IsCanceling()
    {
        return isCanceling;
    }
    public bool IsAttacking()
    {
        return isAttacking;
    }

    public void OnAbility1(InputValue inputValue)
    {
        if (!inputValue.isPressed)
        {
            isUsingAbility = false;
        }
        else
        {
            abilityPressed = 1;
            isUsingAbility = true;
        }
    }
    public void OnAbility2(InputValue inputValue)
    {
        if (!inputValue.isPressed)
        {
            isUsingAbility = false;
        }
        else
        {
            abilityPressed = 2;
            isUsingAbility = true;
        }
    }
    public void OnAbility3(InputValue inputValue)
    {
        if (!inputValue.isPressed)
        {
            isUsingAbility = false;
        }
        else
        {
            abilityPressed = 3;
            isUsingAbility = true;
        }
    }


    public bool IsUsingAbility()
    {
        return isUsingAbility;
    }

    public int GetCurrentAbility()
    {
        return abilityPressed;
    }

    public void StopUsingAbility()
    {
        isUsingAbility = false;
    }

    public void OnControlsChanged(PlayerInput playerInput)
    {
        Debug.Log("Current control scheme: " + playerInput.currentControlScheme);

        if (playerInput.currentControlScheme.Equals("Keyboard"))
        {
            isKeyboard = true;
            isGamepad = false;
        }
        if (playerInput.currentControlScheme.Equals("Gamepad"))
        {
            isGamepad = true;
            isKeyboard = false;
        }
        else
        {
            Debug.LogWarning("Unknown control scheme: " + playerInput.currentControlScheme);
        }
    }

    public bool IsUsingKeyboard()
    {
        return isKeyboard;
    }

    public bool IsUsingGamepad()
    {
        return isGamepad;
    }
}

