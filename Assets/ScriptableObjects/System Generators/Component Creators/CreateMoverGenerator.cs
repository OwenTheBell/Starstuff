using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Mover", menuName = "SuperMash/Systems/Create Mover")]
public class CreateMoverGenerator : SystemGenerator {

    public Sprite Sprite;
    public string Name;

    public override ISystem Generate(Contexts contexts) {
        var mover = new CreateMoverSystem(contexts);
        mover.Sprite = Sprite;
        mover.Name = Name;
        return mover;
    }
}
