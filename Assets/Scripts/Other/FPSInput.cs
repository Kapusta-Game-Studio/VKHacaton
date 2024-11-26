using UnityEngine;
using System.Collections;

namespace Movement
{
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Control Script/FPS Input")]
    public class FPSInput : MonoBehaviour
    {
        public float speed = 6.0f;

        private CharacterController _charController;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _charController = GetComponent<CharacterController>();

        }

        void Update()
        {
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);


            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charController.Move(movement);

        }
    }
}