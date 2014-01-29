﻿#region Copyright Notice & License Information
//
// TestFlight.cs
//
// Author:
//       Matthew Davey <matthew.davey@dotbunny.com>
//
// Copyright (c) 2013 dotBunny Inc. (http://www.dotbunny.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
#endregion

using UnityEngine;

#if (UNITY_IPHONE || UNITY_IOS) && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
namespace Hydrogen.Plugins
{
		/// <summary>
		/// A static method of interacting with the TestFlight system.. 
		/// 
		/// It can be implemented in numourous ways, this simply serves as a place to call functions.
		/// </summary>
		public class TestFlight
		{
				public static bool Session { get; private set; }

				public static bool Flying { get; private set; }
				#if HYDROGEN_TESTFLIGHT && (UNITY_IPHONE || UNITY_IOS) && (!UNITY_EDITOR)
				[DllImport ("__Internal")]
				static extern void TestFlight_Initialize (string deviceUniqueIdentifier);

				public static void Initialize ()
				{
						Session = false;
						Flying = false;

				//TestFlight_Initialize (SystemInfo.deviceUniqueIdentifier);
				TestFlight_Initialize ();
				}

				[DllImport ("__Internal")]
				static extern void TestFlight_StartSession ();

				public static void StartSession ()
				{
						TestFlight_StartSession ();
						Session = true;
				}

				[DllImport ("__Internal")]
				static extern void TestFlight_EndSession ();

				public static void EndSession ()
				{
						TestFlight_EndSession ();
						Session = false;
				}

				[DllImport ("__Internal")]
				static extern void TestFlight_TakeOff (string token);

				public static void TakeOff (string token)
				{
						TestFlight_TakeOff (token);
						Flying = true;
				}

				[DllImport ("__Internal")]
				static extern void TestFlight_SubmitFeedback (string feedbackString);

				public static void SubmitFeedback (string feedbackString)
				{
						TestFlight_SubmitFeedback (feedbackString);
				}

				[DllImport ("__Internal")]
				static extern void TestFlight_PassCheckpoint (string checkpointName);

				public static void PassCheckpoint (string checkpointName)
				{
						TestFlight_PassCheckpoint (checkpointName);
				}

				[DllImport ("__Internal")]
				static extern void TestFlight_AddCustomEnvironmentInformation (string information, string forKey);

				public static void AddCustomEnvironmentInformation (string information, string forKey)
				{
						TestFlight_AddCustomEnvironmentInformation (information, forKey);
				}

				[DllImport ("__Internal")]
				static extern void TestFlight_Log (string message);

				public static void Log (string message)
				{
						TestFlight_Log (message);
				}

				[DllImport ("__Internal")]
				static extern void TestFlight_LogAsync (string message);

				public static void LogAsync (string message)
				{
						TestFlight_LogAsync (message);
				}






#elif HYDROGEN_TESTFLIGHT && UNITY_ANDROID && !UNITY_EDITOR
				static AndroidJavaClass _testFlight;
				static AndroidJavaClass _unityPlayer;
				static AndroidJavaObject _activity;
				static AndroidJavaObject _application;

				public static void Initialize ()
				{
						Session = false;
						Flying = false;
					
						_unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"); 
						_activity = _unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
						_application = _activity.Call<AndroidJavaObject> ("getApplication");
						_testFlight = new AndroidJavaClass ("com.testflightapp.lib.TestFlight"); 
				}

				public static void TakeOff (string token)
				{
						_testFlight.CallStatic ("takeOff", _application, token);
						Flying = true;
				}

				public static void PassCheckpoint (string checkpointName)
				{
						_testFlight.CallStatic ("passCheckpoint", checkpointName);
				}

				public static void AddCustomEnvironmentInformation (string information, string forKey)
				{
				}

				public static void SubmitFeedback (string feedbackString)
				{
				}

				public static void Log (string message)
				{
						_testFlight.CallStatic ("log", message);
				}

				public static void LogAsync (string message)
				{
				}

				public static void EndSession ()
				{
						_testFlight.CallStatic ("endSession");
						Session = false;
				}

				public static void StartSession ()
				{
						_testFlight.CallStatic ("startSession");
						Session = true;
				}







#else
				public static void Initialize ()
				{
						Session = false;
						Flying = false;
				}

				public static void StartSession ()
				{
						Session = true;
				}

				public static void EndSession ()
				{
						Session = false;
				}

				public static void TakeOff (string token)
				{
						Flying = true;	
				}

				public static void PassCheckpoint (string checkpointName)
				{
				}

				public static void AddCustomEnvironmentInformation (string information, string forKey)
				{
				}

				public static void SubmitFeedback (string feedbackString)
				{
				}

				public static void Log (string message)
				{
				}

				public static void LogAsync (string message)
				{
				}
				#endif
		}
}
