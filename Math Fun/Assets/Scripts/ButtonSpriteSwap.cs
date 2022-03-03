using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteSwap : MonoBehaviour
{

    [SerializeField] Sprite defaultButtonSprite;
    [SerializeField] Sprite lockedButtonSprite;

    GameObject menuButton;

    Image buttonImage;

    //[SerializeField] bool isButtonLocked;
    public bool isButtonLocked;


    private void Start() {

        menuButton = this.gameObject;
        buttonImage = menuButton.GetComponent<Image>();
    }
    private void Update() {
    
        LockedButton(); 
    }

    void LockedButton()
    {
        menuButton = this.gameObject;
        buttonImage = menuButton.GetComponent<Image>();

        if (isButtonLocked) {
            buttonImage.sprite = lockedButtonSprite;
        }
        else {
            buttonImage.sprite = defaultButtonSprite;
        } 
        
    }







}
