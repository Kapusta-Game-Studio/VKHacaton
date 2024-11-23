using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    internal enum TypeOfShell
    {
        Common = 0,
    }

    public class Shell : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;


        private void Awake()
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
        }

        internal Rigidbody GetRb()
        {
            return _rb;
        }
    }
}

