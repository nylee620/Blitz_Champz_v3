using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class DeckOfCard : MonoBehaviour
{
    public List<GameObject> draw_deck;
    public bool ready = false;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Vector3 deck_position = gameObject.GetComponent<Transform>().position;
            //Create and instantiate all of the cards in the deck
            for (int a = 0; a < 2; a++)
            {
                GameObject new_card = PhotonNetwork.InstantiateSceneObject("hail_mary_", deck_position, transform.rotation);
                draw_deck.Add(new_card);
            }
            for (int a = 0; a < 6; a++)
            {
                GameObject new_rushingtd = PhotonNetwork.InstantiateSceneObject("rushing_td_", deck_position, transform.rotation);
                draw_deck.Add(new_rushingtd);
                GameObject new_passingtd = PhotonNetwork.InstantiateSceneObject("passing_td_", deck_position, transform.rotation);
                draw_deck.Add(new_passingtd);
                GameObject new_conversion = PhotonNetwork.InstantiateSceneObject("conversion_", deck_position, transform.rotation);
                draw_deck.Add(new_conversion);
            }
            for (int a = 0; a < 7; a++)
            {
                GameObject new_fieldgoal = PhotonNetwork.InstantiateSceneObject("field_goal_", deck_position, transform.rotation);
                draw_deck.Add(new_fieldgoal);
            }
            for (int a = 0; a < 11; a++)
            {
                GameObject new_extra_point = PhotonNetwork.InstantiateSceneObject("extra_point_", deck_position, transform.rotation);
                draw_deck.Add(new_extra_point);
            }


            for (int a = 0; a < 3; a++)
            {
                GameObject new_interception = PhotonNetwork.InstantiateSceneObject("interception_", deck_position, transform.rotation);
                draw_deck.Add(new_interception);
            }
            for (int a = 0; a < 4; a++)
            {
                GameObject new_tackle = PhotonNetwork.InstantiateSceneObject("tackle_", deck_position, transform.rotation);
                draw_deck.Add(new_tackle);
            }
            for (int a = 0; a < 11; a++)
            {
                GameObject new_blocked_kick = PhotonNetwork.InstantiateSceneObject("blocked_kick_", deck_position, transform.rotation);
                draw_deck.Add(new_blocked_kick);
            }

            for (int a = 0; a < 4; a++)
            {
                GameObject new_fumble = PhotonNetwork.InstantiateSceneObject("fumble_", deck_position, transform.rotation);
                draw_deck.Add(new_fumble);
            }
            for (int a = 0; a < 8; a++)
            {
                GameObject new_first_down = PhotonNetwork.InstantiateSceneObject("first_down_", deck_position, transform.rotation);
                draw_deck.Add(new_first_down);
            }
            for (int a = 0; a < 8; a++)
            {
                GameObject new_pass_completion = PhotonNetwork.InstantiateSceneObject("pass_completion_", deck_position, transform.rotation);
                draw_deck.Add(new_pass_completion);
            }
            for (int a = 0; a < 8; a++)
            {
                GameObject new_5_yard_run = PhotonNetwork.InstantiateSceneObject("5_yard_run_", deck_position, transform.rotation);
                draw_deck.Add(new_5_yard_run);
            }
            for (int a = 0; a < 8; a++)
            {
                GameObject new_blitz = PhotonNetwork.InstantiateSceneObject("blitz_", deck_position, transform.rotation);
                draw_deck.Add(new_blitz);
            }
            for (int a = 0; a < 2; a++)
            {
                GameObject new_end_of_quarter1 = PhotonNetwork.InstantiateSceneObject("end_of_quarter1_", deck_position, transform.rotation);
                draw_deck.Add(new_end_of_quarter1);
            }
            for (int a = 0; a < 2; a++)
            {
                GameObject new_end_of_quarter2 = PhotonNetwork.InstantiateSceneObject("end_of_quarter2_", deck_position, transform.rotation);
                draw_deck.Add(new_end_of_quarter2);
            }
            for (int a = 0; a < 2; a++)
            {
                GameObject new_end_of_quarter3 = PhotonNetwork.InstantiateSceneObject("end_of_quarter3_", deck_position, transform.rotation);
                draw_deck.Add(new_end_of_quarter3);
            }
            for (int a = 0; a < 2; a++)
            {
                GameObject new_end_of_quarter4 = PhotonNetwork.InstantiateSceneObject("end_of_quarter4_", deck_position, transform.rotation);
                draw_deck.Add(new_end_of_quarter4);
            }
            for (int a = 0; a < draw_deck.Count; a++)
            {
                draw_deck[a].GetComponent<BaseCard>().Hide();
            }
            gameObject.GetComponent<Transform>().position = gameObject.transform.position + new Vector3(0f, 0f, -2);
            ready = true;
            Debug.Log("Deck made");
        }
    }

    public GameObject Draw()
    {
        int random_num = Random.Range(0, draw_deck.Count);
        GameObject drawn_card = draw_deck[random_num];
        draw_deck.RemoveAt(random_num);
        drawn_card.GetComponent<BaseCard>().Hide();
        return drawn_card;
    }
    private void OnMouseUpAsButton()
    {
    }
    void Update()
    {
    }
}