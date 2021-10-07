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

        void Start() => _rb.velocity = new Vector2(_speed, _rb.velocity.y);

        void OnGUI()
        {
            var speed_info = $"Move Speed : {_rb.velocity.magnitude}f";
            var guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.MiddleCenter;
            guiStyle.fontSize = 20;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), speed_info, guiStyle);
        }
    }
}