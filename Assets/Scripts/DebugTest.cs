using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    [Header("REFERENCIAS")]
    public GameObject hoguera;
    public GameObject player1;
    public GameObject player2;
    public GameObject colliders;
    public GameObject rotationPivot;
        
    [SerializeField] private protected enum Items {ReplantarArboles, ConstruirTodo, RomperTodo};    
    [SerializeField] bool m_hogueraSeExtingue = true;
    [ContextMenuItem("Hoguera al maximo", "ReavivarHoguera")]
    [Range(0, 5)]
    [SerializeField] float m_frecuenciaDeExtincion;
    [SerializeField] bool m_sinColliders = false;
    [SerializeField] bool m_modoUnJugador = false;    

    Bonfire m_hogueraScript;
    ScenarioRotation m_scenarioRotation;
    //Sun m_solScript;

    // Start is called before the first frame update
    void Start()
    {
        m_scenarioRotation = rotationPivot.GetComponent<ScenarioRotation>();
        m_hogueraScript = hoguera.GetComponent<Bonfire>();
        m_frecuenciaDeExtincion = m_hogueraScript.extinguishFrequency;

        player1.SetActive(!m_modoUnJugador);
        m_scenarioRotation.onePlayerMode = m_modoUnJugador;        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_hogueraSeExtingue) { m_hogueraScript.extinguishFrequency = 0; }
        else { m_hogueraScript.extinguishFrequency = m_frecuenciaDeExtincion; }
        colliders.SetActive(!m_sinColliders);        
    }

    void ReavivarHoguera()
    {
        m_hogueraScript.fire = m_hogueraScript.maxFire;
    }
}
