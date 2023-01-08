//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Game/Scripts/Player/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""OnMove"",
            ""id"": ""14794553-0f7c-4512-a14f-6c68256fd892"",
            ""actions"": [
                {
                    ""name"": ""MOVE"",
                    ""type"": ""Value"",
                    ""id"": ""60634d53-8b42-44a1-883c-2cd5c2d95b2b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DASH"",
                    ""type"": ""Button"",
                    ""id"": ""9721b7d3-4f62-4061-92dc-faa0eadc4c32"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BASIC ATTACK 1"",
                    ""type"": ""Button"",
                    ""id"": ""f0f4a44d-5b43-4a1d-b20a-a1842539e169"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BASIC ATTACK 2"",
                    ""type"": ""Button"",
                    ""id"": ""5fe6c9c7-efc0-44b4-96ce-d7c97b73dd9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SPELL1"",
                    ""type"": ""Button"",
                    ""id"": ""7602c34a-ba56-4318-81c2-b1fe05d0bff9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SPELL2"",
                    ""type"": ""Button"",
                    ""id"": ""1fe2c1f0-9672-4d05-b13a-202f2a015418"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SPELL3"",
                    ""type"": ""Button"",
                    ""id"": ""20bddcc5-c289-48c0-abc4-6e04d2fc752f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SPELL4"",
                    ""type"": ""Button"",
                    ""id"": ""c0ab53b6-551a-4feb-9305-107524fe6e4d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SPELL5"",
                    ""type"": ""Button"",
                    ""id"": ""2573edd9-a3bd-4b92-885d-c29d4b64e8a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""0be41ce0-1fa8-440d-b383-298214243475"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MOVE"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c027ce02-250e-4554-a483-0899ce27b84e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MOVE"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""13912d5b-261d-4fbc-a192-9234309fa913"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MOVE"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4870a18b-c98b-4c1b-85b7-caf8512db2a7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MOVE"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""216cb145-92ca-4861-a3ca-5107dcf80297"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MOVE"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""de89ebff-b677-4d47-9ea1-aef449105eec"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DASH"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1f69139-4d3d-4ed7-bb0e-cfa5f9ce66e7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BASIC ATTACK 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c8a1638-b84e-43c2-87b8-2a6e809791a3"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BASIC ATTACK 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6686dac8-3df0-4a5c-a9f0-6fae486d7455"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SPELL1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2d57398-702d-49b8-b2b9-9c95c3506aa8"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SPELL2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5876f89-6bab-4922-bd5a-a51176302d9f"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SPELL3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e27ec32-e6ec-4f8f-9928-0d77ae6d5b0a"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SPELL4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0e15029-1af6-4068-a685-a557712d4746"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SPELL5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // OnMove
        m_OnMove = asset.FindActionMap("OnMove", throwIfNotFound: true);
        m_OnMove_MOVE = m_OnMove.FindAction("MOVE", throwIfNotFound: true);
        m_OnMove_DASH = m_OnMove.FindAction("DASH", throwIfNotFound: true);
        m_OnMove_BASICATTACK1 = m_OnMove.FindAction("BASIC ATTACK 1", throwIfNotFound: true);
        m_OnMove_BASICATTACK2 = m_OnMove.FindAction("BASIC ATTACK 2", throwIfNotFound: true);
        m_OnMove_SPELL1 = m_OnMove.FindAction("SPELL1", throwIfNotFound: true);
        m_OnMove_SPELL2 = m_OnMove.FindAction("SPELL2", throwIfNotFound: true);
        m_OnMove_SPELL3 = m_OnMove.FindAction("SPELL3", throwIfNotFound: true);
        m_OnMove_SPELL4 = m_OnMove.FindAction("SPELL4", throwIfNotFound: true);
        m_OnMove_SPELL5 = m_OnMove.FindAction("SPELL5", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // OnMove
    private readonly InputActionMap m_OnMove;
    private IOnMoveActions m_OnMoveActionsCallbackInterface;
    private readonly InputAction m_OnMove_MOVE;
    private readonly InputAction m_OnMove_DASH;
    private readonly InputAction m_OnMove_BASICATTACK1;
    private readonly InputAction m_OnMove_BASICATTACK2;
    private readonly InputAction m_OnMove_SPELL1;
    private readonly InputAction m_OnMove_SPELL2;
    private readonly InputAction m_OnMove_SPELL3;
    private readonly InputAction m_OnMove_SPELL4;
    private readonly InputAction m_OnMove_SPELL5;
    public struct OnMoveActions
    {
        private @PlayerInput m_Wrapper;
        public OnMoveActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MOVE => m_Wrapper.m_OnMove_MOVE;
        public InputAction @DASH => m_Wrapper.m_OnMove_DASH;
        public InputAction @BASICATTACK1 => m_Wrapper.m_OnMove_BASICATTACK1;
        public InputAction @BASICATTACK2 => m_Wrapper.m_OnMove_BASICATTACK2;
        public InputAction @SPELL1 => m_Wrapper.m_OnMove_SPELL1;
        public InputAction @SPELL2 => m_Wrapper.m_OnMove_SPELL2;
        public InputAction @SPELL3 => m_Wrapper.m_OnMove_SPELL3;
        public InputAction @SPELL4 => m_Wrapper.m_OnMove_SPELL4;
        public InputAction @SPELL5 => m_Wrapper.m_OnMove_SPELL5;
        public InputActionMap Get() { return m_Wrapper.m_OnMove; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OnMoveActions set) { return set.Get(); }
        public void SetCallbacks(IOnMoveActions instance)
        {
            if (m_Wrapper.m_OnMoveActionsCallbackInterface != null)
            {
                @MOVE.started -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnMOVE;
                @MOVE.performed -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnMOVE;
                @MOVE.canceled -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnMOVE;
                @DASH.started -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnDASH;
                @DASH.performed -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnDASH;
                @DASH.canceled -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnDASH;
                @BASICATTACK1.started -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnBASICATTACK1;
                @BASICATTACK1.performed -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnBASICATTACK1;
                @BASICATTACK1.canceled -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnBASICATTACK1;
                @BASICATTACK2.started -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnBASICATTACK2;
                @BASICATTACK2.performed -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnBASICATTACK2;
                @BASICATTACK2.canceled -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnBASICATTACK2;
                @SPELL1.started -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL1;
                @SPELL1.performed -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL1;
                @SPELL1.canceled -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL1;
                @SPELL2.started -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL2;
                @SPELL2.performed -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL2;
                @SPELL2.canceled -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL2;
                @SPELL3.started -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL3;
                @SPELL3.performed -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL3;
                @SPELL3.canceled -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL3;
                @SPELL4.started -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL4;
                @SPELL4.performed -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL4;
                @SPELL4.canceled -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL4;
                @SPELL5.started -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL5;
                @SPELL5.performed -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL5;
                @SPELL5.canceled -= m_Wrapper.m_OnMoveActionsCallbackInterface.OnSPELL5;
            }
            m_Wrapper.m_OnMoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MOVE.started += instance.OnMOVE;
                @MOVE.performed += instance.OnMOVE;
                @MOVE.canceled += instance.OnMOVE;
                @DASH.started += instance.OnDASH;
                @DASH.performed += instance.OnDASH;
                @DASH.canceled += instance.OnDASH;
                @BASICATTACK1.started += instance.OnBASICATTACK1;
                @BASICATTACK1.performed += instance.OnBASICATTACK1;
                @BASICATTACK1.canceled += instance.OnBASICATTACK1;
                @BASICATTACK2.started += instance.OnBASICATTACK2;
                @BASICATTACK2.performed += instance.OnBASICATTACK2;
                @BASICATTACK2.canceled += instance.OnBASICATTACK2;
                @SPELL1.started += instance.OnSPELL1;
                @SPELL1.performed += instance.OnSPELL1;
                @SPELL1.canceled += instance.OnSPELL1;
                @SPELL2.started += instance.OnSPELL2;
                @SPELL2.performed += instance.OnSPELL2;
                @SPELL2.canceled += instance.OnSPELL2;
                @SPELL3.started += instance.OnSPELL3;
                @SPELL3.performed += instance.OnSPELL3;
                @SPELL3.canceled += instance.OnSPELL3;
                @SPELL4.started += instance.OnSPELL4;
                @SPELL4.performed += instance.OnSPELL4;
                @SPELL4.canceled += instance.OnSPELL4;
                @SPELL5.started += instance.OnSPELL5;
                @SPELL5.performed += instance.OnSPELL5;
                @SPELL5.canceled += instance.OnSPELL5;
            }
        }
    }
    public OnMoveActions @OnMove => new OnMoveActions(this);
    public interface IOnMoveActions
    {
        void OnMOVE(InputAction.CallbackContext context);
        void OnDASH(InputAction.CallbackContext context);
        void OnBASICATTACK1(InputAction.CallbackContext context);
        void OnBASICATTACK2(InputAction.CallbackContext context);
        void OnSPELL1(InputAction.CallbackContext context);
        void OnSPELL2(InputAction.CallbackContext context);
        void OnSPELL3(InputAction.CallbackContext context);
        void OnSPELL4(InputAction.CallbackContext context);
        void OnSPELL5(InputAction.CallbackContext context);
    }
}
