using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScopeScript : MonoBehaviour {


    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Image newImage;

    private Sprite newSprite;
    private SpriteRenderer spriteRend;

    Texture2D newTexture;
    
    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        newTexture = RTImage();
        newSprite = Sprite.Create(newTexture, new Rect(0.0f, 0.0f, newTexture.width, newTexture.height), Vector2.zero);
        newImage.sprite = newSprite;
    }


    Texture2D RTImage()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = currentRT;
        return image;
    }
}
