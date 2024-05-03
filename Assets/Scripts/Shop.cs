using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour{

    [SerializeField] Animator anim;

    private bool isShopOpen = true;
    public void SetSelected(){

    }

    public void ToggleShop(){
        isShopOpen = !isShopOpen;
        anim.SetBool("ShopOpen", isShopOpen);
    }
}
