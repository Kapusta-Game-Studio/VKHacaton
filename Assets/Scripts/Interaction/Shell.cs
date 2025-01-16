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
        private const float CAM_SEPARATION_DISTANCE = 0.5f;


        public Transform killCamPos;
        [SerializeField] protected Rigidbody _rb;

        internal bool wasFirstTouch;

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
            wasFirstTouch = false;
        }

        protected void Update()
        {
            if (!_isCamFocused || wasFirstTouch)
                return;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, _rb.velocity, out hit, CAM_SEPARATION_DISTANCE))
            {
                if (!hit.transform.CompareTag("Barrel"))
                    SeparateCamera();
            }
        }

        protected void SeparateCamera()
        {
            wasFirstTouch = true;

            if (_isCamFocused)
            {
                _camContrl.KillCamRemoval();
                _isCamFocused = false;
            }
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Barrel") || wasFirstTouch || !_isCamFocused)
                return;
            _camContrl.FocusCam(transform);
            SeparateCamera();
        }
    }
}

