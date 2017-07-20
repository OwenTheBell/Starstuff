using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

[System.Serializable]
public class ComponentSet {

    public IContext Context;
    public sm_Component[] Components = new sm_Component[10];
    //public int[] testArray;

    public ComponentSet() { }
}
