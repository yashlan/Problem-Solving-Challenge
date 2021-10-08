using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField]
        private GameObject _tail;
        [SerializeField]
        private bool _ate = false;
        [SerializeField]
        private bool _dead = false;

        List<Transform> tails;

        Vector2 movement;

        bool hasSpawnAreaInit = false;

        public float NotSpawnArea => _notSpawnArea;

        private IEnumerator InitSpawnArea(float newSize)
        {
            _notSpawnArea = 3f;
            yield return new WaitForSeconds(1f);
            _notSpawnArea = newSize;
            hasSpawnAreaInit = true;
        }

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

            if (_problemType == ProblemTypes.ProblemType.problem_8)
            {
                StartCoroutine(InitSpawnArea(0.8f));
            }

            if (_problemType == ProblemTypes.ProblemType.problem_9)
            {
                movement = Vector2.right;
                tails = new List<Transform>();
                StartCoroutine(InitSpawnArea(0.49f));
                InvokeRepeating(nameof(MoveAsSnake), 0.3f, 0.3f);
            }
        }

        #region untuk problem ke 9
        private void MoveAsSnake()
        {
            if (!_dead && hasSpawnAreaInit)
            {
                transform.Translate(movement);

                if (_ate)
                {
                    var newTail = Instantiate(_tail, transform.position, Quaternion.identity);
                    tails.Insert(0, newTail.transform);
                    _ate = false;
                }
                else if (tails.Count > 0)
                {
                    tails.Last().position = transform.position;
                    tails.Insert(0, tails.Last());
                    tails.RemoveAt(tails.Count - 1);
                }
            }
        }
        #endregion

        private void OnDrawGizmosSelected()
        {
            if(_problemType == ProblemTypes.ProblemType.problem_8 ||
               _problemType == ProblemTypes.ProblemType.problem_9  )
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
               _problemType == ProblemTypes.ProblemType.problem_8 ||
               _problemType == ProblemTypes.ProblemType.problem_9  )
            {
                var score_info = $"Score : {_score}";
                var guiStyle = new GUIStyle(GUI.skin.label);
                guiStyle.alignment = TextAnchor.UpperCenter;
                guiStyle.fontSize = 30;
                guiStyle.fontStyle = FontStyle.Bold;
                GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height - 650, 400, 110), score_info, guiStyle);
            }

            if(_problemType == ProblemTypes.ProblemType.problem_9)
            {
                if (_dead)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 60, 320, 120, 53), "RESTART"))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }

                    GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
                    guiStyle.alignment = TextAnchor.MiddleCenter;
                    guiStyle.fontStyle = FontStyle.Bold;
                    guiStyle.fontSize = 50;
                    GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height - 500, 400, 110), "GAME OVER", guiStyle);

                }
            }
        }

        void Update()
        {
            if(_problemType == ProblemTypes.ProblemType.problem_4 ||
               _problemType == ProblemTypes.ProblemType.problem_7  )
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            }

            if (_problemType == ProblemTypes.ProblemType.problem_8 && hasSpawnAreaInit)
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

            if(_problemType == ProblemTypes.ProblemType.problem_9)
            {
                if(transform.position.y > 4.5f || transform.position.y < -4.5f || transform.position.x > 8.5f || transform.position.x < -8.5f)
                {
                    _dead = true;
                }

                if (!_dead)
                {
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                        movement = Vector2.right;
                    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                        movement = -Vector2.up;
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                        movement = -Vector2.right;
                    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                        movement = Vector2.up;
                }
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
                if (_problemType == ProblemTypes.ProblemType.problem_7)
                {
                    _score++;
                    collision.gameObject.SetActive(false);
                }

                if (_problemType == ProblemTypes.ProblemType.problem_8 || 
                    _problemType == ProblemTypes.ProblemType.problem_9  )
                {
                    if (hasSpawnAreaInit)
                    {
                        _score++;
                        collision.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}