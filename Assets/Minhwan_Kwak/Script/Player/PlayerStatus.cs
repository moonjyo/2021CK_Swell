[System.Serializable]
public class PlayerStatus
{
    [EnumFlags]
    public PlayerFSM fsm;

    public PlayerDirection direction;


    // | ^ &   |  하나만 참일때 true  , ^ 둘다 참이면 false  ,  & 둘다 참이여야 true 
    public void FsmAdd(PlayerFSM value)
    {
        fsm = value | fsm;
    }
    public void FsmRemove(PlayerFSM value)
    {
        int tempvalue = fsm.CompareTo(value);
        if (tempvalue == 0 || tempvalue == 1)
        {
            fsm = fsm & ~value;
        }
    }

    public void FsmAllRemove()
    {
        fsm = fsm & ~fsm;
    }


    public int FsmCheck(PlayerFSM value)
    {
        int check = fsm.CompareTo(value);
        return check;
    }

}

public enum PlayerFSM
{
    Walk = 0x00000001, // 0010
    Wall = 0x00000002, // 1000
    Jump = 0x00000004, // 0100
    Ground = 0x00000008,
    Climing = 0x00000010,
    HideWalk = 0x00000020,
    Push = 0x00000040,
    Pull = 0x00000080,
    ItemPickUp = 0x00000100,
    ItemTouch = 0x00000120,
}


public enum PlayerDirection
{
    Top = 0x00000001,
    Left = 0x00000002,
    Right = 0x00000004,
    Bottom = 0x00000008,
    TopRight = 0x000000010,
    TopLeft = 0x000000020,
    BottomRight = 0x000000040,
    BottomLeft = 0x000000080,

}

