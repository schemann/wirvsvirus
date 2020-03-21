using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasGroupHelper
{
    public static void SwitchGroup(this CanvasGroup cg, bool active, float fade = 0f)
    {
        if (fade != 0f)
            cg.DOFade(active ? 1f : 0f, fade);
        else
            cg.alpha = active ? 1f : 0f;

        cg.blocksRaycasts = cg.interactable = active;
    }
}
