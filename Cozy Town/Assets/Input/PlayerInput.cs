//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.8.1
//     from Assets/Input/PlayerInput.inputactions
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
using UnityEngine;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""e03210ec-8431-4ef7-a051-5034de89da49"",
            ""actions"": [
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Value"",
                    ""id"": ""dcbd6c01-6d11-40f0-9d59-604240721173"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Value"",
                    ""id"": ""ac126289-1d14-4a05-a6d7-3432bb76dec6"",
                    ""expectedControlType"": ""Double"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateCameraRight"",
                    ""type"": ""Button"",
                    ""id"": ""b458ec21-600e-4a63-ae8d-2772330a604a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateCameraLeft"",
                    ""type"": ""Button"",
                    ""id"": ""98da4f30-bcf5-43e1-b604-6a0457c8d803"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseMovement"",
                    ""type"": ""Value"",
                    ""id"": ""a5285aa1-0ff1-4a34-a41a-80e74969d22e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""606bbee2-8634-4e8b-b4f3-f24fffee14dd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a575ba8f-383f-4c06-a909-e66b939480af"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f890dd5a-e8d5-4a95-afb4-5028598ac363"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a08985cc-d371-42a9-bf4f-15763b7badfd"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""dcb49fcc-65ea-484b-8a23-9bf2ba41b431"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ffa2a028-6b31-4a4f-a23a-7867ec8d7e77"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""2d8983ba-300e-41d2-8f59-ff3c473bb301"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f3e41333-7797-442b-9d9a-dce513c9dfdb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e953ee7c-4229-4fc5-b85e-c92c9d38194b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7043a354-2093-4ce6-bc09-af10b63833b8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""00986e22-f034-4e99-9d95-b8c5c61a4361"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cd9727e6-5f43-4473-af48-a3df1e019d34"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa44e03d-33c5-41d8-9279-3b9e527cccba"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17840b02-b80e-4e28-91d4-a6417adfe463"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7eeb2d9f-4c48-4855-8b62-5deb85be8ce3"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""55eb5e04-f479-40b7-825b-73f8e797719f"",
            ""actions"": [
                {
                    ""name"": ""PlaceBuilding"",
                    ""type"": ""Button"",
                    ""id"": ""85e34797-1726-4631-bf26-fb6d94c740cb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateBuilding"",
                    ""type"": ""Button"",
                    ""id"": ""308756ff-719e-4ce7-8189-10714ed579b3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ExitCurrentMode"",
                    ""type"": ""Button"",
                    ""id"": ""8722ea2d-8a8d-4266-80c5-f1fe4a43af3f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""61fc1253-ede2-4de5-bb2a-9e2a4a70598e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ba755ac-becb-41cc-b9b8-48cc79b9933c"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd314a19-7d90-40d0-adad-c9acb61bbd41"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitCurrentMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e584b1b-a88a-499a-b13a-c2d87f7348af"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitCurrentMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Vertical = m_Camera.FindAction("Vertical", throwIfNotFound: true);
        m_Camera_Horizontal = m_Camera.FindAction("Horizontal", throwIfNotFound: true);
        m_Camera_RotateCameraRight = m_Camera.FindAction("RotateCameraRight", throwIfNotFound: true);
        m_Camera_RotateCameraLeft = m_Camera.FindAction("RotateCameraLeft", throwIfNotFound: true);
        m_Camera_MouseMovement = m_Camera.FindAction("MouseMovement", throwIfNotFound: true);
        m_Camera_Zoom = m_Camera.FindAction("Zoom", throwIfNotFound: true);
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_PlaceBuilding = m_Player.FindAction("PlaceBuilding", throwIfNotFound: true);
        m_Player_RotateBuilding = m_Player.FindAction("RotateBuilding", throwIfNotFound: true);
        m_Player_ExitCurrentMode = m_Player.FindAction("ExitCurrentMode", throwIfNotFound: true);
    }

    ~@PlayerInput()
    {
        Debug.Assert(!m_Camera.enabled, "This will cause a leak and performance issues, PlayerInput.Camera.Disable() has not been called.");
        Debug.Assert(!m_Player.enabled, "This will cause a leak and performance issues, PlayerInput.Player.Disable() has not been called.");
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

    // Camera
    private readonly InputActionMap m_Camera;
    private List<ICameraActions> m_CameraActionsCallbackInterfaces = new List<ICameraActions>();
    private readonly InputAction m_Camera_Vertical;
    private readonly InputAction m_Camera_Horizontal;
    private readonly InputAction m_Camera_RotateCameraRight;
    private readonly InputAction m_Camera_RotateCameraLeft;
    private readonly InputAction m_Camera_MouseMovement;
    private readonly InputAction m_Camera_Zoom;
    public struct CameraActions
    {
        private @PlayerInput m_Wrapper;
        public CameraActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Vertical => m_Wrapper.m_Camera_Vertical;
        public InputAction @Horizontal => m_Wrapper.m_Camera_Horizontal;
        public InputAction @RotateCameraRight => m_Wrapper.m_Camera_RotateCameraRight;
        public InputAction @RotateCameraLeft => m_Wrapper.m_Camera_RotateCameraLeft;
        public InputAction @MouseMovement => m_Wrapper.m_Camera_MouseMovement;
        public InputAction @Zoom => m_Wrapper.m_Camera_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void AddCallbacks(ICameraActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraActionsCallbackInterfaces.Add(instance);
            @Vertical.started += instance.OnVertical;
            @Vertical.performed += instance.OnVertical;
            @Vertical.canceled += instance.OnVertical;
            @Horizontal.started += instance.OnHorizontal;
            @Horizontal.performed += instance.OnHorizontal;
            @Horizontal.canceled += instance.OnHorizontal;
            @RotateCameraRight.started += instance.OnRotateCameraRight;
            @RotateCameraRight.performed += instance.OnRotateCameraRight;
            @RotateCameraRight.canceled += instance.OnRotateCameraRight;
            @RotateCameraLeft.started += instance.OnRotateCameraLeft;
            @RotateCameraLeft.performed += instance.OnRotateCameraLeft;
            @RotateCameraLeft.canceled += instance.OnRotateCameraLeft;
            @MouseMovement.started += instance.OnMouseMovement;
            @MouseMovement.performed += instance.OnMouseMovement;
            @MouseMovement.canceled += instance.OnMouseMovement;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
        }

        private void UnregisterCallbacks(ICameraActions instance)
        {
            @Vertical.started -= instance.OnVertical;
            @Vertical.performed -= instance.OnVertical;
            @Vertical.canceled -= instance.OnVertical;
            @Horizontal.started -= instance.OnHorizontal;
            @Horizontal.performed -= instance.OnHorizontal;
            @Horizontal.canceled -= instance.OnHorizontal;
            @RotateCameraRight.started -= instance.OnRotateCameraRight;
            @RotateCameraRight.performed -= instance.OnRotateCameraRight;
            @RotateCameraRight.canceled -= instance.OnRotateCameraRight;
            @RotateCameraLeft.started -= instance.OnRotateCameraLeft;
            @RotateCameraLeft.performed -= instance.OnRotateCameraLeft;
            @RotateCameraLeft.canceled -= instance.OnRotateCameraLeft;
            @MouseMovement.started -= instance.OnMouseMovement;
            @MouseMovement.performed -= instance.OnMouseMovement;
            @MouseMovement.canceled -= instance.OnMouseMovement;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
        }

        public void RemoveCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_PlaceBuilding;
    private readonly InputAction m_Player_RotateBuilding;
    private readonly InputAction m_Player_ExitCurrentMode;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlaceBuilding => m_Wrapper.m_Player_PlaceBuilding;
        public InputAction @RotateBuilding => m_Wrapper.m_Player_RotateBuilding;
        public InputAction @ExitCurrentMode => m_Wrapper.m_Player_ExitCurrentMode;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @PlaceBuilding.started += instance.OnPlaceBuilding;
            @PlaceBuilding.performed += instance.OnPlaceBuilding;
            @PlaceBuilding.canceled += instance.OnPlaceBuilding;
            @RotateBuilding.started += instance.OnRotateBuilding;
            @RotateBuilding.performed += instance.OnRotateBuilding;
            @RotateBuilding.canceled += instance.OnRotateBuilding;
            @ExitCurrentMode.started += instance.OnExitCurrentMode;
            @ExitCurrentMode.performed += instance.OnExitCurrentMode;
            @ExitCurrentMode.canceled += instance.OnExitCurrentMode;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @PlaceBuilding.started -= instance.OnPlaceBuilding;
            @PlaceBuilding.performed -= instance.OnPlaceBuilding;
            @PlaceBuilding.canceled -= instance.OnPlaceBuilding;
            @RotateBuilding.started -= instance.OnRotateBuilding;
            @RotateBuilding.performed -= instance.OnRotateBuilding;
            @RotateBuilding.canceled -= instance.OnRotateBuilding;
            @ExitCurrentMode.started -= instance.OnExitCurrentMode;
            @ExitCurrentMode.performed -= instance.OnExitCurrentMode;
            @ExitCurrentMode.canceled -= instance.OnExitCurrentMode;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface ICameraActions
    {
        void OnVertical(InputAction.CallbackContext context);
        void OnHorizontal(InputAction.CallbackContext context);
        void OnRotateCameraRight(InputAction.CallbackContext context);
        void OnRotateCameraLeft(InputAction.CallbackContext context);
        void OnMouseMovement(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
    public interface IPlayerActions
    {
        void OnPlaceBuilding(InputAction.CallbackContext context);
        void OnRotateBuilding(InputAction.CallbackContext context);
        void OnExitCurrentMode(InputAction.CallbackContext context);
    }
}
