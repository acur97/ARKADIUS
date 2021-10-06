using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    [HideInInspector]
    public bool start = false;
    [HideInInspector]
    public float fadeDamp = 0.0f;
    [HideInInspector]
    public string fadeScene;
    [HideInInspector]
    public float alpha = 0.0f;
    [HideInInspector]
    public bool isFadeIn = false;
    [HideInInspector]
    public int type;
    [HideInInspector]
    public Transform circleTransform;
    [HideInInspector]
    public CanvasGroup canvasTransform;

    float lastTime = 0;
    bool startedLoading = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void InitiateFaderColor()
    {
        DontDestroyOnLoad(gameObject);

        if (canvasTransform)
        {
            canvasTransform.alpha = 0.0f;
            StartCoroutine(FadeIt());
        }
    }

    public void InitiateFaderCircle()
    {
        DontDestroyOnLoad(gameObject);

        if (circleTransform)
        {
            circleTransform.localScale = new Vector3(0, 0, 0);
            StartCoroutine(FadeIt());
        }
    }

    IEnumerator FadeIt()
    {
        while (!start)
        {
            yield return null;
        }

        lastTime = Time.time;
        float coDelta = lastTime;
        bool hasFadedIn = false;

        while (!hasFadedIn)
        {
            switch (type)
            {
                case 1:
                    coDelta = Time.time - lastTime;
                    if (!isFadeIn)
                    {
                        //Fade in
                        alpha = newAlpha(coDelta, 1, alpha);
                        if (alpha == 1 && !startedLoading)
                        {
                            startedLoading = true;
                            SceneManager.LoadScene(fadeScene);
                        }
                    }
                    else
                    {
                        //Fade out
                        alpha = newAlpha(coDelta, 0, alpha);
                        if (alpha == 0)
                        {
                            hasFadedIn = true;
                        }
                    }
                    lastTime = Time.time;
                    canvasTransform.alpha = alpha;
                    break;

                case 2:
                    coDelta = Time.time - lastTime;
                    if (!isFadeIn)
                    {
                        //Fade in
                        alpha = newAlpha(coDelta, 1, alpha);
                        if (alpha == 1 && !startedLoading)
                        {
                            startedLoading = true;
                            SceneManager.LoadScene(fadeScene);
                        }
                    }
                    else
                    {
                        //Fade out
                        alpha = newAlpha(coDelta, 0, alpha);
                        if (alpha == 0)
                        {
                            hasFadedIn = true;
                        }
                    }
                    lastTime = Time.time;
                    circleTransform.localScale = new Vector3(alpha * 2.5f, alpha * 2.5f, alpha * 2.5f);
                    break;
            }

            yield return null;
        }

        Initiate.DoneFading();

        Destroy(gameObject);

        yield return null;
    }

    float newAlpha(float delta, int to, float currAlpha)
    {
        switch (to)
        {
            case 0:
                currAlpha -= fadeDamp * delta;
                if (currAlpha <= 0)
                    currAlpha = 0;

                break;
            case 1:
                currAlpha += fadeDamp * delta;
                if (currAlpha >= 1)
                    currAlpha = 1;

                break;
        }
        return currAlpha;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIt());
        isFadeIn = true;
    }
}