using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    private CanvasGroup _cg;
    protected MainMenu _mainMenu;

    void Start()
    {
        _mainMenu = FindObjectOfType<MainMenu>();
    }

    public void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void Activate(float fade = 0f)
    {
        _cg.SwitchGroup(true, fade);
    }

    public void Deactivate(float fade = 0f)
    {
        _cg.SwitchGroup(false, fade);
    }
}
