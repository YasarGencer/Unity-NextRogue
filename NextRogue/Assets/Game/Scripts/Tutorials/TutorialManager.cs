 
using UnityEngine; 

public class TutorialManager : MonoBehaviour
{
    public TutorialSpellsManager TutorialSpellsManager { get; private set; }
    public TutorialUIManager TutorialUIManager{ get; private set; }
    private void Start() {
        Initialize();
    }
    void Initialize() { 
        TutorialSpellsManager = GetComponent<TutorialSpellsManager>(); 
        TutorialUIManager= GetComponent<TutorialUIManager>();

        TutorialSpellsManager.Initialize();
        TutorialUIManager.Initialize();
    }
    public void OnPlayerInitialized() {
        TutorialSpellsManager.OnPlayerInitialized();
        TutorialUIManager.OnPlayerInitialized();
    }
}
