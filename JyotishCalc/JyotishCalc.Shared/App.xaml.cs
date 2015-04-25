using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;

using JyotishCalc.Services;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace JyotishCalc
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : MvvmAppBase
    {
        #region Member Variables
        private readonly IUnityContainer _container = new UnityContainer();
        #endregion


        #region Overrides
        /// <summary>
        /// OnInitializeAsync initializes the application.
        /// </summary>
        /// <param name="args">The arguments to the application</param>
        /// <returns>Task object</returns>
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            //Initialize the Unity types
            this.InitTypesInUnity();

            //Return the base method
            return base.OnInitializeAsync(args);
        }


        /// <summary>
        /// OnLaunchApplicationAsync navigates to the main Page.
        /// </summary>
        /// <param name="args">The arguments to the application</param>
        /// <returns>Empty Task object</returns>
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            //Navigate to the ProfileList
            this.NavigationService.Navigate("ProfileList", null);

            //Return a blank Task
            return Task.FromResult<object>(null);
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// InitTypesInUnity puts all of the Types that are to 
        /// be resolved by Unity into the IoC Container.
        /// </summary>
        private void InitTypesInUnity()
        {
            //Initialize the Types
            this._container.RegisterInstance(this.NavigationService);
            this._container.RegisterInstance(new ProfileService());
        }
        #endregion
    }
}