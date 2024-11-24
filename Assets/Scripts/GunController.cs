using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class GunController : MonoBehaviour
    {
        internal Interaction.Shooting gun;

        public void TryToShoot()
        {
            // Put here logic of check stars challenges

            if (!gun)
                throw new System.Exception("No gun chosen!");
            gun.Shoot();
        }
    }
}


