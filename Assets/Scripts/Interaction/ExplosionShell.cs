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
        [SerializeField] private string _explosionSound = "SmallExplosion";

        private new void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            if (collision.transform.CompareTag("Barrel"))
                return;

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, _explosionRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Environment.Brick brick = rb.GetComponent<Environment.Brick>();
                    if (brick)
                        brick.ChangeKinematic(false);

                    rb.AddExplosionForce(_explosionPower *  1000, explosionPos, _explosionRadius, _explosionShakeWave);
                }
            }
            Audio.AudioManager.Instance.PlaySound(_explosionSound, this.transform.position, voluminous: true);
            Destroy(this.gameObject);
        }
    }
}

