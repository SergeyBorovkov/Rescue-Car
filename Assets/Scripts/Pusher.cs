using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pusher : MonoBehaviour
{    
    private Animator _animator;    
    private float _pause = 2f;
    
    public bool IsDefault;
    public bool IsPushing;
    public bool CanPush = true;
        
    private void Awake()
    {        
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf == true && other.GetComponent<PusherUncatcher>() != null)
        {
            if (IsDefault == false)
            {
                IsPushing = false;

                StopPushAnimation();
                
                CanPush = false;
                Invoke(nameof(SetCanPushTrue), _pause);

                transform.parent = null;                
            }
        }
    }   

    public void PlayPushAnimation()
    {        
        _animator.SetBool(nameof(IsPushing), true);
    }

    private void StopPushAnimation()
    {
        _animator.SetBool(nameof(IsPushing), false);
    }

    public void ChangeAnimationSpeed(float speed)
    {
        _animator.speed = speed;
    }

    private void SetCanPushTrue()
    {
        CanPush = true;
    }
}