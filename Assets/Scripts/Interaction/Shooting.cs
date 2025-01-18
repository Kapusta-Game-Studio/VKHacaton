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
        [SerializeField] private List<string> _shotingSound;

        internal void Shoot(Shell shell = null)
        {
            if (_shotingSound.Count > 0)
                Audio.AudioManager.Instance.PlaySound(_shotingSound[Random.Range(0, _shotingSound.Count)], this.transform.position, voluminous: true);
            Shell sh = !shell ? Instantiate(_shellPrefab, _shellSpawnPos).GetComponent<Shell>() : shell;
            sh.transform.parent = null;
            sh.GetRb().AddForce(_shellSpawnPos.forward * _shootingPower * 20, ForceMode.Impulse);
            sh.SetupKillCam();
        }

        internal void ChangeShell(GameObject shell) => _shellPrefab = shell;

        private IEnumerator WaitAndShoot(float time)
        {
            yield return new WaitForSeconds(time);
            Shoot();
        }
    }
}

