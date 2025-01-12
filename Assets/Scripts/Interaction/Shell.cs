using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    internal enum TypeOfShell
    {
        Common = 0,
        Explosive = 1,
    }

    [System.Serializable]
    internal struct GunAvalibleShell
    {
        [SerializeField] internal List<GameObject> sourcePrefabs;
        [SerializeField] internal List<TypeOfShell> types;
    }

    public class Shell : MonoBehaviour
    {
        public Transform killCamPos;
        [SerializeField] protected Rigidbody _rb;

        private Cinematic.CameraController _camContrl;
        private bool _isCamFocused;

        internal Rigidbody GetRb()
        {
            return _rb;
        }

        internal void SetupKillCam()
        {
            if (!killCamPos)
            {
                GameObject obj = new GameObject();
                obj.transform.position = this.transform.position;
                obj.transform.parent = this.transform;
                obj.transform.Translate(0, 0.5f, 0);
                obj.name = "Generated KillCamPos";

                killCamPos = obj.transform;
            }

            _camContrl.KillCamShow(killCamPos, transform);
            _isCamFocused = true;
        }

        protected void Awake()
        {
            if (!_rb)
                try
                {
                    TryGetComponent<Rigidbody>(out _rb);
                }
                catch
                {
                    throw new System.Exception("No rigidbody on a shell!");
                }

            _camContrl = GameObject.FindObjectOfType<Cinematic.CameraController>();
            _isCamFocused = false;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Barrel"))
                return;

            if (_isCamFocused)
            {
                _camContrl.KillCamRemoval();
                _isCamFocused = false;
            }
        }
        protected void OnDestroy()
        {
            if (_isCamFocused)
                _camContrl.KillCamRemoval();
        }
    }
}

