using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class ExplosionShell : Shell
    {
        [SerializeField] private float _explosionPower = 20;
        [SerializeField] private float _explosionRadius = 1f;
        [SerializeField] private float _explosionShakeWave = 3.0f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Barrel"))
                return;

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, _explosionRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(_explosionPower*1000, explosionPos, _explosionRadius, _explosionShakeWave);
            }
            Destroy(this.gameObject);
        }
    }
}
