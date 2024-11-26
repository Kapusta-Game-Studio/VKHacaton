using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Rigidbody))]
    public class Brick : MonoBehaviour
    {
        public bool shouldBeKinematicAtStart;

        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            if (shouldBeKinematicAtStart)
                _rb.isKinematic = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Brick"))
                _rb.isKinematic = false;
        }
        
        internal void ChangeKinematic(bool state) => _rb.isKinematic = state;
    }
}


