using UnityEngine;

namespace Gem_Scripts
{
    public class GemRotate : MonoBehaviour
    {
        private float speed = 0.5f;
        private float angle;
        void Update()
        {
            angle = (angle + speed) % 360f;
            transform.localRotation = Quaternion.Euler(new Vector3(45f, angle, 0f));
        }
    }
}
