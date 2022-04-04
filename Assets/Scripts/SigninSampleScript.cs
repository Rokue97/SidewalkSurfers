// <copyright file="SigninSampleScript.cs" company="Google Inc.">
// Copyright (C) 2017 Google Inc. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations

namespace SignInSample {
  using System;
    using System.Collections;
    using System.Collections.Generic;
  using System.Threading.Tasks;
    using Google;
  using UnityEngine;
  using UnityEngine.UI;

  public class SigninSampleScript : MonoBehaviour {
        /*
    public Text statusText, welcomeText, debugText;
        public bool signIn = false;
        public FirebaseUser currentUser;
        [SerializeField] LocalCache cache;
        //[SerializeField] Database database;
        [SerializeField] GameObject logInText, logInButton, accountButton;
        [SerializeField] GameManager gameManager;
        bool otosign = false;
        bool exist = false;
        string name;

        public string webClientId = "<your client id here>";

    private GoogleSignInConfiguration configuration;

    // Defer the configuration creation until Awake so the web Client ID
    // Can be set via the property inspector in the Editor.
    void Awake() {
      configuration = new GoogleSignInConfiguration {
            WebClientId = webClientId,
            RequestIdToken = true
      };
    }

    private void Start()
    {
            if (PlayerPrefs.GetInt("SignIn") == 1)
                OnSignInSilently();
            else
                OnSignIn();
    }

    public void OnSignIn() 
    {
      GoogleSignIn.Configuration = configuration;
      GoogleSignIn.Configuration.UseGameSignIn = false;
      GoogleSignIn.Configuration.RequestIdToken = true;
      AddStatusText("Calling SignIn");
            otosign = false;
      GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
        OnAuthenticationFinished);
    }

    public void OnSignOut() {
      AddStatusText("Calling SignOut");
      GoogleSignIn.DefaultInstance.SignOut();
            PlayerPrefs.SetInt("SignIn", 0);
            logInButton.SetActive(true);
            accountButton.SetActive(false);
            signIn = false;
    }

    public void OnDisconnect() {
      AddStatusText("Calling Disconnect");
      GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task) {
      if (task.IsFaulted) {
        using (IEnumerator<System.Exception> enumerator =
                task.Exception.InnerExceptions.GetEnumerator()) {
          if (enumerator.MoveNext()) {
            GoogleSignIn.SignInException error =
                    (GoogleSignIn.SignInException)enumerator.Current;
            AddStatusText("Got Error: " + error.Status + " " + error.Message);
          } else {
            AddStatusText("Got Unexpected Exception?!?" + task.Exception);
          }
        }
      } else if(task.IsCanceled) {
        AddStatusText("Canceled");
      } else  
            {
                AddStatusText("Welcome: " + task.Result.DisplayName + "!");

                FirebaseAuth auth = FirebaseAuth.DefaultInstance;

                Credential credential =
                GoogleAuthProvider.GetCredential(task.Result.IdToken, task.Result.AuthCode);
                auth.SignInWithCredentialAsync(credential).ContinueWith(move => {
                    if (move.IsCanceled)
                    {
                        Debug.LogError("SignInWithCredentialAsync was canceled.");
                        statusText.text = "SignInWithCredentialAsync was canceled.";
                        return;
                    }
                    if (move.IsFaulted)
                    {
                        Debug.LogError("SignInWithCredentialAsync encountered an error: " + move.Exception);
                        statusText.text = "SignInWithCredentialAsync encountered an error: " + move.Exception;
                        return;
                    }

                    FirebaseUser newUser = move.Result;
                    Debug.LogFormat("User signed in successfully: {0} ({1})",
                        newUser.DisplayName, newUser.UserId);
                    statusText.text = newUser.DisplayName + newUser.UserId;
                    currentUser = auth.CurrentUser;

                    logInButton.SetActive(false);
                    accountButton.SetActive(true);

                    FirebaseDatabase.DefaultInstance
                    .GetReference("users")
                    .GetValueAsync().ContinueWith(next => {
                        if (next.IsFaulted)
                        {
                            // Handle the error...
                        }
                        else if (next.IsCompleted)
                        {
                            DataSnapshot snapshot = next.Result;
                            exist = snapshot.Child(currentUser.UserId).Exists;
                            if (exist)
                                name = snapshot.Child(currentUser.UserId).Child("username").Value.ToString();
                            Debug.Log(exist.ToString());
                            Debug.Log(name);
                            Debug.Log(cache.GetName());
                            // Do something with snapshot...

                            if (!otosign)
                            {
                                if (exist)
                                {
                                    if (cache.GetName() == name)
                                    {
                                        database.WriteAllData();
                                        Debug.Log("Updated");
                                    }
                                    else
                                    {
                                        database.GetAllData();
                                        Debug.Log("Get All Data");
                                    }
                                }
                                else
                                {
                                    database.ReadyWriteNewUser();
                                    Debug.Log("New User");
                                }
                            }
                            else
                            {
                                database.WriteAllData();
                                Debug.Log("Written");
                            }
                        }
                    });

                    FirebaseUser user = auth.CurrentUser;
                    if (user != null)
                    {
                        string name = user.DisplayName;
                        string email = user.Email;
                        System.Uri photo_url = user.PhotoUrl;
                        // The user's Id, unique to the Firebase project.
                        // Do NOT use this value to authenticate with your backend server, if you
                        // have one; use User.TokenAsync() instead.
                        string uid = user.UserId;
                    }

                    PlayerPrefs.SetInt("SignIn", 1);
                    LogInText(user.DisplayName);                   
                    signIn = true;
                });                            
            }
    }

    public void OnSignInSilently() {
      GoogleSignIn.Configuration = configuration;
      GoogleSignIn.Configuration.UseGameSignIn = false;
      GoogleSignIn.Configuration.RequestIdToken = true;
      AddStatusText("Calling SignIn Silently");
            otosign = true;
      GoogleSignIn.DefaultInstance.SignInSilently()
            .ContinueWith(OnAuthenticationFinished);
    }


    public void OnGamesSignIn() {
      GoogleSignIn.Configuration = configuration;
      GoogleSignIn.Configuration.UseGameSignIn = true;
      GoogleSignIn.Configuration.RequestIdToken = false;

      AddStatusText("Calling Games SignIn");

      GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
        OnAuthenticationFinished);
    }

    private List<string> messages = new List<string>();
    void AddStatusText(string text) {
      if (messages.Count == 5) {
        messages.RemoveAt(0);
      }
      messages.Add(text);
      string txt = "";
      foreach (string s in messages) {
        txt += "\n" + s;
      }
      statusText.text = txt;
    }
    
    public void LogInText(string displayName)
        {
            logInText.SetActive(true);
            StartCoroutine(DisableLogInText());
            welcomeText.text = "Welcome " + displayName;
        }

        IEnumerator DisableLogInText()
        {
            yield return new WaitForSeconds(3f);
            logInText.SetActive(false);
        }
        */
  }
        
}
