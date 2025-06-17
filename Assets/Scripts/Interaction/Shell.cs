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
        [SerializeField] private List<string> _hitSounds;

        internal bool wasFirstTouch;

        private Cinematic.CameraController _camContrl;
        private bool _isCamFocused;

        private Vector3 _prevCamPos;
        private Quaternion _prevCamRot;
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
            _rb.AddRelativeForce(Environment.WindManager.GlobalWindDirection * Environment.WindManager.GlobalWindStrength);
        }

        protected void LateUpdate()
        {
            if (!_isCamFocused || wasFirstTouch)
                return;

            _prevCamPos = killCamPos.position;
            _prevCamRot = killCamPos.rotation;
        }

        protected void SeparateCamera()
        {
            wasFirstTouch = true;
            
            _camContrl.KillCamRemoval();
            _camContrl.FixCam(_prevCamPos, _prevCamRot);
            _isCamFocused = false;

        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Barrel") || wasFirstTouch || !_isCamFocused)
                return;

            if (_hitSounds.Count > 0)
                Audio.AudioManager.Instance.PlaySound(_hitSounds[Random.Range(0, _hitSounds.Count)], transform.position, true, _rb.velocity.magnitude/4);

            MeshDeformation deform = null;
            collision.gameObject.TryGetComponent<MeshDeformation>(out deform);
            deform?.AddDeformation(collision.GetContact((int)(collision.contactCount/2)).point,
                0.1f,
                _rb.velocity.magnitude);

            SeparateCamera();
        }
    }
}

