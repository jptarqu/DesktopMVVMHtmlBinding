using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross;
using Cirrious.MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReportingFramework.ViewModels
{
    /// <summary>
    /// Adapted from http://blog.fire-development.com/2013/06/29/mvvmcross-enable-unit-testing/
    /// </summary>
    public class MvvmCrossSetup
    {
        private IMvxIoCProvider _ioc;

        protected IMvxIoCProvider Ioc
        {
            get { return _ioc; }
        }

        public void Setup()
        {
            ClearAll();
            Mvx.Resolve<IMvxSettings>().AlwaysRaiseInpcOnUserInterfaceThread = false; //Very important to allow the PropertyChanged events to be fired up 
        }

        protected void ClearAll()
        {
            // fake set up of the IoC
            MvxSingleton.ClearAllSingletons();
            _ioc = MvxSimpleIoCContainer.Initialize();
            _ioc.RegisterSingleton(_ioc);
            _ioc.RegisterSingleton<IMvxTrace>(new TestTrace());
            RegisterAdditionalSingletons();
            InitialiseSingletonCache();
            InitialiseMvxSettings();
            MvxTrace.Initialize();
        }

        protected void InitialiseMvxSettings()
        {
            _ioc.RegisterSingleton<IMvxSettings>(new MvxSettings());
        }

        protected virtual void RegisterAdditionalSingletons()
        {
        }

        private static void InitialiseSingletonCache()
        {
            MvxSingletonCache.Initialize();
        }

        private class TestTrace : IMvxTrace
        {
            public void Trace(MvxTraceLevel level, string tag, Func<string> message)
            {
                Debug.WriteLine(tag + ":" + level + ":" + message());
            }

            public void Trace(MvxTraceLevel level, string tag, string message)
            {
                Debug.WriteLine(tag + ": " + level + ": " + message);
            }

            public void Trace(MvxTraceLevel level, string tag, string message, params object[] args)
            {
                Debug.WriteLine(tag + ": " + level + ": " + message, args);
            }
        }
    }
}
