using UnityEngine;
using Yashlan.enums;

namespace Yashlan.manage
{
    #region untuk problem 6
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
                Spawn();
            }
        }

        void Spawn()
        {
            var spawnLength = Random.Range(5, 15);
            for (int i = 0; i < spawnLength; i++)
            {
                var sr = _box.GetComponent<SpriteRenderer>();
                sr.sprite = _boxSprites[Random.Range(0, _boxSprites.Length)];
                Instantiate(_box, new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), _box.transform.position.z), Quaternion.identity);
            }
        }
    }
    #endregion
}