using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Utilizado para localizar o Mouse na cena e verificar no que está batendo o raycast
    [HideInInspector] public Vector2 worldPoint;
    [HideInInspector] public RaycastHit2D hit;
    //pega o componente de texto no formato adequado para poder mudar quando ganha ou tira algum das engrenagens
    [HideInInspector] public static Text txt;
    //Contador para saber quantas engrenagens estão posicionadas
    [HideInInspector] public static int cont = 0;
    //Marca quando atingir o objetivo de colocar as engrenagens
    public static bool ganhou = false;
    //pega o gmae object aonde está escrito a frase do Nugget
    private GameObject painel;
    //Guarda a cor da engrenagem para quando for movida passar para o outro objeto
    public Color cor = Color.white;
    //Usado para destacar se já mudou a frase depois que ganhou
    private bool mudouTexto = false;
    void Awake(){
        //Captura com a tag o painel de conversa do Nugget
        painel = GameObject.FindWithTag(Tags.Texto);
        //Captura o componente de texto para poder mudar depois
        txt = painel.GetComponent<Text>();
    }
    //chamado quando coloca uma nova engrenagem ou retira
    public void Girar(bool operador){
        //verifica se está tirando ou colocando uma engrenagem para contabilizar
        if(operador){
            cont += 1;
        } else {
            cont -= 1;
        }
        //verifica se atingiu a quantidade de engrenagens necessárias para ganhar
        if(cont >= 5){
            ganhou = true;
        } else {
            ganhou = false;
        }
    }
    //Verifica aonde está acertando o raycast do mouse
    public void Hit(){
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(worldPoint, Vector2.zero);
    }
    public void Update(){
        //Verifia se atingiu o objetivo
        if(ganhou){
            //Verifica se o texto já foi alterado para não ficar mandando mudar o texto o tempo todo
            if(!mudouTexto){
                //Muda para o texto de vitória
                txt.text = "YAY, PARABÉNS. TASK CONCLUÍDA!";
                //Bloqueia para não entrar novamente aqui e ficar mandando mudar a mensagem
                mudouTexto = true;
            }
        } else {
            //Texta se acabou de sair do ganhou e deve voltar a frase
            if(mudouTexto){
                //Muda para a frase pedindo para colocar as engrenagens
                txt.text = "ENCAIXE AS ENGRENAGENS EM QUALQUER ORDEM!";
                //Bloqueia para não mudar a mensagem de novo e deixar pronto para quando ganhar novamente
                mudouTexto = false;
            }
        }
    }
}
