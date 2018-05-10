using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : Model
{

    #region 常量
    const int InitCoin = 5000;
    #endregion

    #region 事件
    #endregion

    #region 字段
    bool m_IsPlay = true;
    bool m_IsPause = false;

    //特技时间
    int m_SkillTime = 5;
    int m_Magnet;
    int m_Multiply;
    int m_Invincible;
    int m_Grade;
    int m_Exp;
    int m_Coin;
    //装备
    int m_TakeOnFootball = 0;
    //购买
    public List<int> BuyFootBall = new List<int>();

    //装备衣服
    BuyID m_TakeOnCloth = new BuyID() { SkinID =0, ClothID = 0};
    //购买的衣服
    public List<BuyID> BuyCloth = new List<BuyID>();
    public int lastIndex = 1;

    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Consts.M_GameModel;
        }
    }

    public bool IsPlay
    {
        get
        {
            return m_IsPlay;
        }

        set
        {
            m_IsPlay = value;
        }
    }

    public bool IsPause
    {
        get
        {
            return m_IsPause;
        }

        set
        {
            m_IsPause = value;
        }
    }

    public int SkillTime
    {
        get
        {
            return m_SkillTime;
        }

        set
        {
            m_SkillTime = value;
        }
    }

    public int Magnet
    {
        get
        {
            return m_Magnet;
        }

        set
        {
            m_Magnet = value;
        }
    }

    public int Multiply
    {
        get
        {
            return m_Multiply;
        }

        set
        {
            m_Multiply = value;
        }
    }

    public int Invincible
    {
        get
        {
            return m_Invincible;
        }

        set
        {
            m_Invincible = value;
        }
    }

    public int Grade
    {
        get
        {
            return m_Grade;
        }

        set
        {
            m_Grade = value;
        }
    }

    public int Exp
    {
        get
        {
            return m_Exp;
        }

        set
        {


            while (value > 500 + Grade * 100)
            {
                value -= 500 + Grade * 100;
                Grade++;
            }
            m_Exp = value;
        }
    }

    public int Coin
    {
        get
        {
            return m_Coin;
        }

        set
        {
            m_Coin = value;
            Debug.Log("现在还剩" + value + "钱");
        }
    }

    public int TakeOnFootball
    {
        get
        {
            return m_TakeOnFootball;
        }

        set
        {
            m_TakeOnFootball = value;
        }
    }

    public BuyID TakeOnCloth
    {
        get
        {
            return m_TakeOnCloth;
        }

        set
        {
            m_TakeOnCloth = value;
        }
    }
    #endregion

    #region 方法
    //初始化
    public void Init()
    {
        m_Magnet = 0;
        m_Invincible = 0;
        m_Multiply = 0;
        m_SkillTime = 5;
        m_Exp = 0;
        m_Grade = 1;
        m_Coin = InitCoin;
        InitSkin();
        

    }

    void InitSkin()
    {
        //足球信息
        BuyFootBall.Add(m_TakeOnFootball);
        //衣服
        BuyCloth.Add(TakeOnCloth);
    }

    //买东西
    public bool GetMoney(int coin)
    {
        if(coin <= Coin)
        {
            Coin -= coin;
            return true;
        }
        return false;
    }

    //检查足球状态
    public ItemState CheckFootBallState(int i)
    {
        if(i == TakeOnFootball)
        {
            return ItemState.Equip;
        }
        else
        {
            if (BuyFootBall.Contains(i))
            {
                return ItemState.Buy;
            }
            else
            {
                return ItemState.UnBuy;
            }
        }
    }
    //检查skin
    public ItemState CheckSkinState(int i)
    {
        if (i == TakeOnCloth.SkinID)
        {
            return ItemState.Equip;
        }
        else
        {
            foreach(var a in BuyCloth)
            {
                if(a.SkinID == i)
                {
                    return ItemState.Buy;
                }
            }
            return ItemState.UnBuy;
        }

    }

    //检查cloth
    public ItemState CheckClothState(BuyID  id)
    {
        if (id.SkinID == TakeOnCloth.SkinID && id.ClothID == TakeOnCloth.ClothID)
        {
            return ItemState.Equip;
        }
        else
        {
            foreach (var a in BuyCloth)
            {
                if (a.SkinID == id.SkinID && a.ClothID == id.ClothID)
                {
                    return ItemState.Buy;
                }
            }
            return ItemState.UnBuy;
        }
    }
   

    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion



}

public class BuyID
{

    public int SkinID;
    public int ClothID;
}
