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

        void Start()
        {
            if (_problemType == ProblemType.problem_2)
            {
                _speed = 2f;
                _rb.velocity = new Vector2(_speed, _rb.velocity.y);
            }
        }

        void OnGUI()
        {
            if (_problemType == ProblemType.problem_2)
            {
                var speed_info = $"Move Speed : {_rb.velocity.magnitude}f";
                var guiStyle = new GUIStyle(GUI.skin.textArea);
                guiStyle.alignment = TextAnchor.MiddleCenter;
                guiStyle.fontSize = 20;
                GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), speed_info, guiStyle);
            }
        }
    }
}