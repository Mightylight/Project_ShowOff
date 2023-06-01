//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Settings/JoystickControls.inputactions
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

public partial class @JoystickControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @JoystickControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""JoystickControls"",
    ""maps"": [
        {
            ""name"": ""Alligator"",
            ""id"": ""aacb8334-df8a-4d73-b338-b0fe1e6b429c"",
            ""actions"": [
                {
                    ""name"": ""Bite"",
                    ""type"": ""Button"",
                    ""id"": ""1cdee587-6bcf-4ac5-b65c-1d09dab4196e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""78a011c7-098a-4344-9f0b-b8f461c3e299"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Bite"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Alligator
        m_Alligator = asset.FindActionMap("Alligator", throwIfNotFound: true);
        m_Alligator_Bite = m_Alligator.FindAction("Bite", throwIfNotFound: true);
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

    // Alligator
    private readonly InputActionMap m_Alligator;
    private List<IAlligatorActions> m_AlligatorActionsCallbackInterfaces = new List<IAlligatorActions>();
    private readonly InputAction m_Alligator_Bite;
    public struct AlligatorActions
    {
        private @JoystickControls m_Wrapper;
        public AlligatorActions(@JoystickControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Bite => m_Wrapper.m_Alligator_Bite;
        public InputActionMap Get() { return m_Wrapper.m_Alligator; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AlligatorActions set) { return set.Get(); }
        public void AddCallbacks(IAlligatorActions instance)
        {
            if (instance == null || m_Wrapper.m_AlligatorActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_AlligatorActionsCallbackInterfaces.Add(instance);
            @Bite.started += instance.OnBite;
            @Bite.performed += instance.OnBite;
            @Bite.canceled += instance.OnBite;
        }

        private void UnregisterCallbacks(IAlligatorActions instance)
        {
            @Bite.started -= instance.OnBite;
            @Bite.performed -= instance.OnBite;
            @Bite.canceled -= instance.OnBite;
        }

        public void RemoveCallbacks(IAlligatorActions instance)
        {
            if (m_Wrapper.m_AlligatorActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IAlligatorActions instance)
        {
            foreach (var item in m_Wrapper.m_AlligatorActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_AlligatorActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public AlligatorActions @Alligator => new AlligatorActions(this);
    public interface IAlligatorActions
    {
        void OnBite(InputAction.CallbackContext context);
    }
}
