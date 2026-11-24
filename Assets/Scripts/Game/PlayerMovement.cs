using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float turnSpeed = 720f;

    CharacterController cc;
    Health health;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (PauseMenu.IsPaused) return;
        if (health.IsDead) return;

        var kb = Keyboard.current;
        if (kb == null) return;

        Vector3 dir = Vector3.zero;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) dir.z += 1;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) dir.z -= 1;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) dir.x += 1;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) dir.x -= 1;
        dir = Vector3.ClampMagnitude(dir, 1f);

        cc.Move(dir * speed * Time.deltaTime);

        if (dir != Vector3.zero)
        {
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look, turnSpeed * Time.deltaTime);
        }
    }
}
