using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : View
{
    public SkinnedMeshRenderer skm;
    public MeshRenderer render;
    GameModel gm;

    public override string Name
    {
        get
        {
            return Consts.V_MainMenu;
        }
    }

    public override void HandleEvent(string name, object data)
    {
    }


    public void OnPlayClicK()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(3);
    }

    public void OnShopClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(2);
    }
    private void Awake()
    {
        gm = GetModel<GameModel>();
        skm.material = Game.Instance.staticData.GetPlayerInfo(gm.TakeOnCloth.SkinID, gm.TakeOnCloth.ClothID).Material;
        render.material = Game.Instance.staticData.GetFootballInfo(gm.TakeOnFootball).Material;
    }
}
