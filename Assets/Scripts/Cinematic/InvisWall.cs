using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cinematic
{
    public class InvisWall : MonoBehaviour
    {
        [SerializeField] private CameraController controller;
        private void OnTriggerEnter(Collider other)
        {
            Interaction.Shell shell = other.gameObject.GetComponent<Interaction.Shell>();
            if (shell)
            {
                if (!shell.wasFirstTouch)
                    controller.KillCamRemoval();
            }
            
        }
    }
}

