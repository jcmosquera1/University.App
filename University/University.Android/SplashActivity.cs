﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Droid
{
    [Activity(MainLauncher = true, 
              NoHistory =true,
             Theme =  "@style / MyTheme.Splash")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            Task.Delay(1800);
            StartActivity(typeof(MainActivity));

           
        }
    }
}