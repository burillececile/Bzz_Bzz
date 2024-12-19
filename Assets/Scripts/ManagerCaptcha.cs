

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class ManagerCaptcha : MonoBehaviour
{

    public Color colorInitPicture;
    public Color colorSelectedPicture;

    public List<Button> listButonPicture;
    public List<Button> listButonSelected;
    public List<int> listIndexNicePicture;
    public int nbNicePicture;

    public Sprite[] spritesNice;
    public Sprite[] spriteBad;

    public List<int> indexNicePicture;
    public List<Button> listBadPicture;

    public ManagerGame managerGame;
    // Start is called before the first frame update
    void Start()
    {
        spritesNice = Resources.LoadAll("Captcha/PictureTrue/", typeof(Sprite)).Cast<Sprite>().ToArray();
        spriteBad = Resources.LoadAll("Captcha/PictureFalse/", typeof(Sprite)).Cast<Sprite>().ToArray();
        GenerateCaptcha();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedPicture(Button button)
    {
        if (listButonSelected.Contains(button))
        {
            // on supprime de la liste des selection
            listButonSelected.Remove(button);
            button.image.color = colorInitPicture;
        }
        else
        {
            // on selectionne
            listButonSelected.Add(button);
            button.image.color = colorSelectedPicture;

        }
    }

    public void ValideCaptcha()
    {
        bool captchaValid = true;
        for (int i = 0; i < listButonSelected.Count; i++)
        {
            if (listBadPicture.Contains(listButonSelected[i]) || listButonSelected.Count!=nbNicePicture)
            {
                captchaValid = false;
            }
            else
            {

            }
        }

        if(captchaValid)
        {

            Debug.Log("captcha réussi");
        }
        else
        {

            Debug.Log("captcha raté");
        }
        if (gameObject.name.Contains("Vigil"))
        {
            managerGame.DisableCaptchaVigil(captchaValid);

        }
        else
        {

            managerGame.DisableCaptcha(captchaValid);
        }


    }

    public void GenerateCaptcha()
    {

        nbNicePicture = Random.Range(3, 6);
        listButonSelected = new List<Button>();
        listBadPicture = new List<Button>();
        listIndexNicePicture = new List<int>();
        indexNicePicture = new List<int>();
        listBadPicture = new List<Button>();

        while (listIndexNicePicture.Count < nbNicePicture)
        {
            int index = Random.Range(0, listButonPicture.Count);
            if (!listIndexNicePicture.Contains(index))
            {
                listIndexNicePicture.Add(index);
            }
        }
        for (int i = 0; i < listButonPicture.Count; i++)
        {
            listButonPicture[i].image.color = colorInitPicture;

            if (!listIndexNicePicture.Contains(i))
            {

                listBadPicture.Add(listButonPicture[i]);
            }
        }

        for (int i = 0; i < listButonPicture.Count; i++)
        {
            if (listIndexNicePicture.Contains(i))
            {
                // Donner une bonne image
                listButonPicture[i].image.sprite = spritesNice[Random.Range(0, spritesNice.Length)];
            }
            else
            {
                // Donner une mauvaise image
                listButonPicture[i].image.sprite = spriteBad[Random.Range(0, spriteBad.Length)];
            }
        }
    }
}
