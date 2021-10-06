using UnityEngine;
using UnityEngine.UI;

public static class Initiate
{
    static bool areWeFading = false;

    public static void FadeColor(string scene, Color col, float multiplier)
    {
        if (areWeFading)
        {
            return;
        }

        GameObject init = new GameObject
        {
            name = "Fader"
        };
        CanvasGroup myCanvasGroup = init.AddComponent<CanvasGroup>();

        Canvas myCanvas = init.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        myCanvas.sortingOrder = 32767;

        Fader scr = init.AddComponent<Fader>();
        Image img = init.AddComponent<Image>();

        img.maskable = false;
        img.color = col;

        scr.type = 1;
        scr.fadeDamp = multiplier;
        scr.fadeScene = scene;
        scr.canvasTransform = myCanvasGroup;
        scr.start = true;

        areWeFading = true;
        scr.InitiateFaderColor();
    }

    public static void FadeSprite(string scene, Sprite sprite, Color col, Color colBackground, float multiplier)
    {
        if (areWeFading)
        {
            return;
        }

        GameObject init = new GameObject
        {
            name = "Fader"
        };
        CanvasGroup myCanvasGroup = init.AddComponent<CanvasGroup>();

        Canvas myCanvas = init.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        myCanvas.sortingOrder = 32767;

        Fader scr = init.AddComponent<Fader>();
        Image img = init.AddComponent<Image>();
        img.maskable = false;
        img.color = colBackground;

        GameObject init2 = new GameObject
        {
            name = "Fader Sprite"
        };
        init2.transform.SetParent(init.transform);
        RectTransform rect = init2.AddComponent<RectTransform>();
        rect.anchorMax = new Vector2(1, 1);
        rect.anchorMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);
        rect.offsetMin = new Vector2(0, 0);

        Image img2 = init2.AddComponent<Image>();
        img2.maskable = false;
        img2.color = col;
        img2.sprite = sprite;

        scr.type = 1;
        scr.fadeDamp = multiplier;
        scr.fadeScene = scene;
        scr.canvasTransform = myCanvasGroup;
        scr.start = true;

        areWeFading = true;
        scr.InitiateFaderColor();
    }
    public static void FadeSpritePreserveAspect(string scene, Sprite sprite, Color col, Color colBackground, float multiplier)
    {
        if (areWeFading)
        {
            return;
        }

        GameObject init = new GameObject
        {
            name = "Fader"
        };
        CanvasGroup myCanvasGroup = init.AddComponent<CanvasGroup>();

        Canvas myCanvas = init.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        myCanvas.sortingOrder = 32767;

        Fader scr = init.AddComponent<Fader>();
        Image img = init.AddComponent<Image>();
        img.maskable = false;
        img.color = colBackground;

        GameObject init2 = new GameObject
        {
            name = "Fader Sprite"
        };
        init2.transform.SetParent(init.transform);
        RectTransform rect = init2.AddComponent<RectTransform>();
        rect.anchorMax = new Vector2(1, 1);
        rect.anchorMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);
        rect.offsetMin = new Vector2(0, 0);

        Image img2 = init2.AddComponent<Image>();
        img2.maskable = false;
        img2.color = col;
        img2.sprite = sprite;
        img2.preserveAspect = true;

        scr.type = 1;
        scr.fadeDamp = multiplier;
        scr.fadeScene = scene;
        scr.canvasTransform = myCanvasGroup;
        scr.start = true;

        areWeFading = true;
        scr.InitiateFaderColor();
    }

    public static void FadeCircle(string scene, Color col, float multiplier)
    {
        if (areWeFading)
        {
            return;
        }

        GameObject init = new GameObject
        {
            name = "Fader"
        };

        Canvas myCanvas = init.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        myCanvas.sortingOrder = 32767;

        Fader scr = init.AddComponent<Fader>();

        GameObject init2 = new GameObject
        {
            name = "Fader Circle"
        };
        init2.transform.SetParent(init.transform);
        RectTransform rect = init2.AddComponent<RectTransform>();
        rect.anchorMax = new Vector2(1, 1);
        rect.anchorMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);
        rect.offsetMin = new Vector2(0, 0);

        Image img = init2.AddComponent<Image>();
        img.sprite = Resources.Load<Sprite>("circle");
        img.color = col;
        img.preserveAspect = true;
        img.maskable = false;

        scr.type = 2;
        scr.fadeDamp = multiplier;
        scr.fadeScene = scene;
        scr.circleTransform = init2.transform;
        scr.start = true;
        areWeFading = true;
        scr.InitiateFaderCircle();
    }

    public static void DoneFading()
    {
        areWeFading = false;
    }
}