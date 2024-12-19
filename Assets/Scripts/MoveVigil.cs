using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveVigil : MonoBehaviour
{

    public Vector2 turn;              // Pour la rotation via souris

    public float maxX;
    public float maxY;

    [SerializeField] public LayerMask layerMask;

    public List<GameObject> listePrisonniers;
    public int nbBeeSelectedMax;

    public bool beeCaptured;

    private MoveBee mb;
    private MoveRobotBee mrb;

    public ManagerGame managerGame;

    public GameObject canvasHandcluff;
    public GameObject[] listeCanvaHandcuff;
    public Transform pointPrison;

    void Start()
    {
        beeCaptured = false;

        listeCanvaHandcuff = new GameObject[canvasHandcluff.transform.childCount];
        for (int i = 0; i < listeCanvaHandcuff.Length; i++)
        {
            listeCanvaHandcuff[i] = canvasHandcluff.transform.GetChild(i).gameObject;
        }
        GenerateCanvaVigil();
    }
    void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        // Rotation avec la souris
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");

        turn.y = Mathf.Clamp(turn.y, -maxY, maxY);
        turn.x = Mathf.Clamp(turn.x, -maxX, maxX);

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }


    private void OnSelectBee(InputValue value)
    {
        Debug.Log("input");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray à partir de la position de la souris
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask)) // Longueur maximale du ray
        {
            Debug.Log($"Hit {hitInfo.collider.gameObject.layer} at {hitInfo.point}");

            // Faites que l'objet regarde le point cliqué
            Vector3 targetPosition = hitInfo.point;
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0; // Optionnel : ignorez la hauteur si nécessaire
            transform.rotation = Quaternion.LookRotation(direction);
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 2f);
            listePrisonniers.Add(hitInfo.collider.gameObject);
            //listePrisonniers[listePrisonniers.Count - 1].SetActive(false);
            if (listePrisonniers[listePrisonniers.Count - 1].tag == "Bee")
            {
                beeCaptured = true;
                mb = listePrisonniers[listePrisonniers.Count-1].GetComponent<MoveBee>();
                mb.BeeStoped();
            }
            else
            {
                mrb = listePrisonniers[listePrisonniers.Count - 1].GetComponent<MoveRobotBee>();
                mrb.BeeStoped();
                //Methode pour stoper les abeilles

            }
            managerGame.ActiveCaptchaVigil();
            //afficher captcha
        }
        else
        {
            Debug.Log("Mouse click hit nothing");
        }
    }

    public void RapunzelCaptcha(bool rep)
    {
        if (rep)
        {
            // arreter joueur
            GenerateCanvaVigil();
            listePrisonniers[listePrisonniers.Count-1].transform.position = pointPrison.position;
        }
        else
        {
            // Relacher joueur

            if(mb !=  null)
            {
                mb.BeeFree();
                mb = null;
            }
            else
            {
                mrb.BeeFree();
                //Methode pour relacher les abeilles
            }
            listePrisonniers.RemoveAt(listePrisonniers.Count - 1); // on relache l'abeille
        }

        Debug.Log("listePrisonniers.Count == nbBeeSelectedMax : " + listePrisonniers.Count + " == " + nbBeeSelectedMax + " : " + (listePrisonniers.Count == nbBeeSelectedMax));

        if (listePrisonniers.Count == nbBeeSelectedMax)
        {
            VerifEndConditionVigil();
        }
    }

    public void VerifEndConditionVigil()
    {
        if (beeCaptured)
        {
            //Victoire du vigile
            managerGame.VictoireVigil();
        }
        else
        {
            managerGame.VictoireBee();
            //Victoire de l'abeille
        }
    }
    private void GenerateCanvaVigil()
    {
        Debug.Log("listePrisonniers.Count : " + listePrisonniers.Count);
        for (int i = 0; listeCanvaHandcuff.Length > i; i++)
        {
            if (i == (listePrisonniers.Count))
            {
                listeCanvaHandcuff[i].SetActive(true);
            }
            else
            {
                listeCanvaHandcuff[i].SetActive(false);

            }
        }
    }

    public bool BeeIsCaptured()
    {
        return beeCaptured;
    }
}
