using UnityEngine;

namespace Yashlan.controller 
{
    public enum ProblemType
    {
        problem_2,
        problem_3,
        problem_4,
        problem_5,
        problem_6,
        problem_7,
        problem_8,
        problem_9,
        problem_10,
    }
    public class CircleController : MonoBehaviour
    {
        [SerializeField]
        private ProblemType _problemType;
        [SerializeField]
        private Rigidbody2D _rb;
        [SerializeField]
        private float _speed;

        Vector2 movement;


        void Start() 
        {
            if(_problemType == ProblemType.problem_2)
            {
                _speed = 2f;
                _rb.velocity = new Vector2(_speed, _rb.velocity.y);
            }

            if (_problemType == ProblemType.problem_3)
            {
                _speed = 1000f;
                _rb.AddForce(new Vector2(_speed, _speed));
            }
        } 

        void OnGUI()
        {
            if (_problemType == ProblemType.problem_2 || _problemType == ProblemType.problem_3)
            {
                var speed_info = $"Move Speed : {_rb.velocity.magnitude}f";
                var guiStyle = new GUIStyle(GUI.skin.textArea);
                guiStyle.alignment = TextAnchor.MiddleCenter;
                guiStyle.fontSize = 20;
                GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), speed_info, guiStyle);
            }
        }

        void Update()
        {
            if(_problemType == ProblemType.problem_4)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            }

            if(_problemType == ProblemType.problem_5)
            {
                _speed = 0.1f;
                var mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.position = Vector2.Lerp(transform.position, mousePosition, _speed);
            }
        }

        void FixedUpdate()
        {
            if (_problemType == ProblemType.problem_4)
                _rb.MovePosition(_rb.position + movement * _speed * Time.fixedDeltaTime);
        }
    }
}