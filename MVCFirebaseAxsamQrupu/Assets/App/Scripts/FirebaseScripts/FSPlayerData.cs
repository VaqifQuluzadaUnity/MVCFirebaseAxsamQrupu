using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public class FSPlayerData
{
  [FirestoreProperty]
   public string Name { get; set; }

  [FirestoreProperty]
  public string NickName { get; set; }

  [FirestoreProperty]
  public string Password { get; set; }
}
