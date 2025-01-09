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

        private void Start()
        {
            Input.gyro.enabled = true;
        }

        private void Update()
        {
            if (!barrel)
                return;

            if (Input.GetKey(KeyCode.W))
            {
                _rotationY += _rotAngle * Time.deltaTime;
                _rotationY = Mathf.Clamp(_rotationY, -_maxYRot, _maxYRot);

                barrel.transform.localEulerAngles = new Vector3(_rotationY, barrel.transform.localEulerAngles.y, barrel.transform.localEulerAngles.z);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _rotationY -= _rotAngle * Time.deltaTime;
                _rotationY = Mathf.Clamp(_rotationY, -_maxYRot, _maxYRot);

                barrel.transform.localEulerAngles = new Vector3(_rotationY, barrel.transform.localEulerAngles.y, barrel.transform.localEulerAngles.z);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _rotationX -= _rotAngle * Time.deltaTime;
                _rotationX = Mathf.Clamp(_rotationX, -_maxXRot, _maxXRot);

                gun.transform.localEulerAngles = new Vector3(gun.transform.localEulerAngles.x, _rotationX, gun.transform.localEulerAngles.z);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _rotationX += _rotAngle * Time.deltaTime;
                _rotationX = Mathf.Clamp(_rotationX, -_maxXRot, _maxXRot);

                gun.transform.localEulerAngles = new Vector3(gun.transform.localEulerAngles.x, _rotationX, gun.transform.localEulerAngles.z);
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


