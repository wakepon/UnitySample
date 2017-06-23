using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using WK.Tween;

public class ToggleSwitchController : MonoBehaviour, IPointerClickHandler {
    [SerializeField]
    Transform button;

    [SerializeField]
    Transform inactiveBackground;

    [SerializeField]
    float duration = 0.1f;

    [SerializeField]
    UnityEvent onEvent;

    [SerializeField]
    UnityEvent offEvent;

    bool currToggle = false;
    public bool CurrToggle { get { return currToggle; } }

    private enum EState {
          idle
        , gotoOn
        , gotoOff
    }

    private EState currState    = 0;
    private EState nextState    = 0;
    private int    stateCounter = 0;

    static readonly Vector3 cOnPos  = new Vector3(  100.0f, 0.0f, 0.0f );
    static readonly Vector3 cOffPos = new Vector3( -100.0f, 0.0f, 0.0f );

    //------------------------------------------------------------------------------
    void Awake()
    {
        stateCounter = 0;
        currState    = EState.idle;
        nextState    = EState.idle;
    }

    //------------------------------------------------------------------------------
    void Update()
    {
        ++stateCounter; 
        if( currState != nextState )
        {
            currState    = nextState;
            stateCounter = 0;
        }

        switch( currState )
        {
            case EState.gotoOn:
                updateGoToOn();
                break;
            case EState.gotoOff:
                updateGoToOff();
                break;
        }
    }

    //------------------------------------------------------------------------------
    private void changeState( EState next_state )
    {
        nextState = next_state;
    }

    //------------------------------------------------------------------------------
    private void updateGoToOn()
    {
        if( stateCounter == 0 )
        {
            inactiveBackground.TwScale( Vector3.zero, duration );
            button.TwMove( cOnPos, duration ).OnComplete( 
                    () => {
                            changeState( EState.idle );
                            onEvent.Invoke();
                        }
                    );
        }
    }

    //------------------------------------------------------------------------------
    private void updateGoToOff()
    {
        if( stateCounter == 0 )
        {
            inactiveBackground.TwScale( Vector3.one, duration );
            button.TwMove( cOffPos, duration ).OnComplete( () => {
                            changeState( EState.idle );
                            offEvent.Invoke();
                        }
                    );
        }
    }

    //------------------------------------------------------------------------------
    public bool GoToOnImmediately()
    {
        if( currState == EState.idle || nextState == EState.idle )
        {
            inactiveBackground.localScale = Vector3.zero;
            button.localPosition = cOnPos;
            currToggle = true;
            changeState( EState.idle );
            return true;
        }
        return false;
    }

    //------------------------------------------------------------------------------
    public bool GoToOffImmediately()
    {
        if( currState == EState.idle || nextState == EState.idle )
        {
            inactiveBackground.localScale = Vector3.one;
            button.localPosition = cOffPos;
            currToggle = false;
            changeState( EState.idle );
            return true;
        }
        return false;
    }

    //------------------------------------------------------------------------------
    public bool GoToOn()
    {
        if( currState == EState.idle )
        {
            changeState( EState.gotoOn );
            return true;
        }
        return false;
    }

    //------------------------------------------------------------------------------
    public bool GoToOff()
    {
        if( currState == EState.idle )
        {
            changeState( EState.gotoOff );
            return true;
        }
        return false;
    }

    //------------------------------------------------------------------------------
    public virtual void OnPointerClick( PointerEventData event_data )
    {
        if( currState == EState.idle )
        {
            currToggle ^= true;
            if( currToggle )
            {
                GoToOn();
            }
            else
            {
                GoToOff();
            }
        }
    }
}
