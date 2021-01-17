// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/MyInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace MayoMatic
{
    public class @MyInputAction : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MyInputAction()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyInputAction"",
    ""maps"": [
        {
            ""name"": ""Mayomatic"",
            ""id"": ""97229575-19ff-4997-9cce-e12eadb3e445"",
            ""actions"": [
                {
                    ""name"": ""StartButton"",
                    ""type"": ""Button"",
                    ""id"": ""dc0dbd5d-667f-4811-91fe-bfa68b4a26cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OilButton"",
                    ""type"": ""Button"",
                    ""id"": ""60c33d0b-3a7b-42e1-b3a6-22053f80e5e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SaltButton"",
                    ""type"": ""Button"",
                    ""id"": ""5ace1737-32d7-4c04-808a-b05791bfe0d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MustardButton"",
                    ""type"": ""Button"",
                    ""id"": ""17e46588-1d0f-4956-8e49-b1b404f6e022"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VinegarButton"",
                    ""type"": ""Button"",
                    ""id"": ""030bd09e-4ada-47f5-b8c1-04bb156ebab1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Joystick"",
                    ""type"": ""Button"",
                    ""id"": ""babad0f3-022c-4dbb-9c81-a9b88005f31c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f73b6014-2657-4ee1-8425-0d3cdae94a0b"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be898a22-2590-4e02-af1c-a040eceecc02"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OilButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19abddc6-a795-451d-ad06-87bbb9fbf2c1"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SaltButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7113260b-fdb4-46ae-9a22-b25355f736e9"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MustardButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1740528e-fdce-4687-82f4-8080dd3cc06d"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VinegarButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62bcd421-29b8-4107-be4e-9aa05dc1c589"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Mayomatic
            m_Mayomatic = asset.FindActionMap("Mayomatic", throwIfNotFound: true);
            m_Mayomatic_StartButton = m_Mayomatic.FindAction("StartButton", throwIfNotFound: true);
            m_Mayomatic_OilButton = m_Mayomatic.FindAction("OilButton", throwIfNotFound: true);
            m_Mayomatic_SaltButton = m_Mayomatic.FindAction("SaltButton", throwIfNotFound: true);
            m_Mayomatic_MustardButton = m_Mayomatic.FindAction("MustardButton", throwIfNotFound: true);
            m_Mayomatic_VinegarButton = m_Mayomatic.FindAction("VinegarButton", throwIfNotFound: true);
            m_Mayomatic_Joystick = m_Mayomatic.FindAction("Joystick", throwIfNotFound: true);
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

        // Mayomatic
        private readonly InputActionMap m_Mayomatic;
        private IMayomaticActions m_MayomaticActionsCallbackInterface;
        private readonly InputAction m_Mayomatic_StartButton;
        private readonly InputAction m_Mayomatic_OilButton;
        private readonly InputAction m_Mayomatic_SaltButton;
        private readonly InputAction m_Mayomatic_MustardButton;
        private readonly InputAction m_Mayomatic_VinegarButton;
        private readonly InputAction m_Mayomatic_Joystick;
        public struct MayomaticActions
        {
            private @MyInputAction m_Wrapper;
            public MayomaticActions(@MyInputAction wrapper) { m_Wrapper = wrapper; }
            public InputAction @StartButton => m_Wrapper.m_Mayomatic_StartButton;
            public InputAction @OilButton => m_Wrapper.m_Mayomatic_OilButton;
            public InputAction @SaltButton => m_Wrapper.m_Mayomatic_SaltButton;
            public InputAction @MustardButton => m_Wrapper.m_Mayomatic_MustardButton;
            public InputAction @VinegarButton => m_Wrapper.m_Mayomatic_VinegarButton;
            public InputAction @Joystick => m_Wrapper.m_Mayomatic_Joystick;
            public InputActionMap Get() { return m_Wrapper.m_Mayomatic; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MayomaticActions set) { return set.Get(); }
            public void SetCallbacks(IMayomaticActions instance)
            {
                if (m_Wrapper.m_MayomaticActionsCallbackInterface != null)
                {
                    @StartButton.started -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnStartButton;
                    @StartButton.performed -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnStartButton;
                    @StartButton.canceled -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnStartButton;
                    @OilButton.started -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnOilButton;
                    @OilButton.performed -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnOilButton;
                    @OilButton.canceled -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnOilButton;
                    @SaltButton.started -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnSaltButton;
                    @SaltButton.performed -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnSaltButton;
                    @SaltButton.canceled -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnSaltButton;
                    @MustardButton.started -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnMustardButton;
                    @MustardButton.performed -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnMustardButton;
                    @MustardButton.canceled -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnMustardButton;
                    @VinegarButton.started -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnVinegarButton;
                    @VinegarButton.performed -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnVinegarButton;
                    @VinegarButton.canceled -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnVinegarButton;
                    @Joystick.started -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnJoystick;
                    @Joystick.performed -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnJoystick;
                    @Joystick.canceled -= m_Wrapper.m_MayomaticActionsCallbackInterface.OnJoystick;
                }
                m_Wrapper.m_MayomaticActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @StartButton.started += instance.OnStartButton;
                    @StartButton.performed += instance.OnStartButton;
                    @StartButton.canceled += instance.OnStartButton;
                    @OilButton.started += instance.OnOilButton;
                    @OilButton.performed += instance.OnOilButton;
                    @OilButton.canceled += instance.OnOilButton;
                    @SaltButton.started += instance.OnSaltButton;
                    @SaltButton.performed += instance.OnSaltButton;
                    @SaltButton.canceled += instance.OnSaltButton;
                    @MustardButton.started += instance.OnMustardButton;
                    @MustardButton.performed += instance.OnMustardButton;
                    @MustardButton.canceled += instance.OnMustardButton;
                    @VinegarButton.started += instance.OnVinegarButton;
                    @VinegarButton.performed += instance.OnVinegarButton;
                    @VinegarButton.canceled += instance.OnVinegarButton;
                    @Joystick.started += instance.OnJoystick;
                    @Joystick.performed += instance.OnJoystick;
                    @Joystick.canceled += instance.OnJoystick;
                }
            }
        }
        public MayomaticActions @Mayomatic => new MayomaticActions(this);
        public interface IMayomaticActions
        {
            void OnStartButton(InputAction.CallbackContext context);
            void OnOilButton(InputAction.CallbackContext context);
            void OnSaltButton(InputAction.CallbackContext context);
            void OnMustardButton(InputAction.CallbackContext context);
            void OnVinegarButton(InputAction.CallbackContext context);
            void OnJoystick(InputAction.CallbackContext context);
        }
    }
}
