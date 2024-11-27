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
        [SerializeField] internal GameObject barrel;

        [Header("Shooting options")]

        [SerializeField] private float _shootingPower;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private Transform _explosionPosition;

        [SerializeField] private string _shotingSound;

        internal void Shoot(Shell shell = null)
        {
            if (_shotingSound != string.Empty)
                Audio.AudioManager.Instance.PlaySound(_shotingSound, this.transform.position, voluminous: true);
            Shell sh = !shell ? Instantiate(_shellPrefab, _shellSpawnPos).GetComponent<Shell>() : shell;
            sh.transform.parent = null;
            sh.GetRb().AddExplosionForce(_shootingPower*1000, _explosionPosition.position, _explosionRadius);
        }

        internal void ChangeShell(GameObject shell) => _shellPrefab = shell;

        private IEnumerator WaitAndShoot(float time)
        {
            yield return new WaitForSeconds(time);
            Shoot();
        }
    }
}

