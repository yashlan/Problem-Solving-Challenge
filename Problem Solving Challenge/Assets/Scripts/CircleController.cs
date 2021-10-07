using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yashlan.controller 
{
    public class CircleController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rb;
        [SerializeField]
        private float _speed;

        Vector2 movement;

        void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        void FixedUpdate() => 
            _rb.MovePosition(_rb.position + movement * _speed * Time.fixedDeltaTime);
    }
}