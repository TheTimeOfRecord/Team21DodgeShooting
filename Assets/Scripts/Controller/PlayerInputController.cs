using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : DodgeController
{
    private Camera _camera;
    private Vector2 previousAim = Vector2.zero;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>().normalized;
        CallMoveEvent(inputDir);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;
        
        if(newAim.magnitude >= 0.9f)
        {
            //좀 더 부드러운 회전을 가능케 한다고 합니다.
            CallLookEvent(Vector2.Lerp(previousAim, newAim, 0.5f));
            previousAim = newAim;
        }
    }

    public void OnFire(InputValue value)
    {
        isAttacking = value.isPressed;
    }
    public void OnBomb()
    {
        CallBombEvent();
    }
}
