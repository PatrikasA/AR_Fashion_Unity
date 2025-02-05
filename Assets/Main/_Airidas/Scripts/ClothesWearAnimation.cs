using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ClothesWearAnimation : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] private float fadeSpeed = 4f;
    private Color videoImageColor;
    private Color currentVideoImageColor;

    private VideoClip previousClip;
    private VideoClip currentClip;

    private float t;

    private bool fadeIn = false;
    private bool fadeOut = false;
    private bool fadeLoadFinished = false;
    private bool fadeOutAfter = false;
    
    [SerializeField] Animator anim;
    [SerializeField] private Image videoSwitchImage;
    [SerializeField] private RawImage videoPlayerImage;

    [SerializeField] private ButtonContinueInteraction buttonContinue;
    private GameObject currentDress;
    private GameObject previousDress;

    void Start()
    {
        previousClip = videoPlayer.clip;
        previousDress = DressList.Instance.GetStartingDress();
        ResetAnimationClip();
    }

    void Update()
    {
        if (fadeIn)
        {
            FadeInSequence(true);
        }
        else if (fadeOut)
        {
            FadeOutSequence(true);
        }
        else if (videoPlayer.isPrepared && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
        else if (videoPlayer.frame > 5 && !fadeIn && !fadeOut && !fadeLoadFinished)
        {
            //videoSwitchImage.enabled = false;
            fadeOut = true;
        }
        else if (videoPlayer.clip != null)
        {
            if (videoPlayer.frame >= videoPlayer.clip.frameCount - 15f && !fadeOutAfter)
            {
                FadeInSequence(false);
            }

        }

    }

    void FadeInSequence(bool playNewVideo)
    {
        if (!videoSwitchImage.enabled)
        {
            videoSwitchImage.enabled = true;
        }

        if (buttonContinue.gameObject.activeInHierarchy)
        {
            buttonContinue.DisableButton();
        }

        t += Time.deltaTime;
        videoImageColor.a = Mathf.Lerp(currentVideoImageColor.a, 1f, t * fadeSpeed);
        videoSwitchImage.color = videoImageColor;

        float stopTimerAfter = 1f;

        if (t > stopTimerAfter / fadeSpeed)
        {
            if (playNewVideo)
            {
                videoPlayer.clip = currentClip;
                videoPlayer.Prepare();
                videoPlayerImage.enabled = true;
            }
            else
            {
                if (DressList.Instance.GetStartingDress().gameObject.activeInHierarchy)
                {
                    DressList.Instance.GetStartingDress().gameObject.SetActive(false);
                }

                if (!currentDress.activeInHierarchy)
                {
                    DressList.Instance.DisableDresses();
                    currentDress.SetActive(true);
                }

                buttonContinue.EnableButton();

                //if (DressList.Instance.GetStartingDress().activeInHierarchy)
                //{
                //    DressList.Instance.GetStartingDress().SetActive(false);
                //}

                videoPlayerImage.enabled = false;
                fadeOut = true;
                fadeOutAfter = true;
            }

            currentVideoImageColor = videoImageColor;
            fadeIn = false;
            t = 0f;
        }
    }

    void FadeOutSequence(bool playNewVideo)
    {
        t += Time.deltaTime;

        videoImageColor.a = Mathf.Lerp(currentVideoImageColor.a, 0f, t * fadeSpeed);
        videoSwitchImage.color = videoImageColor;

        float stopTimerAfter = 1f;
        if (t >= stopTimerAfter / fadeSpeed)
        {
            currentVideoImageColor = videoImageColor;
            videoSwitchImage.enabled = false;
            fadeIn = false;
            fadeOut = false;
            fadeLoadFinished = true;
            t = 0f;
        }

        
    }

    public void PlayAnimationClip(VideoClip clip)
    {
        if (previousClip == null || clip != previousClip)
        {
            t = 0f;
            currentClip = clip;
            previousClip = clip;
            fadeIn = true;
            fadeLoadFinished = false;
            fadeOutAfter = false;
            currentVideoImageColor = videoImageColor;
        }
    }

    public void SetCurrentDress(GameObject dressObject)
    {
        if (currentDress != null)
        {
            previousDress = currentDress;
        }

        currentDress = dressObject;
    }

    public void PauseVideoClip()
    {
        videoPlayer.frame = 0;
        videoPlayer.Pause();
    }

    void ResetAnimationClip()
    {
        PauseVideoClip();
        fadeIn = false;
        fadeOut = false;
        fadeLoadFinished = false;
        fadeOutAfter = false;
        t = 0f;

        videoImageColor = videoSwitchImage.color;
        videoImageColor.a = 0f;
        currentVideoImageColor = videoImageColor;
        videoSwitchImage.color = currentVideoImageColor;
        videoSwitchImage.enabled = false;
        videoPlayerImage.enabled = false;
    }

    private void OnEnable()
    {
        ResetAnimationClip();
    }
}
