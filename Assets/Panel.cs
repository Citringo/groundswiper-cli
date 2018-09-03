using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Panel : MonoBehaviour {

    Image img;
    RectTransform rect;

    [SerializeField]
    ConsoleKey keyInfo;

    public ConsoleKey KeyInfo
    {
        get { return keyInfo; }
        set { keyInfo = value; }
    }

    Color off, on;

    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        KeyInfo = (ConsoleKey)Enum.Parse(typeof(ConsoleKey), gameObject.name, true);

        ColorUtility.TryParseHtmlString("#CCCC88", out off);
        ColorUtility.TryParseHtmlString("#CC88CC", out on);
        img.color = off;

    }

	bool prevIsBeingTouched;

    public TouchResponse GetTouch()
    {
        bool isBeingTouched = false;

        foreach (var t in Input.touches)
        {
            if (Physics2D.CircleCastAll(t.position, t.radius, Vector2.zero).Any(r => r.transform == transform))
            {
                isBeingTouched = true;
                break;
            }
        }

        if (isBeingTouched == prevIsBeingTouched)
            return TouchResponse.Keeping;
        img.color = isBeingTouched ? on : off;
        prevIsBeingTouched = isBeingTouched;
        return isBeingTouched ? TouchResponse.Entered : TouchResponse.Leaved;
    }
}

public enum TouchResponse 
{
    Entered,
    Leaved,
    Keeping
}

static class Extension
{
    public static bool Overlaps(this RectTransform r, Vector2 point, float radius)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(r, point);

    }
}
