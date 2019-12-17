using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [Header("REFERENCIAS")]
    [SerializeField] GameObject hoguera;
    [SerializeField] GameObject sol;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
        
    [SerializeField] private protected enum Items {ReplantarArboles, ConstruirTodo, RomperTodo};
    [SerializeField] bool m_cicloSolar = true;
    [SerializeField] bool m_hogueraSeExtingue = true;
    [ContextMenuItem("Hoguera al maximo", "ReavivarHoguera")]
    [Range(0, 5)]
    [SerializeField] float m_frecuenciaDeExtincion;

    Bonfire m_hogueraScript;
    Sun m_solScript;

    // Start is called before the first frame update
    void Start()
    {
        m_hogueraScript = hoguera.GetComponent<Bonfire>();
        m_frecuenciaDeExtincion = m_hogueraScript.extinguishFrequency;
        m_solScript = sol.GetComponent<Sun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_hogueraSeExtingue) { m_hogueraScript.extinguishFrequency = 0; }
        else { m_hogueraScript.extinguishFrequency = m_frecuenciaDeExtincion; }
        m_solScript.enabled = m_cicloSolar;
    }

    void ReavivarHoguera()
    {
        m_hogueraScript.fire = m_hogueraScript.maxFire;
    }
}
