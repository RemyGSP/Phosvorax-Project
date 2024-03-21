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
    private bool isInteracting;
    private bool isShooting;
    private bool isUsingAbility1;
    private bool isUsingAbility2;
    private bool isKeyboard;
    private bool isGamepad;
    private bool isCanceling;
    private bool isPausing;
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

    public void OnPause()
    {
        isPausing = true;
    }

    public bool IsPausing()
    {
        return isPausing;
    }
    public void HasPaused()
    {
        isPausing = false;
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

   
    public void OnInteract()
    {
        isInteracting = true;
    }

    //Returns true if interact and makes it false so it doesnt execute twice
    public bool TryToInteract()
    {
        bool aux = false;
        if (isInteracting)
        {
            aux = true;
            isInteracting = false;
        }
        return aux;
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
    

    public void OnAbility1(InputValue inputValue)
    {
        if (!inputValue.isPressed)
        {
            isUsingAbility1 = false;
        }
        else
        {
            abilityPressed = 1;
            isUsingAbility1 = true;
        }
    }
    public void OnAbility2(InputValue inputValue)
    {
        if (!inputValue.isPressed)
        {
            isUsingAbility2 = false;
        }
        else
        {
            abilityPressed = 2;
            isUsingAbility2 = true;
        }
    }

    public bool IsUsingAbility1()
    {
        return isUsingAbility1;
    }
    public bool IsUsingAbility2()
    {
        return isUsingAbility2;
    }

    public int GetCurrentAbility()
    {
        return abilityPressed;
    }

    public void StopUsingAbility()
    {
        isUsingAbility1 = false;
        isUsingAbility2 = false;
    }

    public void OnControlsChanged(PlayerInput playerInput)
    {
        //Debug.Log("Current control scheme: " + playerInput.currentControlScheme);

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

