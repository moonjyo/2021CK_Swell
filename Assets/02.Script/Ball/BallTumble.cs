using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTumble : MonoBehaviour, IInteractbale
{
    Rigidbody rigidBody;
    Vector3 movement;
    float fTime = 0.0f;

    public string ItemKey;
    public string MonologueKey; //임시 

    public bool IsTake;
    public bool IsWatch;
    public bool IsRotate;

    public bool IsInterAction = false;

    public Sprite InventoryIcon; // 이 아이템의 아이콘

    public Vector3 SizeObj;

    //public PlayerInterActionObj InteractObj;
    public string InteractObjKey;

    public Animator InterActAnim;

    [SerializeField]
    private GameObject[] Objs;

    [SerializeField]
    Vector3 UIOffsetVec;


    [HideInInspector]
    public List<GameObject> UISecondObjList = new List<GameObject>();
    public FirstInterActionUI UIFirstObj;

    void Awake()
    {
        this.gameObject.GetComponent<FMODUnity.StudioEventEmitter>().enabled = false;
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        movement = transform.forward;
        gameObject.layer = 0;

        IsInterAction = true;
        ItemKey = this.gameObject.name;

        //GameManager.Instance.uiManager.uiInventory.OnDistingush += TestCheck;

        for (int i = 0; i < Objs.Length; ++i)
        {
            GameObject Targetobj = Instantiate(Objs[i]);
            Targetobj.SetActive(false);
            IInteractableUI target = Targetobj.transform.GetComponent<IInteractableUI>();


            target.SetTargetObj(transform.gameObject);
            target.SetTargetCanvas(GameManager.Instance.uiManager.InterActionUICanvas);
            target.Init();


            if (Targetobj.CompareTag("SecondInterActionUI"))
            {
                UISecondObjList.Add(Targetobj);
            }
        }
    }

    private void FixedUpdate()
    {
        fTime += Time.fixedDeltaTime;
        if (fTime < 4.1f)
        {
            rigidBody.AddForce(movement * 10.0f);
        }
        else
        {
            gameObject.layer = 17;
        }
    }


    public void SecondInteractOn()
    {
        foreach (var Obj in UISecondObjList)
        {
            Obj.gameObject.SetActive(true);
        }
    }
    public void SecondInteractOff()
    {
        foreach (var Obj in UISecondObjList)
        {
            Obj.gameObject.SetActive(false);
        }
    }
    public void AllDestroyObj()
    {
        foreach (var Obj in UISecondObjList)
        {
            Obj.SetActive(false);
        }
        //UIFirstObj.gameObject.SetActive(false);
        // GameManager.Instance.uiManager.OnActiveFirstInterActionUI.Remove(UIFirstObj);
        GameManager.Instance.uiManager.IsOnFirstInterActionUI = false;
    }

    public List<GameObject> GetUIObjList()
    {
        return UISecondObjList;
    }

    public bool IsGetInterAction()
    {
        return IsInterAction;
    }


    public IEnumerator InterAct()
    {
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.PICKUPDOWN);
        GameManager.Instance.uiManager.uiInventory.Distinguish.FirePlace();
        yield return new WaitForSeconds(1.4f);
        GameManager.Instance.timeLine.Play("FirePlace");
        SecondInteractOff();


        if (GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Contains(this))
        {
            GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Remove(this);
        }


        gameObject.SetActive(false);
    }
}
