using System;
using System.Collections.Generic;
using Entitas;

/// <summary>
/// Systems manager that allows for systems to be removed and added as need during runtime
/// </summary>
public class sm_Feature : Entitas.VisualDebugging.Unity.DebugSystems, IFixedUpdateSystem {

    protected List<IFixedUpdateSystem> _fixedUpdateSystems;

    public sm_Feature(string name) : base(name) {
        _fixedUpdateSystems = new List<IFixedUpdateSystem>();
    }

    public sm_Feature() : base(true) {
        var typeName = Entitas.Utils.TypeSerializationExtension.ToCompilableString(GetType());
        var shortType = Entitas.Utils.TypeSerializationExtension.ShortTypeName(typeName);
        var readableType = Entitas.Utils.StringExtension.ToSpacedCamelCase(shortType);

        _fixedUpdateSystems = new List<IFixedUpdateSystem>();

        initialize(readableType);
    }

    public override Systems Add(ISystem system) {
        var fixedUpdateSystem = system as IFixedUpdateSystem;
        if (fixedUpdateSystem != null) {
            _fixedUpdateSystems.Add(fixedUpdateSystem);
        }
        return base.Add(system);
    }

    public void Remove(System.Type type) {
        RemoveSystem(_initializeSystems, type);
        RemoveSystem(_executeSystems, type);
        RemoveSystem(_cleanupSystems, type);
        RemoveSystem(_tearDownSystems, type);
        RemoveSystem(_fixedUpdateSystems, type);
    }

    private void RemoveSystem<T>(List<T> systemList, System.Type type) where T : ISystem {
        for (var i = systemList.Count - 1; i >= 0; i--) {
            if (systemList[i].GetType() == type) {
                systemList.RemoveAt(i);
            }
        }
    }

    public bool HasSystem(System.Type type) {
        if (ContainsType(_initializeSystems, type)) return true;
        if (ContainsType(_executeSystems, type)) return true;
        if (ContainsType(_cleanupSystems, type)) return true;
        if (ContainsType(_tearDownSystems, type)) return true;
        if (ContainsType(_fixedUpdateSystems, type)) return true;
        return false;
    }

    private bool ContainsType<T>(List<T> systems, System.Type type) where T : ISystem {
        return GetSystem<T>(systems, type) != null;
    }

    public ISystem GetSystem(System.Type type) {
        var value = GetSystem(_initializeSystems, type) ?? null;
        value = GetSystem(_executeSystems, type) ?? value;
        value = GetSystem(_cleanupSystems, type) ?? value;
        value = GetSystem(_tearDownSystems, type) ?? value;
        value = GetSystem(_fixedUpdateSystems, type) ?? value;
        return value;
    }

    private ISystem GetSystem<T>(List<T> systems, System.Type type) where T : ISystem {
        foreach (var s in systems) {
            if (s.GetType() == type) return s;
        }
        return null;
    }

    public void FixedUpdate() {
        for (var i = 0; i < _fixedUpdateSystems.Count; i++) {
            _fixedUpdateSystems[i].FixedUpdate();
        }
    }
}
