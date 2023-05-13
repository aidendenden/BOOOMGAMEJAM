using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour
{
    private SpriteRenderer _sp;
    void Start()
    {
        OpenShadow();
    }

   void OpenShadow()
    {
        //获取动画组件
        if (transform.TryGetComponent<SpriteRenderer>(out _sp))
        {
            Debug.Log("Found SpriteRenderer component: " + _sp.name);
            _sp.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
        else
        {
            Debug.Log("Could not find SpriteRenderer component");

        }

       
    }
}
