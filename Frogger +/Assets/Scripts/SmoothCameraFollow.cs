using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
        #region Variables
        private Vector3 _offset;
        // target is the player
        [SerializeField] private Transform target;
        // smoothTime is the delay of the camera following the player
        [SerializeField] private float smoothTime;
        private Vector3 _currentVelocity = Vector3.zero;
        private void Awake() => _offset = transform.position - target.position;
        #endregion

        #region Unity callbacks
        private void LateUpdate()
        {
            Vector3 targetPosition = target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        }
        
        #endregion
}