[System.Serializable]
public class PlayerStatus 
{
    public int Hp;
    public int Mp;

    public float WalkSpeed;

    public float RunSpeed;

    public float invincibilityTime;

    public float AttackSpeed;
    public float AttackPower;

    public float JumpPower;
    public float DoubleJumPower;


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
        if(tempvalue == 0 || tempvalue == 1)  
        {
            fsm = fsm & ~value;
        }
    }

    public void FsmAllRemove()
    {
        fsm =  fsm &~ fsm; 
    }


    public int FsmCheck(PlayerFSM value)
    {
       int check = fsm.CompareTo(value);
       return check;
    }
}

public enum PlayerFSM
{
    Move = 0x00000001, // 0010
    Wall = 0x00000002, // 1000
    Jump = 0x00000004, // 0100
    Ground = 0x00000008,  
    Climing = 0x00000016, 
}


public enum PlayerDirection
{
   Left,
   Right,

}

