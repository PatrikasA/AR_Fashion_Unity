using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinAnimationManager : MonoBehaviour
{
    public static MannequinAnimationManager Instance;

    [SerializeField] private Animator anim;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    public void PlayAnimation(string animationName)
    {
        Debug.LogError(animationName + " <--------------------------------- THIS THIS");
        anim.Play(animationName);
    }

}
