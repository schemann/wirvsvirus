using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterColors : MonoBehaviour
{
    [Header("Farben")]
    [SerializeField]
    private Color kopfbedeckungFarbe;

    public Color KopfbedeckungFarbe
    {
        get { return kopfbedeckungFarbe; }
        set { kopfbedeckungFarbe = value; }
    }

    [SerializeField]
    private Color hautFarbe;
    public Color HautFarbe
    {
        get { return hautFarbe; }
        set { hautFarbe = value; }
    }
    [SerializeField]
    private Color anzugFarbe;
    public Color AnzugFarbe
    {
        get { return anzugFarbe; }
        set { anzugFarbe = value; }
    }

    [SerializeField]
    private Color schuheFarbe;
    public Color SchuheFarbe
    {
        get { return schuheFarbe; }
        set { schuheFarbe = value; }
    }

    [SerializeField]
    private Color zeichenFarbe;
    public Color ZeichenFarbe
    {
        get { return zeichenFarbe; }
        set { zeichenFarbe = value; }
    }

    [Header("Sprites")]
    [SerializeField]
    private List<SpriteRenderer> Kopfbedeckungen;
    [SerializeField]
    private List<SpriteRenderer> Haut;
    [SerializeField]
    private List<SpriteRenderer> Anzug;
    [SerializeField]
    private List<SpriteRenderer> Schuhe;
    [SerializeField]
    private List<SpriteRenderer> Zeichen;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        UpdateColors();
#endif
    }

    void UpdateColors()
    {
        foreach(var sr in Kopfbedeckungen)
        {
            if(sr!= null)
            {
                sr.color = kopfbedeckungFarbe;
            }
        }
        foreach (var sr in Haut)
        {
            if(sr!=null)
            {
                sr.color = hautFarbe;
            }
        }
        foreach (var sr in Anzug)
        {
            if(sr!=null)
            {
                sr.color = anzugFarbe;
            }
        }
        foreach (var sr in Schuhe)
        {
            if(sr!=null)
            {
                sr.color = schuheFarbe;
            }
        }
        foreach (var sr in Zeichen)
        {
            if(sr!=null)
            {
                sr.color = zeichenFarbe;
            }
        }
    }
}
