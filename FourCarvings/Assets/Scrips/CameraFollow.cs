using UnityEngine;

namespace FourCarvings
{
    /// <summary>
    /// ¬Û¾÷¸òÀH¥D¨¤
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        private Vector3 offset;

        private void Start()
        {
            
            offset = target.position - this.transform.position;
        }

        private void Update()
        {
            this.transform.position = target.position - offset;
        }
    }
}
