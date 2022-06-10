using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class CameraShakerController : MonoBehaviour
{
    [SerializeField] private ShakeData _shakeData;      

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf == true && other.TryGetComponent<PusherUncatcher>(out PusherUncatcher pusherUncatcher) && pusherUncatcher.CanShake)
        {
            CameraShakerHandler.Shake(_shakeData);

            pusherUncatcher.RestartShakingCapability();
        }
    }
}