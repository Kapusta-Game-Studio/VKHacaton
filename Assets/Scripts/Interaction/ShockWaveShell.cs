using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class ShockWaveShell : Shell
    {
        [SerializeField] private float _shockWavePower = 20;
        [SerializeField] private float _shockWaveRadius = 1f;
        [SerializeField] private float _shockWaveShakeWave = 3.0f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Barrel"))
                return;

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, _shockWaveRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Environment.Brick brick = rb.GetComponent<Environment.Brick>();
                    if (brick)
                        brick.ChangeKinematic(false);

                    rb.AddExplosionForce(_shockWavePower * 100, explosionPos, _shockWaveRadius, _shockWaveShakeWave);
                }
            }
        }
    }
}

