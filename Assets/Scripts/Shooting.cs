using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class Shooting : MonoBehaviour
    {
        [Header("Basic options")]

        [SerializeField] private Transform _shellSpawnPos;
        [SerializeField] private GameObject _shellPrefab;

        [Header("Shooting options")]

        [SerializeField] private float _shootingPower;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private Transform _explosionPosition;

        private void Start()
        {
            StartCoroutine(WaitAndShoot(2));
        }

        private void Shoot(Shell shell = null)
        {
            Shell sh = !shell ? Instantiate(_shellPrefab, _shellSpawnPos).GetComponent<Shell>() : shell;
            sh.GetRb().AddExplosionForce(_shootingPower*1000, _explosionPosition.position, _explosionRadius);
        }

        private IEnumerator WaitAndShoot(float time)
        {
            yield return new WaitForSeconds(time);
            Shoot();
        }
    }
}

