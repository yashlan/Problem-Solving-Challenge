using UnityEngine;
using Yashlan.enums;

namespace Yashlan.manage
{
    public class SpawnerManager : MonoBehaviour
    {
        [SerializeField]
        private ProblemTypes.ProblemType _problemType;
        [SerializeField]
        private GameObject _box;
        [SerializeField]
        private Sprite[] _boxSprites;

        void Start()
        {
            if (_problemType == ProblemTypes.ProblemType.problem_6)
            {
                Spawn(false);
            }

            if (_problemType == ProblemTypes.ProblemType.problem_7)
            {
                Spawn(true);
            }
        }

        void Spawn(bool setupBoxCollider2D)
        {
            var spawnLength = Random.Range(5, 15);
            for (int i = 0; i < spawnLength; i++)
            {
                var sr = _box.GetComponent<SpriteRenderer>();
                sr.sprite = _boxSprites[Random.Range(0, _boxSprites.Length)];
                if (setupBoxCollider2D)
                {
                    var boxCollider = _box.GetComponent<BoxCollider2D>();
                    boxCollider.size = sr.bounds.size / 2;
                    boxCollider.offset = new Vector2(0, 0);
                }
                Instantiate(_box, new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), _box.transform.position.z), Quaternion.identity);
            }
        }
    }
}