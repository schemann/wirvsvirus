using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMainCharacter : MonoBehaviour
{
    [SerializeField]
    private float Speed = 1.0f;

    [SerializeField]
    private Vector2 target;


    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {


#if UNITY_EDTIOR || UNITY_STANDALONE_WIN
        if(Input.GetMouseButtonDown(0))
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target = new Vector2(point.x, point.y);
            
        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            var point = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            target = new Vector2(point.x, point.y); 
        }
#endif
        if (Vector2.Distance(target, new Vector2( transform.position.x, transform.position.y)) > 0.05f)
        {
            transform.position = transform.position + (new Vector3(target.x, target.y, 0) - transform.position) * Time.deltaTime * Speed;
        }
    }
}
