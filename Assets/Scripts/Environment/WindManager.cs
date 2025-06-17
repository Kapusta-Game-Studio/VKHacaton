using UnityEngine;

namespace Environment
{
    public class WindManager : MonoBehaviour
    {
        public static Vector3 GlobalWindDirection { get; private set; }
        public static float GlobalWindStrength { get; private set; }

        [SerializeField] private Vector3 windDirection = Vector3.forward;
        [SerializeField] [Range(0, 100)] private float windStrength = 5f;
        [SerializeField] private float windChangeFrequency = 30f;

        private void Start()
        {
            UpdateWind();
            InvokeRepeating(nameof(UpdateWind), windChangeFrequency, windChangeFrequency);
        }

        private void UpdateWind()
        {
            windDirection = Quaternion.Euler(
                Random.Range(-10f, 10f),
                Random.Range(-180f, 180f),
                Random.Range(-10f, 10f)
            ) * windDirection;

            windStrength = Mathf.Clamp(windStrength + Random.Range(-1f, 1f), 1f, 20f);

            GlobalWindDirection = windDirection.normalized;
            GlobalWindStrength = windStrength;
        }
    }
}
