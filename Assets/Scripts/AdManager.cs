using System;
using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {
	public static InterstitialAd interstitial;

	public static void RequestInterstitial()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-7698866020497726/1650236292";
		#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
		string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create an interstitial.
		interstitial = new InterstitialAd (adUnitId);
		// Register for ad events.
		interstitial.OnAdLoaded += HandleInterstitialLoaded;
		interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.OnAdOpening += HandleInterstitialOpened;
		interstitial.OnAdClosed += HandleInterstitialClosed;
		interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;
		// Load an interstitial ad.
		interstitial.LoadAd (createAdRequest ());

	}
	
	// Returns an ad request with custom ad targeting.
	public static AdRequest createAdRequest()
	{
	return new AdRequest.Builder()
	.AddTestDevice(AdRequest.TestDeviceSimulator)
	.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
	.AddKeyword("game")
	.SetGender(Gender.Male)
	.SetBirthday(new DateTime(1985, 1, 1))
	.TagForChildDirectedTreatment(false)
	.AddExtra("color_bg", "9B30FF")
	.Build();
	}

	public static void ShowInterstitial()
	{
		if (interstitial.IsLoaded()) {
			interstitial.Show();
		}

		else {
		print("Interstitial is not ready yet.");
		}
	}

	#region Interstitial callback handlers

	public static void HandleInterstitialLoaded(object sender, EventArgs args)
	{
	print("HandleInterstitialLoaded event received.");
	}

	public static void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
	print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	public static void HandleInterstitialOpened(object sender, EventArgs args)
	{
	print("HandleInterstitialOpened event received");
	}

	public static void HandleInterstitialClosing(object sender, EventArgs args)
	{
	print("HandleInterstitialClosing event received");
	}

	public static void HandleInterstitialClosed(object sender, EventArgs args)
	{
	print("HandleInterstitialClosed event received");
	}

	public static void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
	print("HandleInterstitialLeftApplication event received");
	}

	#endregion

}