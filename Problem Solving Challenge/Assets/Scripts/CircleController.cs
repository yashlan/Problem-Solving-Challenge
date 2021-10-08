using UnityEngine;
using Yashlan.enums;
using Yashlan.util;

namespace Yashlan.controller 
{
    public class CircleController : Singleton<CircleController>
    {
        [SerializeField]
        private ProblemTypes.ProblemType _problemType;
        [SerializeField]
        private Rigidbody2D _rb;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private int _score;
        [SerializeField]
        private float _notSpawnArea;

        Vector2 movement;

        public int Score
        {
            set => _score = value;
            get => _score;
        }

        public float NotSpawnArea => _notSpawnArea;

        void Start() 
        {
            if(_problemType == ProblemTypes.ProblemType.problem_2)
            {
                _speed = 2f;
                _rb.velocity = new Vector2(_speed, _rb.velocity.y);
            }

            if (_problemType == ProblemTypes.ProblemType.problem_3)
            {
                _speed = 1000f;
                _rb.AddForce(new Vector2(_speed, _speed));
            }
        }

        private void OnDrawGizmosSelected()
        {
            if(_problemType == ProblemTypes.ProblemType.problem_8)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, _notSpawnArea);
            }
        }

        void OnGUI()
        {
            if (_problemType == ProblemTypes.ProblemType.problem_2 || 
                _problemType == ProblemTypes.ProblemType.problem_3  )
            {
                var speed_info = $"Move Speed : {_rb.velocity.magnitude}f";
                var guiStyle = new GUIStyle(GUI.skin.textArea);
                guiStyle.alignment = TextAnchor.MiddleCenter;
                guiStyle.fontSize = 20;
                GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), speed_info, guiStyle);
            }

            if(_problemType == ProblemTypes.ProblemType.problem_7 ||
               _problemType == ProblemTypes.ProblemType.problem_8  )
            {
                var score_info = $"Score : {_score}";
                var guiStyle = new GUIStyle(GUI.skin.label);
                guiStyle.alignment = TextAnchor.UpperCenter;
                guiStyle.fontSize = 30;
                guiStyle.fontStyle = FontStyle.Bold;
                GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height - 650, 400, 110), score_info, guiStyle);
            }
        }

        void Update()
        {
            if(_problemType == ProblemTypes.ProblemType.problem_4 || 
               _problemType == ProblemTypes.ProblemType.problem_7 ||
               _problemType == ProblemTypes.ProblemType.problem_8  )
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            }

            if(_problemType == ProblemTypes.ProblemType.problem_5)
            {
                _speed = 0.1f;
                var mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.position = Vector2.Lerp(transform.position, mousePosition, _speed);
            }
        }

        void FixedUpdate()
        {
            if (_problemType == ProblemTypes.ProblemType.problem_4 ||
                _problemType == ProblemTypes.ProblemType.problem_7 ||
                _problemType == ProblemTypes.ProblemType.problem_8  )
            {
                _speed = 5;
                _rb.MovePosition(_rb.position + movement * _speed * Time.fixedDeltaTime);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "box(Clone)")
            {
                _score++;
                collision.gameObject.SetActive(false);
            }
        }
    }
}