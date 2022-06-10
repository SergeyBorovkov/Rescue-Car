using UnityEngine;

public class PusherUncatcher : MonoBehaviour
{
    private bool _canShake = true;
    private float _pause = 5f;

    public bool CanShake => _canShake;

    public void RestartShakingCapability()
    {
        _canShake = false;
        Invoke(nameof(SetCanShakeTrue), _pause);
    }

    private void SetCanShakeTrue()
    {
        _canShake = true;
    }
}