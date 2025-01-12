using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    internal enum TypeOfShell
    {
        Common = 0,
        Explosive = 1,
    }

    [System.Serializable]
    internal struct GunAvalibleShell
    {
        [SerializeField] internal List<GameObject> sourcePrefabs;
        [SerializeField] internal List<TypeOfShell> types;
    }

    public class Shell : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rb;

        protected void Awake()
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

