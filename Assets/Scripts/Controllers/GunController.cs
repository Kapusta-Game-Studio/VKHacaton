using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private float _maxXRot;
        [SerializeField] private float _maxYRot;
        [SerializeField] private float _rotAngle;

        internal Interaction.Shooting gun;
        internal GameObject barrel;

        private float _rotationY = 0;
        private float _rotationX = 0;

        private void Update()
        {
            if (!barrel)
                return;

            if (Input.GetKey(KeyCode.Q))
            {
                _rotationY -=  _rotAngle;
                _rotationY = Mathf.Clamp(_rotationY, -_maxXRot, _maxXRot);
                barrel.transform.localEulerAngles = new Vector3(barrel.transform.localEulerAngles.x, _rotationY, barrel.transform.localEulerAngles.z);
            }

            else if (Input.GetKey(KeyCode.E))
            {
                _rotationY += _rotAngle;
                _rotationY = Mathf.Clamp(_rotationY, -_maxXRot, _maxXRot);
                barrel.transform.localEulerAngles = new Vector3(barrel.transform.localEulerAngles.x, _rotationY, barrel.transform.localEulerAngles.z);
            }

            else if (Input.GetKey(KeyCode.G))
            {
                _rotationX += _rotAngle;
                _rotationX = Mathf.Clamp(_rotationX, -_maxYRot, _maxYRot);
                barrel.transform.localEulerAngles = new Vector3(_rotationX, barrel.transform.localEulerAngles.y, barrel.transform.localEulerAngles.z);
            }
            else if (Input.GetKey(KeyCode.T))
            {
                _rotationX -= _rotAngle;
                _rotationX = Mathf.Clamp(_rotationX, -_maxYRot, _maxYRot);
                barrel.transform.localEulerAngles = new Vector3(_rotationX, barrel.transform.localEulerAngles.y, barrel.transform.localEulerAngles.z);
            }
        }

        public void TryToShoot()
        {
            // Put here logic of check stars challenges

            if (!gun)
                throw new System.Exception("No gun chosen!");
            gun.Shoot();
        }
    }
}


