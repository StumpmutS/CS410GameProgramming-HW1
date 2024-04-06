using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject winTextObject;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private int winCount;
    [SerializeField] private float moveSpeed;

    private int _count;
    private Rigidbody _rigidbody;
    private float _movementX;
    private float _movementY;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        winTextObject.SetActive(false);
        SetCountText();
    }

    public void OnMove(InputValue movementValue)
    {
        var movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        var movement = new Vector3(_movementX, 0, _movementY);

        _rigidbody.AddForce(movement * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            _count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = $"Count: {_count}";

        if (_count >= winCount)
        {
            winTextObject.SetActive(true);
        }
    }
}