using System.Collections.Generic;
using UnityEngine;
using Yashlan.controller;
using Yashlan.enums;
using Yashlan.util;

namespace Yashlan.manage
{
    public class SpawnerManager : Singleton<SpawnerManager>
    {
        [SerializeField]
        private ProblemTypes.ProblemType _problemType;
        [SerializeField]
        private GameObject _box;
        [SerializeField]
        private Sprite[] _boxSprites;

        private List<GameObject> _boxTempList;

        private float delay;

        void Start()
        {
            if (_problemType == ProblemTypes.ProblemType.problem_6)
            {
                Spawn(false, false);
            }

            if (_problemType == ProblemTypes.ProblemType.problem_7)
            {
                Spawn(true, false);
            }

            if (_problemType == ProblemTypes.ProblemType.problem_8)
            {
                _boxTempList = new List<GameObject>();
                Spawn(true, true);
            }
        }

        private void Update()
        {
            if(_problemType == ProblemTypes.ProblemType.problem_8)
            {
                for (int i = 0; i < _boxTempList.Count; i++)
                {
                    var distance = Vector2.Distance(_boxTempList[i].transform.position, CircleController.Instance.transform.position);

                    if (distance <= CircleController.Instance.NotSpawnArea)
                    {
                        _boxTempList[i].transform.position = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), _boxTempList[i].transform.position.z);
                    }

                    if (!_boxTempList[i].activeSelf)
                    {
                        delay += Time.unscaledDeltaTime;
                        if(delay >= 3f)
                        {
                            ReSpawnBox(_boxTempList[i]);
                            delay = 0f;
                        }
                    }
                }
            }
        }

        void Spawn(bool setupBoxCollider2D, bool dontSpawnInCircleArea)
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

                if (dontSpawnInCircleArea)
                {
                    var boxTemp = Instantiate(_box, new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), _box.transform.position.z), Quaternion.identity);
                    boxTemp.SetActive(false);
                    _boxTempList.Add(boxTemp);
                    if(_boxTempList.Count == spawnLength)
                    {
                        foreach (var box in _boxTempList)
                        {
                            box.SetActive(true);
                        }
                    }
                }
                else
                    Instantiate(_box, new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), _box.transform.position.z), Quaternion.identity);
            }
        }

        #region untuk problem ke 8
        public void ReSpawnBox(GameObject _boxTemp)
        {
            _boxTemp.transform.position = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), _boxTemp.transform.position.z);
            _boxTemp.SetActive(true);
        }
        #endregion
    }
}