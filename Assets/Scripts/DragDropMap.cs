using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDropMap : MonoBehaviour
{
    //Usado para corrigir a posição do objeto com a localização do mouse
    private Vector3 mouseOffset;
    //Salva a localização inicial do objeto para poder devolver se for solto fora do lugar certo
    private Vector3 posInicial;
    //Aponta o script Game para interagir individualmente com cada objeto
    public Game scriptGame;
    //Salva a cor atual do objeto para passar para outro objeto quando movido
    private Color corObj;
    //Positivo se for uma engrenagem do lado de cima e negativo se for do lado de baixo, usado para definir a direção que vai girar
    public bool pos;
    //Usado para saber se ganhou para começar e parar de girar
    private bool ganhou;
    void Awake(){
        //Salva a posicão inicial para poder devolver
        posInicial = this.transform.position;
    }
    //Roda quando ativa o objeto executa essa parte
    void OnEnable(){
        //Pega a cor que está no objeto
        corObj = this.gameObject.GetComponent<SpriteRenderer>().color;
        //Salva a cor do objeto na variavel dentro do script Game
        scriptGame.cor = corObj;
        //Ativa o collider para poder selecionar o objeto
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
    //Roda quando clica em um objeto na cena
    void OnMouseDown(){
        //Captura o posicionamento do mouse conforme a cena
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
    }
    //Usado para saber aonde está o mouse na tela
    private Vector3 GetMouseWorldPos(){
        //Captura o mouse
        Vector3 mousePoint = Input.mousePosition;
        //Devolve a posição do mouse conforme a camera
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    //Roda quando arrasta um obejto da cena
    void OnMouseDrag(){
        //Muda a posicção do objeto conforme o movimento do mouse
        this.transform.position = GetMouseWorldPos() + mouseOffset;
        //Desativa o collider para o Raycast passar por ele
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        //atualiza o raycast para saber no que ele está focado
        scriptGame.Hit();
    }
    //Roda quando larga o mouse que estava segurando um objeto da cena
    void OnMouseUp(){
        //Devolve ele para a posição original
        this.transform.position = posInicial;
        //Reativa o collider para poder selecionar ele novamente
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        //Testa no que o raycaste está batendo e se for segue se for um espaço de engrenagem da UI
        if(scriptGame.hit.collider != null && scriptGame.hit.collider.tag == Tags.GearUI){
            //Muda a cor do filho do objeto que foi acertado pelo raycast
            scriptGame.hit.collider.transform.GetChild(0).gameObject.GetComponent<Image>().color = corObj;
            //Ativa o objeto filho do que foi acertado pelo raycast
            scriptGame.hit.collider.transform.GetChild(0).gameObject.SetActive(true);
            //Roda o código para diminuir a contagem de engrenagens na tela
            scriptGame.Girar(false);
            //Desativa a engrenagem atual
            this.gameObject.SetActive(false);
        }
    }
    public void Update(){
        //Altera a variavel "ganhou" do código atual com a versão salva no script Game
        ganhou = Game.ganhou;
        //Testa se o ganhou está positivo
        if(ganhou){
            //Verifica a posição da engrenagem para escolher a direção que vai girar
            if(pos){
                //Gira a engrenagem em sentido horário
                this.transform.Rotate(0.0f, 0.0f, -1.0f,Space.World);
            } else {
                //Gira a engrenagem em sentido anti-horário
                this.transform.Rotate(0.0f, 0.0f, 1.0f,Space.World);
            }
        } else {
            //Para de girar as engrenagens
            this.transform.Rotate(0.0f, 0.0f, 0.0f,Space.World);
        }
    }
}
