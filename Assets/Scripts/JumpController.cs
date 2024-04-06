using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class JumpController : MonoBehaviour
{
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float groundAngle = 45;
    [SerializeField] private float jumpForce;

    private int _currentJumps;
    private Rigidbody _rigidbody;
    private HashSet<Collider> _currentGrounds = new();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnJump(InputValue _)
    {
        if (_currentJumps < maxJumps) Jump();
    }

    private void Jump()
    {
        _currentJumps++;
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        TryUpdateGrounded(other);
    }

    private void OnCollisionStay(Collision other)
    {
        TryUpdateGrounded(other);
    }

    private void OnCollisionExit(Collision other)
    {
        TryUpdateGrounded(other);
    }

    private void TryUpdateGrounded(Collision other)
    {
        if (!IsGroundCollision(other))
        {
            TryUnGround(other);
            return;
        }
        
        _currentJumps = 0;
        _currentGrounds.Add(other.collider);
    }

    private void TryUnGround(Collision other)
    {
        if (_currentGrounds.Remove(other.collider) && _currentGrounds.Count < 1 && _currentJumps == 0) _currentJumps = 1;
    }

    private bool IsGroundCollision(Collision other)
    {
        foreach (var point in other.contacts)
        {
            var angle = Vector3.Angle(Vector3.up, point.normal);
            if (angle <= groundAngle) return true;
        }

        return false;
    }
}