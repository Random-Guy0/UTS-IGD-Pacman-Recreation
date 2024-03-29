// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""1673f9a1-d31a-4a9f-a095-3204e4acc95c"",
            ""actions"": [
                {
                    ""name"": ""Move Vertical"",
                    ""type"": ""Button"",
                    ""id"": ""67e706ce-e74c-4132-bbaf-dc4b17d45374"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move Horizontal"",
                    ""type"": ""Button"",
                    ""id"": ""585000c9-0a06-4359-8f2d-d36ccb4d4e78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD Keys"",
                    ""id"": ""6bb5788d-614f-482f-8c99-499260f2ef96"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a2e32381-e17d-434b-a92a-508aff7e88b6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f8e468f3-aab5-4a46-a8e9-a8960beeff49"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""bc217f58-c781-4220-900a-479c0fd8d2d4"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f554531b-ee5a-4f09-97d7-f4c1be494f71"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""60e0c728-16c3-43e9-b0e6-0ec66a36e0ad"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD Keys"",
                    ""id"": ""c0129660-53dd-49eb-8397-ca83ee1c8d1f"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""97639a71-281d-41b0-b7db-246bb06b652a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f9a16f59-1d23-4a8c-ae7d-5590bc6352b3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""7730c7c9-9813-4ecb-a247-1521daff5bad"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3fe4f2eb-52d8-4cac-bed0-44f2d9cc6cac"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""638fd2e7-7cb0-4d95-9356-065ae56dbcad"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Time Powers"",
            ""id"": ""987aa702-b9f8-4f98-b32c-27e338ef4057"",
            ""actions"": [
                {
                    ""name"": ""Fast Forward"",
                    ""type"": ""Button"",
                    ""id"": ""06c6c923-eda0-4e1c-a3fb-ac687435a2fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slow Motion"",
                    ""type"": ""Button"",
                    ""id"": ""5e7109a2-f114-459f-b2ce-fa1c914577d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ultra Slow Motion"",
                    ""type"": ""Button"",
                    ""id"": ""931d4ff0-8bbf-416c-80c8-a89886225e37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8b152f15-0622-445b-bd42-788463af3caf"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fast Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33d5221c-5348-4b71-9b93-6073b63f7e6f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slow Motion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""def3ba12-76f2-46ae-bee8-a94fa6a1ec3a"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ultra Slow Motion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_MoveVertical = m_Movement.FindAction("Move Vertical", throwIfNotFound: true);
        m_Movement_MoveHorizontal = m_Movement.FindAction("Move Horizontal", throwIfNotFound: true);
        // Time Powers
        m_TimePowers = asset.FindActionMap("Time Powers", throwIfNotFound: true);
        m_TimePowers_FastForward = m_TimePowers.FindAction("Fast Forward", throwIfNotFound: true);
        m_TimePowers_SlowMotion = m_TimePowers.FindAction("Slow Motion", throwIfNotFound: true);
        m_TimePowers_UltraSlowMotion = m_TimePowers.FindAction("Ultra Slow Motion", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_MoveVertical;
    private readonly InputAction m_Movement_MoveHorizontal;
    public struct MovementActions
    {
        private @PlayerInput m_Wrapper;
        public MovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveVertical => m_Wrapper.m_Movement_MoveVertical;
        public InputAction @MoveHorizontal => m_Wrapper.m_Movement_MoveHorizontal;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @MoveVertical.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveVertical;
                @MoveVertical.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveVertical;
                @MoveVertical.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveVertical;
                @MoveHorizontal.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveHorizontal;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveVertical.started += instance.OnMoveVertical;
                @MoveVertical.performed += instance.OnMoveVertical;
                @MoveVertical.canceled += instance.OnMoveVertical;
                @MoveHorizontal.started += instance.OnMoveHorizontal;
                @MoveHorizontal.performed += instance.OnMoveHorizontal;
                @MoveHorizontal.canceled += instance.OnMoveHorizontal;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Time Powers
    private readonly InputActionMap m_TimePowers;
    private ITimePowersActions m_TimePowersActionsCallbackInterface;
    private readonly InputAction m_TimePowers_FastForward;
    private readonly InputAction m_TimePowers_SlowMotion;
    private readonly InputAction m_TimePowers_UltraSlowMotion;
    public struct TimePowersActions
    {
        private @PlayerInput m_Wrapper;
        public TimePowersActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @FastForward => m_Wrapper.m_TimePowers_FastForward;
        public InputAction @SlowMotion => m_Wrapper.m_TimePowers_SlowMotion;
        public InputAction @UltraSlowMotion => m_Wrapper.m_TimePowers_UltraSlowMotion;
        public InputActionMap Get() { return m_Wrapper.m_TimePowers; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TimePowersActions set) { return set.Get(); }
        public void SetCallbacks(ITimePowersActions instance)
        {
            if (m_Wrapper.m_TimePowersActionsCallbackInterface != null)
            {
                @FastForward.started -= m_Wrapper.m_TimePowersActionsCallbackInterface.OnFastForward;
                @FastForward.performed -= m_Wrapper.m_TimePowersActionsCallbackInterface.OnFastForward;
                @FastForward.canceled -= m_Wrapper.m_TimePowersActionsCallbackInterface.OnFastForward;
                @SlowMotion.started -= m_Wrapper.m_TimePowersActionsCallbackInterface.OnSlowMotion;
                @SlowMotion.performed -= m_Wrapper.m_TimePowersActionsCallbackInterface.OnSlowMotion;
                @SlowMotion.canceled -= m_Wrapper.m_TimePowersActionsCallbackInterface.OnSlowMotion;
                @UltraSlowMotion.started -= m_Wrapper.m_TimePowersActionsCallbackInterface.OnUltraSlowMotion;
                @UltraSlowMotion.performed -= m_Wrapper.m_TimePowersActionsCallbackInterface.OnUltraSlowMotion;
                @UltraSlowMotion.canceled -= m_Wrapper.m_TimePowersActionsCallbackInterface.OnUltraSlowMotion;
            }
            m_Wrapper.m_TimePowersActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FastForward.started += instance.OnFastForward;
                @FastForward.performed += instance.OnFastForward;
                @FastForward.canceled += instance.OnFastForward;
                @SlowMotion.started += instance.OnSlowMotion;
                @SlowMotion.performed += instance.OnSlowMotion;
                @SlowMotion.canceled += instance.OnSlowMotion;
                @UltraSlowMotion.started += instance.OnUltraSlowMotion;
                @UltraSlowMotion.performed += instance.OnUltraSlowMotion;
                @UltraSlowMotion.canceled += instance.OnUltraSlowMotion;
            }
        }
    }
    public TimePowersActions @TimePowers => new TimePowersActions(this);
    public interface IMovementActions
    {
        void OnMoveVertical(InputAction.CallbackContext context);
        void OnMoveHorizontal(InputAction.CallbackContext context);
    }
    public interface ITimePowersActions
    {
        void OnFastForward(InputAction.CallbackContext context);
        void OnSlowMotion(InputAction.CallbackContext context);
        void OnUltraSlowMotion(InputAction.CallbackContext context);
    }
}
