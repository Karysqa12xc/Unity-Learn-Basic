using System.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using Chapter.LogicAndGameplay;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<Menu> menus = new List<Menu>();
    // Start is called before the first frame update
    private void Start()
    {
        ShowMenu(menus[0]);
    }

    public void ShowMenu(Menu menuToShow)
    {
        if(menus.Contains(menuToShow) == false){
            Debug.LogErrorFormat("{0} is not in the list of menus"
                        , menuToShow.name);
            return;    
        }
        foreach (var otherMenu in menus)
        {
            if(otherMenu == menuToShow){
                otherMenu.gameObject.SetActive(true);
                otherMenu.menuDidAppear.Invoke();
            }else{
                if(otherMenu.gameObject.activeInHierarchy){
                    otherMenu.menuWillDisappear.Invoke();
                }
                otherMenu.gameObject.SetActive(false);
            }
        }
    }
}
