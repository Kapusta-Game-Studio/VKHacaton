using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Rigidbody))]
    public class Brick : MonoBehaviour
    {
        public bool shouldBeKinematicAtStart;

        protected Rigidbody _rb;

        protected void Start()
        {
            _rb = GetComponent<Rigidbody>();
            if (shouldBeKinematicAtStart)
                _rb.isKinematic = true;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Brick"))
                return;
            _rb.isKinematic = false;
        }
        
        internal void ChangeKinematic(bool state) => _rb.isKinematic = state;
    }
}


