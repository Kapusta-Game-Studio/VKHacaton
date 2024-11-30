using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Rigidbody))]
    public class Troops : Brick
    {
        private bool _onceActivated;

        private void Awake() => _onceActivated = false;

        private new void OnCollisionEnter(Collision collision)
        {
            if (_onceActivated)
                return;
            _onceActivated = true;
            base.OnCollisionEnter(collision);
            GameObject.FindObjectOfType<Controllers.ProgressController>().TargetDestroyed();
            Destroy(this);
        }
    }
}


