using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

[System.Serializable]
[CreateAssetMenu(fileName = "Component Set", menuName = "SuperMash/Entities/Component Set")]
public class ComponentSet : ScriptableObject {

    //public IEntity Entity;
    public IContext Context;

    //[HideInInspector]
    //public Dictionary<System.Reflection.FieldInfo, object> SavedValues;
    [HideInInspector]
    public List<System.Reflection.FieldInfo> Fields;
    [HideInInspector]
    public List<object> Values;

    [HideInInspector]
    public List<int> Test;

    public void SetupComponents(Contexts contexts) { }
}

public class SerializationTest {
    public int Test;
}