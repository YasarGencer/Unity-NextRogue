using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : AInteract {
    [TextArea, SerializeField] List<string> _npcTalk;
    protected override void OnStart() {
        base.OnStart(); 
    }
    public void InteractPublic() {
        Interact();
    }
    protected override void Interact() { 
        InfoText(_npcTalk[Random.Range(0, _npcTalk.Count)]);
    }
    protected override void Info(bool value) {
        base.Info(value);
        if(value)
            InfoText(_npcTalk[Random.Range(0, _npcTalk.Count)]);
    }
}
