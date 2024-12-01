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

            Quaternion gyroPos = GyroToUnity(Input.gyro.attitude);
            //barrel.transform.localRotation = new Quaternion(gyroPos.x, gyroPos.y, barrel.transform.localRotation.z, barrel.transform.localRotation.w);
        }

        public void TryToShoot()
        {
            // Put here logic of check stars challenges

            if (!gun)
                throw new System.Exception("No gun chosen!");
            gun.Shoot();
        }
        
        private static Quaternion GyroToUnity(Quaternion q)
        {
            return new Quaternion(q.x, q.y, -q.z, -q.w);
        }
    }
}


